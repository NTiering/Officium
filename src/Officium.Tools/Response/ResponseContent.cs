using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Officium.Tools.Response
{
    public class ResponseContent
    {
        internal ResponseContent()
        {
            Result = new { };
        }
        public readonly List<ValidationError> ValidationError = new List<ValidationError>();
        public int StatusCode { get; set; }
        public object Result { get; set; }
        public Exception Exception { get; set; }
        public ActionResult GetActionResult()
        {
            if (Exception != null)return new BadRequestObjectResult(Exception.Message);
            if (ValidationError != null && ValidationError.Any())return new BadRequestObjectResult(ValidationError);
            return new OkObjectResult(Result){ StatusCode = StatusCode};         
        }
    }
}
