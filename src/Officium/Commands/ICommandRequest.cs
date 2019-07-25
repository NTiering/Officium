using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Commands
{
    public interface ICommandRequest
    {
        T GetValue<T>(string name);
        object CommandType { get; }
    }
}
