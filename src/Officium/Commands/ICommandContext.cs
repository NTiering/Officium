using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Commands
{
    public interface ICommandContext
    {
        string RequestPath { get; set; }
        CommandRequestType CommandRequestType { get; set; }
        ICommandResponse CommandResponse { get; set; }
        Dictionary<string, string> Input { get; set; }
    }
}
