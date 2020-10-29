using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core
{
    public abstract class ApiHandlerBase
    {
        protected static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public abstract ApiHandlerOutput Process(ApiInputHandler input);

        protected string Get(ApiInputHandler input, string parameterName, bool required = true)
        {
            string s = input.Context.Request.Query[parameterName];

            if (string.IsNullOrWhiteSpace(s) && required)
            {
                throw new ApiInvalidArgumentException(parameterName);
            }

            return s;
        }

        protected ApiHandlerOutput GetOutput(object response, bool cache = false)
        {
            return new ApiHandlerOutput()
            {
                Output = response,
                Type = ApiHandlerOutputType.JSon,
                ResourceHistoryOutput = null,
                CachePolicy = cache ? ApiCachePolicy.Enable : ApiCachePolicy.Disable
            };
        }

        protected T GetEntity<T>(ApiInputHandler input, bool mandatory)
        {
            if (String.IsNullOrWhiteSpace(input.Body))
            {
                if (mandatory)
                {
                    ThrowInvalidArgument();
                }
                else
                {
                    return default;
                }
            }

            T r = JsonHelper.Deserialize<T>(input.Body);

            if (r == null && mandatory)
            {
                ThrowInvalidArgument();
            }

            return r;
        }

        protected T GetEntity<T>(ApiInputHandler input)
        {
            if (String.IsNullOrWhiteSpace(input.Body))
            {
                return default;
            }

            T t = JsonHelper.Deserialize<T>(input.Body);

            if (t == null)
            {
                throw new ApplicationException(Messages.RequestIsNull);
            }

            PropertyInfo[] props = t.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                IEnumerable<Attribute> attributes = prop.GetCustomAttributes();

                foreach (Attribute attribute in attributes)
                {
                    if (attribute is RequiredAttribute)
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            string s = prop.GetValue(t) as string;

                            if (string.IsNullOrWhiteSpace(s))
                            {
                                throw new MissingRequiredProperty(prop.Name);
                            }
                        }
                        else if (prop.PropertyType == typeof(IList) || prop.PropertyType == typeof(Array))
                        {
                            if (prop.GetValue(t) == null)
                            {
                                throw new MissingRequiredProperty(prop.Name);
                            }
                        }
                        else if (prop.PropertyType == typeof(IList) || prop.PropertyType == typeof(Array))
                        {
                            if (prop.GetValue(t) == null)
                            {
                                throw new MissingRequiredProperty(prop.Name);
                            }
                        }
                        else if (prop.PropertyType == typeof(object))
                        {
                            if (prop.GetValue(t) == null)
                            {
                                throw new MissingRequiredProperty(prop.Name);
                            }
                        }
                        else if (prop.PropertyType == typeof(Guid))
                        {
                            //TODO fix
                            //string s = prop.GetValue(t) as string;

                            //if (string.IsNullOrWhiteSpace(s))
                            //{
                            //    throw new MissingRequiredProperty(prop.Name);
                            //}

                            //Guid g = Guid.Parse(s);

                            //if (g == Guid.Empty)
                            //{
                            //    throw new MissingRequiredProperty(prop.Name);
                            //}
                        }
                    }
                }
            }

            return t;
        }

        protected string Serialize(Object obj)
        {
            return JsonHelper.Serialize(obj);
        }

        public void IsNotNull<T>(T parameter, string name)
        {
            IsNotEmptyString(name, "name");

            if (parameter == null)
            {
                throw new ApiInvalidArgumentException(name);
            }
        }

        public void IsNotEmptyString(string parameter, string name)
        {
            if (String.IsNullOrWhiteSpace(parameter))
            {
                throw new ApiInvalidArgumentException(name);
            }
        }

        public void IsNotDefault<T>(T paramenter, string name) where T : class
        {
            T a = default(T);

            if (paramenter == a)
            {
                throw new ApiInvalidArgumentException(name);
            }
        }

        public void IsNotEmpty(Guid parameter, string name)
        {
            if (parameter == Guid.Empty)
            {
                throw new ApiInvalidArgumentException(name);
            }
        }

        protected void ThrowCustomJson(string json)
        {
            throw new CustomJsonException(json);
        }

        protected void ThrowInvalidArgument()
        {
            throw new ApiInvalidArgumentException(Messages.ArgumentNotFound);
        }

        protected void ThrowInvalidArgument(string message)
        {
            throw new ApiInvalidArgumentException(message);
        }

        protected void ThrowAccessDenied(string message)
        {
            throw new ApiAccessDeniedException(message);
        }

        protected void ThrowAccessDenied()
        {
            throw new ApiAccessDeniedException(Messages.AccessDenied);
        }

        protected void ThrowNotFound()
        {
            throw new ApiNotFoundException(Messages.ObjectNotFound);
        }

        protected void ThrowNotFound(string message)
        {
            throw new ApiNotFoundException(message);
        }

        protected void ThrowApplicationError(string message)
        {
            throw new ApiApplicationErrorException(message);
        }
    }
}
