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
Form, query and path variables can be accessed in any handlers from the request context 

**Form variables**

These values are come from a http form, usually (but not always) from a HTTP Post or Put 

**Query variables**

These variables come from the end of a url , after a question mark (?) E.G. name=Timmy

**Path variables**

These variables form part of the url E.G /api/students/{id}

**Internal variables**

These are a way to pass values between handlers



Edit **Startup.cs**
```
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._3Examples.Startup))]
namespace Officium._3Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {  
            using (var b = new Builder(builder.Services))
            {
                b.BeforeEveryRequest<PreVariablesHandler>();

                b.OnRequest<VariablesHandler>(
                    RequestMethod.GET,
                    "/api/Variables/{somename}");
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
using Officium.Tools.Helpers;

namespace Officium._3Examples
{
    public class VariablesFunction
    {
        private readonly IRequestResolver requestResolver;
        public VariablesFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("Variables")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "Variables/{n?}")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Variables function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class PreVariablesHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            request.SetInternalValue("Greeting", "Hello");
        }
    }

    public class VariablesHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.Result =
                new
                { 
                    Id = request.Id,
                    Greeting = request.GetInternalValue("greeting"),
                    Name = request.GetValue("somename")
                };
        }
    }
}

```
Now start yout project and point a browser to your endpoint 
(somethnig like http://localhost:7071/api/Variables/Timmy)

and you should see something like 

{"id":"56be7acd-2cf4-42eb-934c-241d59316232","greeting":"Hello","name":"Timmy"}

**IMPORTANT CONCEPTS**

You can get values using **request.GetValue("SomeValue")** if a value is not present , an empty string is returned.

Values can be passed from one handler to another using 
**request.SetInternalValue("Greeting", "Hello")** and **request.GetInternalValue("Greeting")** 
Internal values cannot be passed in from outside of the serverless function.

## Dependency Injection / IoC
Dependency injection using the existing IoC framework is supported.  

Edit **Startup.cs**
```
using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._4Examples.Startup))]
namespace Officium._4Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {  
            using (var b = new Builder(builder.Services))
            {
                b.BeforeEveryRequest<PreIoCHandler>();

                b.OnRequest<IoCHandler>(
                    RequestMethod.GET,
                    "/api/IoC/");
            }

            // add our service here 
            builder.Services.AddSingleton<ITextProvider, TextProvider>();
            builder.Services.AddHttpClient();
        }
    }

    public interface ITextProvider
    {
        string GetGreeting();
        string GetName();
    }

    public class TextProvider : ITextProvider
    {
        public string GetGreeting()
        {
            return "Hello";
        }

        public string GetName()
        {
            return "Timmy";
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
using Officium.Tools.Helpers;

namespace Officium._4Examples
{
    public class IoCFunction
    {
        private readonly IRequestResolver requestResolver;
        public IoCFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("IoC")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "IoC/")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing IoC function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class PreIoCHandler : IHandler
    {
        private readonly ITextProvider textProvider;

        public PreIoCHandler(ITextProvider textProvider)
        {
            this.textProvider = textProvider;
        }
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            request.SetInternalValue("Greeting", textProvider.GetGreeting() );
        }
    }

    public class IoCHandler : IHandler
    {
        private readonly ITextProvider textProvider;
        public IoCHandler(ITextProvider textProvider)
        {
            this.textProvider = textProvider;
        }
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.Result =
                new
                { 
                    Id = request.Id,
                    Greeting = request.GetValue("greeting"),
                    Name = textProvider.GetName()
                };
        }
    }
}
```

Start the project and point your browser towards the endpoint (something like http://localhost:7071/api/IoC/) and you should get a response similar to 

{"id":"6a79098a-0dba-4e3b-b4b2-7769c43ac90c","greeting":"","name":"Timmy"}

**IMPORTANT CONCEPTS**

Although the IoC provider is used by the framework, it is still available for use.

Any IoC provider can be used as long as it provides as it uses an implementation of IServiceCollection. 
## Validation 
Requests can be validated prior to to being routed to the handler. Validation errors are automatically returned as an action context

Validation is run before OnRequest, and is run against a given path (similar to OnRequest handlers)

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
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Handlers;

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
Start your project and point your browser (probably http://localhost:7071/api/Validation) and you shoudl see 

[{"propertyName":"name","errorMessage":"Please supply a value for name"}]

add a query parameter of name (E.G. http://localhost:7071/api/Validation?name=timmy) and you should see.

{"message":"Hello timmy"}

**IMPORTANT CONCEPTS**

Validation handlers can separate the logic of validation away from operational logic. 
When validation handlers add validation errors, OnRequest handlers are not called, but AfterEveryRequest handlers will be called. 
AfterEveryRequest handlers can see the validation erros, so can adjust thier behaviours.   
  




## Error Handling
Errors can be routed to a specified handler, which can be used for logging etc

Edit **Startup.cs**
```
using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._6Examples.Startup))]
namespace Officium._6Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<SimpleLogger, SimpleLogger>();

            using (var b = new Builder(builder.Services))
            {               
                b.OnRequest<RequestHandler>(
                    RequestMethod.GET,
                    "/api/Errors/");

                b.OnError<ErrorHandler>();
            }           
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
using Officium.Tools.Helpers;
using System;

namespace Officium._6Examples
{
    public class ErrorFunction
    {
        private readonly IRequestResolver requestResolver;
        public ErrorFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("IoC")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "Errors/")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Error function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class RequestHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            throw new System.IO.FileNotFoundException("Cant Find important file !!!! !");
        }
    }

    public class ErrorHandler : IHandler
    {
        private readonly SimpleLogger logger;

        public ErrorHandler(SimpleLogger logger)
        {
            this.logger = logger; 
        }
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            logger.Log($"Oh no an error occured of type {response.Exception.GetType().Name} with the message '{response.Exception.Message}'");
            response.Exception = new Exception("sorry a very generic problem occured. Nothing to see here ");
        }
    }

    public class SimpleLogger
    {
        public void Log(string message)
        {
            var originalFgColour = System.Console.ForegroundColor;
            var originalbgColour = System.Console.BackgroundColor;
            System.Console.ForegroundColor = System.ConsoleColor.White;
            System.Console.BackgroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine(message);
            System.Console.ForegroundColor = originalFgColour;
            System.Console.BackgroundColor = originalbgColour;
        }
    }

}

```
Start your project and point your browser to your endpoint (something like http://localhost:7071/api/Errors/)
you should see something like 

sorry a very generic problem occured. Nothing to see here 

and in the command prompt window 

Oh no an error occured of type FileNotFoundException with the message 'Cant Find important file !!!! !

**IMPORTANT CONCEPTS**

Error handlers allow errors to be logged in a simple uniform manner

## 'No Handler' handler
Requests that have no defined handler can be routed to a dedicated handler

Edit **Startup.cs**
```

```

Edit **Function1.cs**
```

```

**IMPORTANT CONCEPTS**
## Authentication
Authentication is handled using existing Claims Pricipals

Edit **Startup.cs**
```
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._8Examples.Startup))]
namespace Officium._8Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<TokenResolver,TokenResolver>();

            using (var b = new Builder(builder.Services))
            {
                b.Authorise<AuthHandler>();
                b.OnRequest<RequestHandler>(
                   RequestMethod.GET,
                   "/api/Auth/");
            }
        }
    }

    public class TokenResolver
    {  
        public ClaimsIdentity GetIdentity(string token)
        {
            var claims = new List<Claim>
            {
                new Claim("Role", "WidgetAdmin"),
                new Claim("Role", "GlobalAdmin")
            };
            var rtn = new ClaimsIdentity(claims);
            return rtn;
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
using Officium.Tools.Helpers;
using System.Security.Claims;

namespace Officium._8Examples
{
    public class AuthFunction
    {
        private readonly IRequestResolver requestResolver;
        public AuthFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("Auth")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "Auth/")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Auth function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class AuthHandler : IHandler
    {
        private readonly TokenResolver tokenResolver;

        public AuthHandler(TokenResolver tokenResolver)
        {
            this.tokenResolver = tokenResolver;
        }
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            var token = request.GetHeaderValue("Authorization"); // get the user unique token 
            request.Identity = tokenResolver.GetIdentity(token);
        }
    }

    public class RequestHandler : IHandler
    {
        private static readonly Claim GlobalAdminClaim = new Claim("Role", "GlobalAdmin"); // reference claim to compare against
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            if (request.Identity.HasClaim(GlobalAdminClaim))
            {
                response.Result = "Welcome our new admin";
            }
            else
            {
                response.Result = "Hi there";
            }
        }
    }  

}

```
Start your project and point your browser towards your new endpoint (somethnig like http://localhost:7071/api/Auth/)

you should see something like 

Welcome our new admin

 

**IMPORTANT CONCEPTS**

Auth handlers are called after BeforeEveryRequest handlers but before OnRequest handlers. All requests use the same Auth handlers. Each handler (excluding the BeforeEveryRequest handler) has access to claims.
 
