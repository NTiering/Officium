# Officium

## Overview

Officium is a suite of tools for rapid development of Azure Http triggered functions using existing IoC frameworks 


## Quick start

*Prerequisites* 

Setup your Azure Function to Use Dependency injection 
https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection

Add a reference to Offcium.Tools 
```
Install-Package Officium.Tools
```
Steps 


Add 2 line to your Azure startup file (in the configure method)
```
 public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddPlugins(); // find and register all classes implementing 'IFunctionPlugin'
            builder.Services.AddOficuimServices(); // add the services we'll need
        }
    }

```
Add a constructor to your azure function , with a private field
```
private readonly IExecutor executor;

   public Function1(IExecutor executor)
   {
      this.executor = executor;
   } 
```
In the Run method, Add a line to start routing requests to your handlers 
```
public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "v1/{n1?}")] HttpRequest req,
        ILogger log)
        {           
            return executor.ExecuteRequest(req, log);
        }
```
Finally add your handler 

```
public class HelloWorldPlugin : IFunctionPlugin
{
    public PluginStepOrder StepOrder => PluginStepOrder.OnGet; // run this on every get request

    public IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context)
    {
        string name = req.Query["name"];
        return name != null
            ? (ActionResult)new OkObjectResult($"Hello, {name}")
            : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
    }
}
```