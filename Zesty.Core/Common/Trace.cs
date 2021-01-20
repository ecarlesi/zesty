using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Zesty.Core.Entities;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core.Common
{
    public class Trace
    {
        public static void Write(TraceItem traceItem, HttpContext httpContext = null)
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();

            string className = methodBase.ReflectedType.FullName;
            string methodName = methodBase.Name;
            string username = null;
            string domain = null;
            string sessionId = null;
            string resource = null;
            string clientIp = null;
            string hostname = null;

            if (Context.Current != null && Context.Current.User != null)
            {
                username = Context.Current.User.Username;

                if (Context.Current.User.Domain != null)
                {
                    domain = Context.Current.User.Domain.Name;
                }
            }

            if (httpContext != null)
            {
                resource = httpContext.Request.Path;
                sessionId = httpContext.Session.Id;
                clientIp = httpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
                hostname = httpContext.Request.Host.Value;
            }

            using (SqlConnection connection = new SqlConnection(Settings.Current.StorageSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("Zesty_Trace", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@className", Value = className });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@method", Value = methodName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@hostname", Value = hostname ?? (Object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@clientip", Value = clientIp ?? (Object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@sessionid", Value = sessionId ?? (Object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@username", Value = username ?? (Object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@domain", Value = domain ?? (Object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@resource", Value = resource ?? (Object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@message", Value = traceItem.Message ?? (Object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@error", Value = traceItem.Error ?? (Object)DBNull.Value });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@millis", Value = traceItem.Millis });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
