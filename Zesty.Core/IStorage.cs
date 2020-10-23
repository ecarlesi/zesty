using System;
using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core
{
    public interface IStorage
    {
        List<SettingValue> GetSettingsValues();
        void SetProperty(string name, string value, Entities.User user);
        Dictionary<string, string> GetClientSettings();
        Guid SetResetToken(string email);
        bool ResetPassword(Guid resetToken, string password);
        User GetUserByResetToken(Guid resetToken);
        List<Translation> GetTranslations(string language);
        List<Language> GetLanguages();
        List<Resource> GetResources(string username, Guid domainId);
        void SaveToken(Entities.User user, string sessionId, string tokenValue, bool reusable);
        bool CanAccess(string path, Entities.User user);
        bool IsValid(Guid userId, string sessionId, string tokenValue);
        bool RequireToken(string path);
        bool IsPublicResource(string path);
        void Save(Entities.HistoryItem resource);
        string GetType(string resourceName);
        Entities.User Login(string username, string password);
        Dictionary<string, string> LoadProperties(Entities.User user);
        List<Entities.Domain> GetDomains(string username);
        List<string> GetRoles(string username, Guid domainId);
        bool ChangePassword(string username, string currentPassword, string newPassword);
    }
}
