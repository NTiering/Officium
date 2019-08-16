# Officium
Framework for allow rapid development of Azure Function

## Overview 

Officium is a framework to support radpid development of azure functions. 

## Feature list

* Support ALL http Methods (GET,HEAD,POST,PUT,DELETE,CONNECT,OPTIONSTRACE,PATCH) 
* Request Routing
* Validation
* Dependency Injection / IoC
* Error Handling
* Query, Pody (POST'ed) and URL path params supported
* Handle before and after every request
* Header Parameters
* Unhandled Requests
* Auth and Identity
---
## Getting Started 
Add handling to your azure function in a few lines of code.

Start a new azure function project

Add the following Nuget pakages 
* Microsoft.Azure.Functions.Extensions
* Microsoft.Extensions.DependencyInjection
* Officium.Tools

Add a new file **Startup.cs**
```
// Startup.cs
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._1Examples.Startup))]
namespace Officium._1Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {  
            using (var b = new Builder(builder.Services))
            {
                b.OnRequest<HelloWorldHandler>(
                    RequestMethod.GET,
                    "/api/HelloWorld");
            }

            builder.Services.AddHttpClient();
        }
    }
}
```
this sets up Dependency injection, and addes a handler called *HelloworldHandler* to any GET request on */api/Helloworld*  


Edit the file **Function1.cs**
```
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Helpers;

namespace Officium._1Examples
{
    public class HelloWorldFunction
    {
        private readonly IRequestResolver requestResolver;
        public HelloWorldFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("HelloWorld")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post", 
            Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Hello world function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class HelloWorldHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.Result = new { Message = "Hello " + request.GetValue("name").WithDefault("Stranger ! add a name param to the url") };
        }
    }
}

```
Note we inject an instance of IRequestResolver, which was built by the builder in **Startup.cs**
the lines 
```
var reqContext = req.MakeRequestContext();
var resContext = requestResolver.Execute(reqContext);
return resContext.GetActionResult();
```
Create a request context, process that request and return the result.


Start your app and point a browser at your endpoint (E.G http://localhost:7071/api/HelloWorld ) and you should see the message 

*{"message":"Hello Stranger ! add a name param to the url"}*

Add a query parameter of name (E.G http://localhost:7071/api/HelloWorld?Name=Timmy ) and you should see the message 

*{"message":"Hello Timmy"}*


**IMPORTANT CONCEPTS**

Notice that the actual message is built in the handler, which you have full control over. The request, responce and action result are all already constructed and loaded. 

As the result is an object, it can be any type you wish to return.

In the next chapter, we'll look at firing handlers before and after every request 

## Before and After Every Request
Handlers can be set up to intercept every request either before or after any other handlers

Edit **Startup.cs**
```
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._2Examples.Startup))]
namespace Officium._2Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {  
            using (var b = new Builder(builder.Services))
            {
                b.OnRequest<HelloWorldHandler>(
                    RequestMethod.GET,
                    "/api/BeforeAndAfterEveryRequest"); 

                b.BeforeEveryRequest<BeforeHandler>();
                b.AfterEveryRequest<AfterHandler>();
            }

            builder.Services.AddHttpClient();
        }
    }
}
```
Notice these lines 
```
b.BeforeEveryRequest<BeforeHandler>();
b.AfterEveryRequest<AfterHandler>();
```
We instruct the framework to call BeforeHandler.HandleRequest(...) before each request, and AfterHandler..HandleRequest(...)
after every request (Exceptions may stop the after every handlers from being called, this is covered later).


Edit **Function1.cs**
```
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Helpers;

namespace Officium._2Examples
{
    public class BeforeAndAfterEveryRequest
    {
        private readonly IRequestResolver requestResolver;
        public BeforeAndAfterEveryRequest(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("BeforeAndAfterEveryRequest")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post", 
            Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing BeforeAndAfterEveryRequest function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class BeforeHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.Result = new HandlerResult { BeforeMessage = "Before Hello " + request.GetValue("name").WithDefault("Stranger ! add a name param to the url") };
        }
    }
    public class HelloWorldHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            ((HandlerResult)response.Result).Message = "Hello " + request.GetValue("name").WithDefault("Stranger ! add a name param to the url");
        }
    }
    public class AfterHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            ((HandlerResult)response.Result).AfterMessage = "After Hello " + request.GetValue("name").WithDefault("Stranger ! add a name param to the url");
        }
    }

    /// <summary>
    /// Used to contain an example result
    /// </summary>
    public class HandlerResult
    {
        public string BeforeMessage { get; set; }
        public string Message { get; set; }
        public string AfterMessage { get; set; }
    }
}
```
Now start your app and point a browser at your endpoint (/api/BeforeAndAfterEveryRequest) and you should get the message 

*{"beforeMessage":"Before Hello Stranger ! add a name param to the url",
"message":"Hello Stranger ! add a name param to the url",
"afterMessage":"After Hello Stranger ! add a name param to the url"}*

**IMPORTANT CONCEPTS**

All requests can be routed into common handlers, multiple before and after handlers can be set up to handle, for instance. logging
and authentication.

## Validation
Validation allows automated rules to stop request from entering some handlers 

Edit **Startup.cs**
```
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._1aExamples.Startup))]
namespace Officium._1aExamples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {  
            using (var b = new Builder(builder.Services))
            {
                b.ValidateRequest<ValidatorHandler>(
                    RequestMethod.GET,
                    "/api/Validation");

                b.OnRequest<HelloWorldHandler>(
                    RequestMethod.GET,
                    "/api/Validation");
            }

            builder.Services.AddHttpClient();
        }
    }
}

```
Edit **Function1.cs**
```
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;

namespace Officium._1aExamples
{
    public class HelloWorldFunction
    {
        private readonly IRequestResolver requestResolver;
        public HelloWorldFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("Validation")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post", 
            Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Validation function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class ValidatorHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            if (string.IsNullOrWhiteSpace(request.GetValue("name")))
            {
                response.ValidationErrors.Add(new ValidationError("name", "Please supply a value for name"));
            }
        }
    }

    public class HelloWorldHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.Result = new { Message = "Hello " + request.GetValue("name") };
        }
    }
}

```
Now start your app and point a browser at your endpoint (/api/Validation) and you should get the message 

*[{"propertyName":"name","errorMessage":"Please supply a value for name"}]*

If you add a breakpoint to HelloWorldHandler HandleRequest, you will see it is not called.

Modify the browser url to something like /api/Validation?name=Timmy and you should see the message 

{"message":"Hello Timmy"}

**IMPORTANT CONCEPTS**

Validators can be specified for each request, and these are fired before the main handler. If they add any validation errors 
then the request on that path WILL NOT BE CALLED. before and after handlers are unaffected.

 

## Variables 
Variables can be accessed in any handlers from the request context 

Edit **Startup.cs**
```

```
Edit **Function1.cs**
```

```
## Dependency Injection / IoC
Dependency injection using the existing IoC framework is supported 
```

```

## Validation 
Requests can be validated prior to to being routed to the handler. Validation errors are automatically returned as an action context
```

```


## Error Handling
Errors can be routed to a specified handler, which can be used for logging etc
```

```

## 'No Handler' handler
Requests that have no defined handler can be routed to a dedicated handler
```

```
## Authentication
Authentication is handled using existing Claims Pricipals
```

```
