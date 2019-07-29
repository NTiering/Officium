namespace Officium.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    public interface ICommandFactory
    {
        bool TryRegisterCommandType(CommandRequestType commandType, Regex requestSourceMatch, Type t);
        bool TryRegisterCommandType<T>(CommandRequestType type, Regex requestSourceMatch) where T : ICommand,new();     
        ICommand BuildCommand(CommandRequestType type, string requestSource, Dictionary<string,string> input);
    }
}
