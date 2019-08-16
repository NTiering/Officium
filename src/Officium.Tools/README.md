# Officium
Framework for allow rapid development of Azure Function

## Overview 

Officium is a framework to support radpid development of azure functions. 

## Feature list

1. Support ALL http Methods (GET,HEAD,POST,PUT,DELETE,CONNECT,OPTIONSTRACE,PATCH) 
1. Request Routing
1. Validation
1. Dependency Injection / IoC
1. Error Handling
1. Query, Pody (POST'ed) and URL path params supported
1. Handle before and after every request
1. Header Parameters
1. Unhandled Requests
1. Auth and Identity

## Getting Started 
Add handling to your azure function in a few lines of code.

```

```
## Variables 
Variables can be accessed in any handlers from the request context 
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

## Before and After Every Request
Handlers can be set up to intercept every request either before or after any other handlers
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
