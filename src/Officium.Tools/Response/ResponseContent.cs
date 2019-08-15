using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Officium.Tools.Response
{
    public class ResponseContent
    {
        public ResponseContent()
        {
            Result = new { };
        }
        public readonly List<ValidationError> ValidationErrors = new List<ValidationError>();
        public int StatusCode { get; set; }
        public object Result { get; set; }
        public Exception Exception { get; set; }
        public ActionResult GetActionResult()
        {
            if (Exception != null) return new BadRequestObjectResult(Exception.Message);
            if (ValidationErrors != null && ValidationErrors.Any())return new BadRequestObjectResult(ValidationErrors);
            return new OkObjectResult(Result){StatusCode = StatusCode};         
        }
    }
}
