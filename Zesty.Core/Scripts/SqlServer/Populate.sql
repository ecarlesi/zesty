USE [Zesty]

delete from [dbo].[ResourceType]
delete from [dbo].[UserPassword];
delete from [dbo].[UserProperties];
delete from [dbo].[ResourceRole];
delete from [dbo].[Authorization];
delete from [dbo].[Domain];
delete from [dbo].[Role];
delete from [dbo].[User];
delete from [dbo].[Resource];
delete from [dbo].[Token];
delete from [dbo].[Translation];
delete from [dbo].[Language];
delete from [dbo].[ClientSetting];

insert into [dbo].[ClientSetting] ([Key], [Value]) VALUES ('regex.password','^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$');

insert into [dbo].[Language] ([Name],[Description],[Direction]) values ('en', 'English', 'R');
insert into [dbo].[Language] ([Name],[Description],[Direction]) values ('it', 'Italiano', 'R');

INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'en'), 'Username', 'Username');
INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'en'), 'Access denied', 'Access denied');
INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'en'), 'Invalid credentials', 'Invalid credentials');
INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'en'), 'Password reset token: {0}', 'Password reset token: {0}');
INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'en'), 'Reset password', 'Reset password');

INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'it'), 'Username', 'Nome utente');
INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'it'), 'Access denied', 'Accesso negato');
INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'it'), 'Invalid credentials', 'Nome utente o password errati');
INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'it'), 'Password reset token: {0}', 'Token per il reset della password: {0}');
INSERT INTO [dbo].[Translation] ([LanguageId],[Original],[Translated]) VALUES ((select [Id] from [Language] where [Name] = 'it'), 'Reset password', 'Password reset');

INSERT INTO [dbo].[User] ([Id],[Username],[Email],[Firstname],[Lastname],[ResetToken],[Deleted],[Created]) VALUES (newid(),'eca','emiliano.carlesi@gmail.com','Emiliano','Carlesi',null,null,getdate());
INSERT INTO [dbo].[User] ([Id],[Username],[Email],[Firstname],[Lastname],[ResetToken],[Deleted],[Created]) VALUES (newid(),'leonardo','leotaglio@gmail.com','Leonardo','Taglienti',null,null,getdate());
INSERT INTO [dbo].[User] ([Id],[Username],[Email],[Firstname],[Lastname],[ResetToken],[Deleted],[Created]) VALUES (newid(),'andrea','andrea.sena.ws@gmail.com','Andrea','Sena',null,null,getdate());

INSERT INTO [dbo].[Role] ([Id],[Name])VALUES (newid(),'Administrators');
INSERT INTO [dbo].[Role] ([Id],[Name])VALUES (newid(),'Users');

INSERT INTO [dbo].[Domain] ([Id],[Name])VALUES (newid(),'Domain A');
INSERT INTO [dbo].[Domain] ([Id],[Name])VALUES (newid(),'Domain B');
INSERT INTO [dbo].[Domain] ([Id],[ParentDomainId],[Name])VALUES (newid(),(select [id] from [domain] where [name] = 'Domain A'), 'Domain X');
INSERT INTO [dbo].[Domain] ([Id],[ParentDomainId],[Name])VALUES (newid(),(select [id] from [domain] where [name] = 'Domain X'), 'Domain Y');

INSERT INTO [dbo].[Authorization] ([UserId],[DomainId],[RoleId]) VALUES ((select id from [user] where [username] = 'eca'),(select id from [domain] where [name] = 'Domain A'),(select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[Authorization] ([UserId],[DomainId],[RoleId]) VALUES ((select id from [user] where [username] = 'leonardo'),(select id from [domain] where [name] = 'Domain A'),(select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[Authorization] ([UserId],[DomainId],[RoleId]) VALUES ((select id from [user] where [username] = 'andrea'),(select id from [domain] where [name] = 'Domain A'),(select id from [role] where [name] = 'Administrators'));

INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/Secured/Hello',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/sample.private.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/sample.free.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.login.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.logout.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.check.api',0,1);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.token.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.domains.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.resources.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.clientsettings.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.roles.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.domain.api',0,1);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.info.api',0,1);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.languages.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.translations.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.userresettoken.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.resetpassword.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.setresettoken.api',1,0);

INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/Secured/Hello'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/sample.private.api'), (select id from [role] where [name] = 'Administrators'));

-- INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/sample.free.api'), (select id from [role] where [name] = 'Administrators'));
-- INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.login.api'), (select id from [role] where [name] = 'Administrators'));
-- INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.logout.api'), (select id from [role] where [name] = 'Administrators'));

INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.check.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.token.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.domains.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.resources.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.roles.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.domain.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.info.api'), (select id from [role] where [name] = 'Administrators'));

INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/sample.private.api'),'Zesty.Core.Api.Sample.Private, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/sample.free.api'),'Zesty.Core.Api.Sample.Free, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.login.api'),'Zesty.Core.Api.System.Login, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.logout.api'),'Zesty.Core.Api.System.Logout, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.check.api'),'Zesty.Core.Api.System.Check, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.token.api'),'Zesty.Core.Api.System.Token, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.domains.api'),'Zesty.Core.Api.System.Domains, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.roles.api'),'Zesty.Core.Api.System.Roles, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.domain.api'),'Zesty.Core.Api.System.Domain, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.resources.api'),'Zesty.Core.Api.System.Resources, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.info.api'),'Zesty.Core.Api.System.Info, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.languages.api'),'Zesty.Core.Api.System.Languages, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.translations.api'),'Zesty.Core.Api.System.Translations, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.userresettoken.api'),'Zesty.Core.Api.System.UserByResetToken, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.resetpassword.api'),'Zesty.Core.Api.System.ResetPassword, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.setresettoken.api'),'Zesty.Core.Api.System.SetResetToken, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.clientsettings.api'),'Zesty.Core.Api.System.ClientSettings, Zesty.Core');

INSERT INTO [dbo].[UserPassword] ([Id],[UserId],[Password],[Deleted],[Created]) VALUES (newid(),(select id from [user] where [username] = 'eca'),'5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8',null,getdate());
INSERT INTO [dbo].[UserPassword] ([Id],[UserId],[Password],[Deleted],[Created]) VALUES (newid(),(select id from [user] where [username] = 'leonardo'),'5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8',null,getdate());
INSERT INTO [dbo].[UserPassword] ([Id],[UserId],[Password],[Deleted],[Created]) VALUES (newid(),(select id from [user] where [username] = 'andrea'),'5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8',null,getdate());

INSERT INTO [dbo].[UserProperties] ([UserId],[Key],[Value]) VALUES ((select id from [user] where [username] = 'eca'),'Prop 1','Value 1');
INSERT INTO [dbo].[UserProperties] ([UserId],[Key],[Value]) VALUES ((select id from [user] where [username] = 'eca'),'Prop 2','Value 2');



