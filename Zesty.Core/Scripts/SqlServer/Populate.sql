use [Zesty]

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
delete from [dbo].[History];
delete from [dbo].[ServerSetting];

insert into [dbo].[ClientSetting] ([Key], [Value]) VALUES ('regex.password','^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$');

insert into [dbo].[Language] ([Id], [Name],[Description],[Direction]) values (1, 'en', 'English', 'R');
insert into [dbo].[Language] ([Id], [Name],[Description],[Direction]) values (2, 'it', 'Italiano', 'R');

INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (1, 1, 'Username', 'Username');
INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (2, 1, 'Access denied', 'Access denied');
INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (3, 1, 'Invalid credentials', 'Invalid credentials');
INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (4, 1, 'Password reset token: {0}', 'Password reset token: {0}');
INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (5, 1, 'Reset password', 'Reset password');

INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (6, 2, 'Username', 'Nome utente');
INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (7, 2, 'Access denied', 'Accesso negato');
INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (8, 2, 'Invalid credentials', 'Nome utente o password errati');
INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (9, 2, 'Password reset token: {0}', 'Token per il reset della password: {0}');
INSERT INTO [dbo].[Translation] ([Id], [LanguageId],[Original],[Translated]) VALUES (10, 2, 'Reset password', 'Password reset');

INSERT INTO [dbo].[User] ([Id],[Username],[Email],[Firstname],[Lastname],[ResetToken],[Deleted],[Created]) VALUES ('b81d0335-c175-4af8-89f1-ff4ccc79c666','eca','emiliano.carlesi@gmail.com','Emiliano','Carlesi',null,null,getdate());

INSERT INTO [dbo].[Role] ([Id],[Name])VALUES ('62ef76b8-e39e-41c7-86dc-4801642dc655','Administrators');
INSERT INTO [dbo].[Role] ([Id],[Name])VALUES ('9e73b89c-e645-4084-b925-742818275df5','Users');

INSERT INTO [dbo].[Domain] ([Id], [ParentDomainId], [Name])VALUES ('bc89e749-784b-479f-91e6-85708326558e', null, 'Domain A');
INSERT INTO [dbo].[Domain] ([Id], [ParentDomainId], [Name])VALUES ('4434a180-3c64-4402-bf72-5380773dc43d', null, 'Domain B');
INSERT INTO [dbo].[Domain] ([Id], [ParentDomainId], [Name])VALUES ('608a3cdf-96a3-4c48-870e-11213d0a15f8', 'bc89e749-784b-479f-91e6-85708326558e', 'Domain X');
INSERT INTO [dbo].[Domain] ([Id], [ParentDomainId], [Name])VALUES ('12add769-1d0e-4448-9da1-1f467cd835b5', '608a3cdf-96a3-4c48-870e-11213d0a15f8', 'Domain Y');

INSERT INTO [dbo].[Authorization] ([UserId],[DomainId],[RoleId]) VALUES ('b81d0335-c175-4af8-89f1-ff4ccc79c666','bc89e749-784b-479f-91e6-85708326558e','62ef76b8-e39e-41c7-86dc-4801642dc655');

INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('28757f88-c9f3-40e0-ab28-f073fef63522','/Secured/Hello',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('ba86180d-11db-4778-81ff-6e1f13a4b7a2','/sample.private.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('0eaba387-43b6-4b3d-b834-d042fa33f013','/sample.free.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('dbe43db7-c785-4992-8f3e-5f54c547b968','/system.init.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('47b04f8a-3f67-4939-9ea2-600c922f74a8','/system.login.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('236af387-234a-46be-a6c1-5eb2f761ef1f','/system.logout.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('1f3c9faa-468b-49b2-bb1c-fca8e74fe09b','/system.check.api',0,1);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('94724ee4-45fd-45ed-ba56-20e5771b96d1','/system.token.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('5940baea-f548-4116-ac39-f195dc03c6bd','/system.domains.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('98a8fada-b725-4011-a2fb-92c2cfeaa9d0','/system.resources.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('a4029d34-afef-49c9-89b3-cab33176a6a7','/system.clientsettings.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('f7a18444-1442-40e4-9b70-dbe14b5e573b','/system.roles.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('ceb38502-f544-4b9d-bce6-601deff48f7a','/system.domain.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('1457b879-8dbc-424d-974d-67f5abc08d97','/system.info.api',0,1);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('29fbe0b5-b46f-4f5d-b1a8-061c52e9defb','/system.languages.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('1dbbd895-f887-4df9-b4f0-c322d43d4f20','/system.translations.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('4378c266-21d2-4891-a59f-0ec4172371ad','/system.userresettoken.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('d707997a-5493-4e5c-b086-66f10e75a890','/system.resetpassword.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('bea3ee18-85c5-43ba-b969-a437cd4b4348','/system.setresettoken.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES ('01e9a142-e8eb-404a-b69f-04557fac2eb8','/system.property.api',0,0);

INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('28757f88-c9f3-40e0-ab28-f073fef63522', '62ef76b8-e39e-41c7-86dc-4801642dc655');
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('ba86180d-11db-4778-81ff-6e1f13a4b7a2', '62ef76b8-e39e-41c7-86dc-4801642dc655');
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('1f3c9faa-468b-49b2-bb1c-fca8e74fe09b', '62ef76b8-e39e-41c7-86dc-4801642dc655');
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('94724ee4-45fd-45ed-ba56-20e5771b96d1', '62ef76b8-e39e-41c7-86dc-4801642dc655');
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('5940baea-f548-4116-ac39-f195dc03c6bd', '62ef76b8-e39e-41c7-86dc-4801642dc655');
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('98a8fada-b725-4011-a2fb-92c2cfeaa9d0', '62ef76b8-e39e-41c7-86dc-4801642dc655');
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('f7a18444-1442-40e4-9b70-dbe14b5e573b', '62ef76b8-e39e-41c7-86dc-4801642dc655');
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('ceb38502-f544-4b9d-bce6-601deff48f7a', '62ef76b8-e39e-41c7-86dc-4801642dc655');
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('1457b879-8dbc-424d-974d-67f5abc08d97', '62ef76b8-e39e-41c7-86dc-4801642dc655');
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ('01e9a142-e8eb-404a-b69f-04557fac2eb8', '62ef76b8-e39e-41c7-86dc-4801642dc655');

INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('ba86180d-11db-4778-81ff-6e1f13a4b7a2','Zesty.Core.Api.Sample.Private, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('0eaba387-43b6-4b3d-b834-d042fa33f013','Zesty.Core.Api.Sample.Free, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('dbe43db7-c785-4992-8f3e-5f54c547b968','Zesty.Core.Api.System.Init, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('47b04f8a-3f67-4939-9ea2-600c922f74a8','Zesty.Core.Api.System.Login, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('236af387-234a-46be-a6c1-5eb2f761ef1f','Zesty.Core.Api.System.Logout, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('1f3c9faa-468b-49b2-bb1c-fca8e74fe09b','Zesty.Core.Api.System.Check, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('94724ee4-45fd-45ed-ba56-20e5771b96d1','Zesty.Core.Api.System.Token, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('5940baea-f548-4116-ac39-f195dc03c6bd','Zesty.Core.Api.System.Domains, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('f7a18444-1442-40e4-9b70-dbe14b5e573b','Zesty.Core.Api.System.Roles, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('ceb38502-f544-4b9d-bce6-601deff48f7a','Zesty.Core.Api.System.Domain, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('98a8fada-b725-4011-a2fb-92c2cfeaa9d0','Zesty.Core.Api.System.Resources, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('1457b879-8dbc-424d-974d-67f5abc08d97','Zesty.Core.Api.System.Info, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('29fbe0b5-b46f-4f5d-b1a8-061c52e9defb','Zesty.Core.Api.System.Languages, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('1dbbd895-f887-4df9-b4f0-c322d43d4f20','Zesty.Core.Api.System.Translations, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('4378c266-21d2-4891-a59f-0ec4172371ad','Zesty.Core.Api.System.UserByResetToken, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('d707997a-5493-4e5c-b086-66f10e75a890','Zesty.Core.Api.System.ResetPassword, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('bea3ee18-85c5-43ba-b969-a437cd4b4348','Zesty.Core.Api.System.SetResetToken, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('a4029d34-afef-49c9-89b3-cab33176a6a7','Zesty.Core.Api.System.ClientSettings, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ('01e9a142-e8eb-404a-b69f-04557fac2eb8','Zesty.Core.Api.System.Property, Zesty.Core');

INSERT INTO [dbo].[UserPassword] ([Id],[UserId],[Password],[Deleted],[Created]) VALUES ('1afeb587-1849-4044-a0cc-cbd2e2de8346', 'b81d0335-c175-4af8-89f1-ff4ccc79c666','5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8',null,getdate());

INSERT INTO [dbo].[UserProperties] ([UserId],[Key],[Value]) VALUES ('b81d0335-c175-4af8-89f1-ff4ccc79c666','Property 1','Value 1');
INSERT INTO [dbo].[UserProperties] ([UserId],[Key],[Value]) VALUES ('b81d0335-c175-4af8-89f1-ff4ccc79c666','Property 2','Value 2');

INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('c62a6f65-e324-4d9c-9e36-45a5f1bf5203', 1, 'ThrowsOnAccessDenied' , 'true');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('d84ad49f-89ff-4ff6-a56e-1ca47cc333fc', 2, 'RedirectPathOnAccessDenied' , '/');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('2b43d7bb-f54c-4130-8722-4c8b6ecc998e', 3, 'ThrowsOnAuthorizationFailed' , 'true');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('ed5f3407-c25b-4da9-99af-ebeed7bd3508', 4, 'CorsOrigins' , 'https://localhost:4200');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('b57bb065-f9fe-4b33-9227-7d35d83aadaf', 5, 'CorsOrigins' , 'https://localhost:1099');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('3823bd90-c229-48c3-8136-d1cd008aa2f6', 6, 'PasswordLifetimeInDays' , '10');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('815f81c9-104a-46f5-94f9-3e8eec9f32e7', 7, 'ApiCacheLifetimeInMinutes' , '15');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('f50c79f6-beca-4cbb-a929-94070913b007', 8, 'SessionLifetimeInMinutes' , '30');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('a724bc0a-55c8-4298-b98d-6c71a989b428', 9, 'UrlWhitelist' , '/Secured/Login');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('c0aeb6ff-2089-49a1-8bba-2c3d05d5f7bb', 10, 'UrlWhitelist' , '/Secured/Logout');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('e9a86ed4-a6b4-4928-b352-33de8710d914', 11, 'PreExecutionHandler' , 'Zesty.Core.Handlers.PreLogger, Zesty.Core');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('fdb9e236-9cf5-4acb-ad90-4d0dab64c419', 12, 'PostExecutionHandler' , 'Zesty.Core.Handlers.PostLogger, Zesty.Core');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('b442eb75-84fb-4e9a-bfcf-647337398046', 13, 'SmtpClient.Host' , 'smtp.office365.com');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('082ca1aa-e9ad-4cfb-9e93-2d18a72b6aa6', 14, 'SmtpClient.Port' , '587');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('a837b293-9a6b-4c81-8000-943d0f1fd22a', 15, 'SmtpClient.Ssl' , 'true');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('aea0bfe5-a600-49e6-9804-48d4ff8aeb4b', 16, 'SmtpClient.Username' , '');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('638dac90-fd75-435a-9b8c-bf9221fe027d', 17, 'SmtpClient.Password' , '');
INSERT INTO [dbo].[ServerSetting] ([Id], [Order], [Key] ,[Value]) VALUES ('0678d08c-ca66-4479-9d6b-2758d88f3536', 18, 'PropagateApplicationErrorInFault' , 'true');



