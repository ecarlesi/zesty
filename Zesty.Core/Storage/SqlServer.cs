using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core.Storage
{
    public class SqlServer : IStorage
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public List<Resource> GetResources(string username, string domainName)
        {
            string statement = @"GetResources";

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@username", Value = username });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@domainName", Value = domainName });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Resource> list = new List<Resource>();

                        while (reader.Read())
                        {
                            Resource resource = new Resource();

                            resource.Id = reader.Get<Guid>("Id");
                            resource.ParentId = reader.Get<Guid>("ParentId");
                            resource.Image = reader.Get<string>("Image");
                            resource.Label = reader.Get<string>("Label");
                            resource.Title = reader.Get<string>("Title");
                            resource.Url = reader.Get<string>("Url");
                            resource.IsPublic = reader.Get<bool>("IsPublic");
                            resource.RequireToken = reader.Get<bool>("RequireToken");

                            list.Add(resource);
                        }

                        return list;
                    }
                }
            }
        }

        public bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            string statement = @"ChangePassword";

            string currentHash = HashHelper.GetSha256(currentPassword);
            string newHash = HashHelper.GetSha256(newPassword);

#if DEBUG
            logger.Info($"Password \"{currentPassword}\" become \"{currentHash}\"");
            logger.Info($"Password \"{newPassword}\" become \"{newHash}\"");
#endif

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@username", Value = username });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@currentPassword", Value = currentHash });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@newPassword", Value = newHash });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string status = reader.Get<string>("Status");

                            return status == "OK" ? true : false;
                        }

                        return false;
                    }
                }
            }
        }

        public List<Domain> GetDomains(string username)
        {
            string statement = @"GetDomains";

            List<Domain> list = new List<Domain>();

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@username", Value = username });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Domain domain = new Domain();

                            domain.Name = reader.Get<string>("Name");
                            domain.Id = reader.Get<Guid>("Id");
                            domain.ParentDomainId = reader.Get<Guid>("ParentDomainId");

                            list.Add(domain);
                        }

                        return list;
                    }
                }
            }
        }

        public List<string> GetRoles(string username, string domain)
        {
            string statement = @"GetRoles";

            List<string> list = new List<string>();

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@username", Value = username });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@domain", Value = domain });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.Get<string>("Name"));
                        }

                        return list;
                    }
                }
            }
        }

        public bool CanAccess(string path, User user)
        {
            string statement = @"CanAccess";

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@path", Value = path });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@userid", Value = user.Id });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        public string GetType(string resourceName)
        {
            string statement = @"GetResourceType";

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@resourceName", Value = resourceName });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.Get<string>("Type");
                        }

                        return null;
                    }
                }
            }
        }

        public bool IsPublicResource(string path)
        {
            string statement = @"IsPublicResource";

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@path", Value = path });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string s = reader.Get<string>("IsPublic");

                            return s == "Y" ? true : false;
                        }

                        return false;
                    }
                }
            }
        }

        public bool IsValid(Guid userId, string sessionId, string tokenValue)
        {
            string statement = @"IsValid";

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@userid", Value = userId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@sessionid", Value = sessionId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@token", Value = tokenValue });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string s = reader.Get<string>("IsValid");

                            return s == "Y" ? true : false;
                        }

                        return false;
                    }
                }
            }
        }

        public Dictionary<string, string> LoadProperties(User user)
        {
            string statement = @"GetProperties";

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@userid", Value = user.Id });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Dictionary<string, string> props = new Dictionary<string, string>();

                        while (reader.Read())
                        {
                            string k = reader.Get<string>("Key");
                            string v = reader.Get<string>("Value");

                            props.Add(k, v);
                        }

                        return props;
                    }
                }
            }
        }

        public User Login(string username, string password)
        {
            string statement = @"Login";

            string hash = HashHelper.GetSha256(password);

#if DEBUG
            logger.Info($"Password \"{password}\" become \"{hash}\"");
#endif

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@username", Value = username });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@password", Value = hash });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        User user = null;

                        if (reader.Read())
                        {
                            user = new User();

                            user.Id = reader.Get<Guid>("Id");
                            user.Username = reader.Get<string>("Username");
                            user.Email = reader.Get<string>("Email");
                            user.Firstname = reader.Get<string>("Firstname");
                            user.Lastname = reader.Get<string>("Lastname");
                            user.PasswordChanged = reader.Get<DateTime>("Created");
                        }

                        return user;
                    }
                }
            }
        }

        public bool RequireToken(string path)
        {
            string statement = @"RequireToken";

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@path", Value = path });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string s = reader.Get<string>("IsRequired");

                            return s == "Y" ? true : false;
                        }

                        return false;
                    }
                }
            }
        }

        public void Save(HistoryItem resource)
        {
            string statement = @"SaveHistory";

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@userid", Value = resource.User == null ? Guid.Empty : resource.User.Id });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@resource", Value = resource.Resource });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@actor", Value = resource.Actor });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@text", Value = resource.Text });

                    command.ExecuteNonQuery();
                }
            }
        }

        public void SaveToken(User user, string sessionId, string tokenValue, bool reusable)
        {
            string statement = @"SaveToken";

            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@userid", Value = user.Id });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@sessionid", Value = sessionId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@value", Value = tokenValue });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@reusable", Value = reusable });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
