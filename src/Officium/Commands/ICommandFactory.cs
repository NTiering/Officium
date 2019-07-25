using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Officium.Commands
{
    public interface ICommandFactory
    {
        void RegisterCommandType<T>(CommandRequestType type, Regex requestSourceMatch) where T : ICommand,new();     
        ICommand BuildCommand(CommandRequestType type, Regex requestSourceMatch,object input);
    }
}
