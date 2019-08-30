using System;
using Microsoft.AspNetCore.Mvc;

namespace Officium.Tools.Response
{
    public interface IResponseContent
    {
        Exception Exception { get; set; }
        object Result { get; set; }
        int StatusCode { get; set; }

        ActionResult GetActionResult();
    }
}