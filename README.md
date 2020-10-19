# Description #

**Zesty** was born with the aim of simplifying the development of web applications, allowing to adopt some practices that improve development and operations.

The **Zesty** solution contains the **Zesty.Core** project (the framework) and **Zesty.Web** (a sample project).

To work **Zesty** needs a storage. The default storage is based on SQL Server, in case you prefer to use another technology to save the information, just implement the **Zesty.Core.IStorage** interface and configure the key *StorageType* in **appsettings.json**.

Inside the **Zesty.Core.Scripts** directory there are two SQL scripts, **Create.sql** (contains the commands to create objects) and **Populate.sql** (creates the contents needed to run the **Zesty.Web** project).

## Usage ##

To create a Zesty-based project, you need to create a .NET Core Web Application (MVC) project and add a reference to **Zesty.Core**.

Add the following section in the **appsettings.json**.

Be aware to edit the settings with your environment values.

```json
"Zesty": {
    "StorageImplementationType": "Zesty.Core.Storage.SqlServer, Zesty.Core",
    "StorageSource": "Data Source = 192.168.1.222; Initial Catalog = Zesty; User Id = zestyUser; Password = zesty.Password."
}

```

Add a file named **nlog.config**, set the build type as *Content*, set *Copy to output directory* and paste the following contents. 

Be aware to edit the settings with your environment values.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      autoReload="true">

  <extensions>
    <add assembly="NLog.Web.AspNetCore"/>
  </extensions>

  <targets>
    <target 
            xsi:type="File" 
            name="logger" 
            fileName="/Users/eca/logs/zesty-web-${shortdate}.log"
            layout="${longdate} ${threadid} ${uppercase:${level}} ${logger} ${message} ${exception:format=tostring}" />
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="logger" />
  </rules>
</nlog>
```

In the **Startup.cs** file add this *using*

```c#
using Zesty.Core.Common;
```

In the **Startup.cs** file in the method *ConfigureServices* add this line of code
```c#
services.AddZesty();
```

In the **Startup.cs** file in the method *Configure* add this line of code
```c#
app.UseZesty();
```

## TODO ##
