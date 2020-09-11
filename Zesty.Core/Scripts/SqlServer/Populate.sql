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

INSERT INTO [dbo].[User] ([Id],[Username],[Email],[Firstname],[Lastname],[ResetToken],[Deleted],[Created]) VALUES (newid(),'eca','emiliano.carlesi@gmail.com','Emiliano','Carlesi',null,null,getdate());

INSERT INTO [dbo].[Role] ([Id],[Name])VALUES (newid(),'Administrators');
INSERT INTO [dbo].[Role] ([Id],[Name])VALUES (newid(),'Users');

INSERT INTO [dbo].[Domain] ([Id],[Name])VALUES (newid(),'Domain A');
INSERT INTO [dbo].[Domain] ([Id],[Name])VALUES (newid(),'Domain B');

INSERT INTO [dbo].[Authorization] ([UserId],[DomainId],[RoleId]) VALUES ((select id from [user] where [username] = 'eca'),(select id from [domain] where [name] = 'Domain A'),(select id from [role] where [name] = 'Administrators'));

INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/Secured/Hello',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/sample.private.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/sample.free.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.login.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.logout.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.check.api',0,1);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/system.token.api',0,0);

INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/Secured/Hello'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/sample.private.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/sample.free.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.login.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.logout.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.check.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/system.token.api'), (select id from [role] where [name] = 'Administrators'));

INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/sample.private.api'),'Zesty.Core.Api.Sample.Private, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/sample.free.api'),'Zesty.Core.Api.Sample.Free, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.login.api'),'Zesty.Core.Api.System.Login, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.logout.api'),'Zesty.Core.Api.System.Logout, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.check.api'),'Zesty.Core.Api.System.Check, Zesty.Core');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/system.token.api'),'Zesty.Core.Api.System.Token, Zesty.Core');

INSERT INTO [dbo].[UserPassword] ([Id],[UserId],[Password],[Deleted],[Created]) VALUES (newid(),(select id from [user] where [username] = 'eca'),'5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8',null,getdate());

INSERT INTO [dbo].[UserProperties] ([UserId],[Key],[Value]) VALUES ((select id from [user] where [username] = 'eca'),'Prop 1','Value 1');
INSERT INTO [dbo].[UserProperties] ([UserId],[Key],[Value]) VALUES ((select id from [user] where [username] = 'eca'),'Prop 2','Value 2');



