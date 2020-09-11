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

INSERT INTO [dbo].[User] ([Id],[Username],[Email],[Firstname],[Lastname],[ResetToken],[Deleted],[Created]) VALUES (newid(),'eca','emiliano.carlesi@gmail.com','Emiliano','Carlesi',null,null,getdate());

INSERT INTO [dbo].[Role] ([Id],[Name])VALUES (newid(),'Administrators');
INSERT INTO [dbo].[Role] ([Id],[Name])VALUES (newid(),'Users');

INSERT INTO [dbo].[Domain] ([Id],[Name])VALUES (newid(),'Domain A');
INSERT INTO [dbo].[Domain] ([Id],[Name])VALUES (newid(),'Domain B');

INSERT INTO [dbo].[Authorization] ([UserId],[DomainId],[RoleId]) VALUES ((select id from [user] where [username] = 'eca'),(select id from [domain] where [name] = 'Domain A'),(select id from [role] where [name] = 'Administrators'));

INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/Secured/Hello',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/private.api',0,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/free.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/login.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/logout.api',1,0);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/check.api',0,1);
INSERT INTO [dbo].[Resource] ([Id],[Url],[IsPublic],[RequireToken]) VALUES (newid(),'/token.api',0,0);

INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/Secured/Hello'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/private.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/free.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/login.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/logout.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/check.api'), (select id from [role] where [name] = 'Administrators'));
INSERT INTO [dbo].[ResourceRole] ([ResourceId],[RoleId]) VALUES ((select id from [Resource] where [Url] = '/token.api'), (select id from [role] where [name] = 'Administrators'));

INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/private.api'),'Zesty.Web.Api.Private, Zesty.Web');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/free.api'),'Zesty.Web.Api.Free, Zesty.Web');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/login.api'),'Zesty.Web.Api.Login, Zesty.Web');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/logout.api'),'Zesty.Web.Api.Logout, Zesty.Web');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/check.api'),'Zesty.Web.Api.Check, Zesty.Web');
INSERT INTO [dbo].[ResourceType] ([ResourceId],[Type]) VALUES ((select id from [Resource] where [Url] = '/token.api'),'Zesty.Web.Api.Token, Zesty.Web');

INSERT INTO [dbo].[UserPassword] ([Id],[UserId],[Password],[Deleted],[Created]) VALUES (newid(),(select id from [user] where [username] = 'eca'),'5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8',null,getdate());

INSERT INTO [dbo].[UserProperties] ([UserId],[Key],[Value]) VALUES ((select id from [user] where [username] = 'eca'),'Prop 1','Value 1');
INSERT INTO [dbo].[UserProperties] ([UserId],[Key],[Value]) VALUES ((select id from [user] where [username] = 'eca'),'Prop 2','Value 2');



