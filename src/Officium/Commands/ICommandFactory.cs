namespace Officium.Commands
{
    using System;
    using System.Collections.Generic;
    public interface ICommandFactory
    {
        bool TryRegisterCommandType(CommandRequestType commandType, string requestSourceMatch, Type t);
        bool TryRegisterCommandType<T>(CommandRequestType type, string requestSourceMatch) where T : ICommand,new();     
        ICommand BuildCommand(ICommandContext context, Dictionary<string,string> input);
    }
}
