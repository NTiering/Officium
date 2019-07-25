using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Officium.Commands
{
    public enum CommandRequestType
    {
        HttpPost,
        HttpGet,
        HttpPut,
        HttpDelete,
    }
}