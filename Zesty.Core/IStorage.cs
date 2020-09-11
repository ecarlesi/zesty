using System;
using System.Collections.Generic;

namespace Zesty.Core
{
    public interface IStorage
    {
        void SaveToken(Entities.User user, string sessionId, string tokenValue, bool reusable);
        bool CanAccess(string path, Entities.User user);
        bool IsValid(Guid userId, string sessionId, string tokenValue);
        bool RequireToken(string path);
        bool IsPublicResource(string path);
        void Save(Entities.HistoryItem resource);
        string GetType(string resourceName);
        Entities.User Login(string username, string domain, string password);
        Dictionary<string, string> LoadProperties(Entities.User user);
    }
}
