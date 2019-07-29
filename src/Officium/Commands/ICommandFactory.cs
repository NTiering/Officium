namespace Officium.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    public interface ICommandFactory
    {
        void RegisterCommandType(CommandRequestType commandType, Regex requestSourceMatch, Type t);
        void RegisterCommandType<T>(CommandRequestType type, Regex requestSourceMatch) where T : ICommand,new();     
        ICommand BuildCommand(CommandRequestType type, string requestSource, Dictionary<string,string> input);
    }
}
