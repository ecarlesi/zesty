using System;
using System.IO;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Entities.Settings;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Middleware
{
    public class ApiMiddleware
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public ApiMiddleware(RequestDelegate next)
        {
        }

        public async Task Invoke(HttpContext context)
        {
            TimeKeeper timeKeeper = new TimeKeeper();

            Context.Current.Reset();

            ISession session = context.Session;

            Context.Current.User = session.Get<Entities.User>(Keys.SessionUser);

            bool propagateApplicationErrorInFault = Settings.GetBool("PropagateApplicationErrorInFault", false);

            string resourceName = context.Request.Path.Value;
            string body = new StreamReader(context.Request.Body).ReadToEndAsync().Result;

            logger.Info($"Resource: {resourceName}");
            logger.Debug($"Body: {body}");
            logger.Info($"Session ID: {session.Id}");
            logger.Info($"HTTP method: {context.Request.Method}");

            ApiInputHandler input = new ApiInputHandler()
            {
                Body = body,
                Context = context,
                Resource = resourceName
            };

            string contentType = null;
            string content = null;
            int statusCode = 200;

            try
            {
                if (context.Request.Method == "OPTIONS")
                {
                    //TODO improve this poor code :D
                    contentType = ContentType.TextPlain;
                    content = ":)";
                }
                else
                {
                    HandlerProcessor.Process(Settings.List("PreExecutionHandler"), context);

                    ApiHandlerOutput output = Process(input);

                    HandlerProcessor.Process(Settings.List("PostExecutionHandler"), context);

                    if (output.Type == ApiHandlerOutputType.JSon)
                    {
                        contentType = ContentType.ApplicationJson;
                        content = JsonHelper.Serialize(output.Output);
                    }
                    else if (output.Type == ApiHandlerOutputType.TextAsJson)
                    {
                        contentType = ContentType.ApplicationJson;
                        content = output.Output as string;
                    }
                    else if (output.Type == ApiHandlerOutputType.Text)
                    {
                        contentType = ContentType.TextPlain;
                        content = output.Output as string;
                    }
                    else
                    {
                        throw new Exception(Messages.WrongApiOutput);
                    }
                }
            }
            catch (ApiInvalidArgumentException e)
            {
                logger.Error(e);

                statusCode = 501;
                contentType = ContentType.ApplicationJson;
                string message = propagateApplicationErrorInFault ? e.Message : Messages.GenericFailure;
                content = JsonHelper.Serialize(new { Message = message });
            }
            catch (ApiNotFoundException e)
            {
                logger.Error(e);

                statusCode = 404;
                contentType = ContentType.ApplicationJson;
                string message = propagateApplicationErrorInFault ? e.Message : Messages.GenericFailure;
                content = JsonHelper.Serialize(new { Message = message });
            }
            catch (ApiAccessDeniedException e)
            {
                logger.Error(e);

                statusCode = 401;
                contentType = ContentType.ApplicationJson;
                string message = propagateApplicationErrorInFault ? e.Message : Messages.GenericFailure;
                content = JsonHelper.Serialize(new { Message = message });
            }
            catch (MissingRequiredProperty e)
            {
                logger.Error(e);

                statusCode = 400;
                contentType = ContentType.ApplicationJson;
                string message = propagateApplicationErrorInFault ? e.Message : Messages.GenericFailure;
                content = JsonHelper.Serialize(new { Message = message });
            }
            catch (CustomJsonException e)
            {
                logger.Error(e);

                statusCode = 502; // TODO check this code
                contentType = ContentType.ApplicationJson;
                string message = propagateApplicationErrorInFault ? e.Message : Messages.GenericFailure;
                content = JsonHelper.Serialize(new { Message = message });
            }
            catch (SecurityException e)
            {
                logger.Error(e);

                statusCode = 403; // TODO check this code
                contentType = ContentType.ApplicationJson;
                string message = propagateApplicationErrorInFault ? e.Message : Messages.GenericFailure;
                content = JsonHelper.Serialize(new { Message = message });
            }
            catch (Exception e)
            {
                logger.Error(e);

                statusCode = 500;
                contentType = ContentType.ApplicationJson;
                string message = propagateApplicationErrorInFault ? e.Message : Messages.GenericFailure;
                content = JsonHelper.Serialize(new { Message = message });
            }
            finally
            {
                logger.Debug($"ContentType: {contentType}");
                logger.Debug($"Content: {content}");
                logger.Info($"statusCode: {statusCode}");

                context.Response.ContentType = contentType;
                context.Response.StatusCode = statusCode;
                context.Session = session;
                await context.Response.WriteAsync(content);

                logger.Info($"Request completed in {timeKeeper.Stop().TotalMilliseconds} ms");
            }
        }

        private ApiHandlerOutput Process(ApiInputHandler input)
        {
            string typeName = Business.Resource.GetType(input.Resource);

            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ApiNotFoundException(input.Resource);
            }

            bool canAccess = Business.Authorization.CanAccess(input.Resource, Context.Current.User);

            if (!canAccess)
            {
                logger.Warn($"Access denied for resource {input.Resource}");

                throw new SecurityException(Messages.AccessDenied);
            }

            if (Business.Authorization.RequireToken(input.Resource))
            {
                string token = input.Context.Request.Headers["ZestyApiToken"];

                if (string.IsNullOrWhiteSpace(token))
                {
                    token = input.Context.Request.Query["t"];
                }

                if (!Business.Authorization.IsValid(Context.Current.User.Id, input.Context.Session.Id, token))
                {
                    logger.Warn($"Invalid token for resource {input.Resource}");

                    throw new SecurityException(Messages.TokenMissing);
                }
            }

            ApiCacheItem cacheItem = ApiCache.Get(input);

            if (cacheItem != null)
            {
                logger.Info($"Output found in cache for request {input.Resource}");

                return cacheItem.Output;
            }
            else
            {
                ApiHandlerBase handler = InstanceHelper.Create<ApiHandlerBase>(typeName);

                ApiHandlerOutput output = handler.Process(input);

                if (output.CachePolicy == ApiCachePolicy.Enable)
                {
                    cacheItem = new ApiCacheItem
                    {
                        Created = DateTime.Now,
                        Output = output,
                        Payload = input.Body,
                        Resource = input.Resource
                    };

                    ApiCache.Store(cacheItem);

                    logger.Info($"Output stored in cache for request {input.Resource}");
                }


                if (output.ResourceHistoryOutput != null && output.ResourceHistoryOutput.ResourceHistoryPolicy == ApiResourceHistoryPolicy.Save)
                {
                    Business.History.Save(output.ResourceHistoryOutput.Item);
                }

                return output;
            }
        }
    }
}