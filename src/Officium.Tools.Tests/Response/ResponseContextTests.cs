using Officium.Tools.Response;
using System;
using System.Collections.Generic;
using FluentAssert;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Officium.Tools.Tests.Response
{
    public class ResponseContextTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new ResponseContent().ShouldNotBeNull();
        }

        [Fact]
        public void ValidationErrorAreNotNull()
        {
            new ResponseContent().ValidationErrors.ShouldNotBeNull();
        }

        [Fact]
        public void ExceptionsCauseBadRequests()
        {
            new ResponseContent { Exception = new Exception() }.GetActionResult().ShouldBeOfType<BadRequestObjectResult>();
        }


        [Fact]
        public void ValidationCauseBadRequests()
        {
            var ctx = new ResponseContent();
            ctx.ValidationErrors.Add(new ValidationError("", ""));
            ctx.GetActionResult().ShouldBeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void DefaultsToOKResult()
        {
            var ctx = new ResponseContent();
            ctx.GetActionResult().ShouldBeOfType<OkObjectResult>();
        }        

    }
}
