using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Officium.Commands
{
    public class OfficiumCommandFactory : ICommandFactory
    {
        private List<CommandListEntry> commandListEntries = new List<CommandListEntry>();

        public ICommand BuildCommand(CommandRequestType type, Regex requestSourceMatch, object input)
        {
            throw new NotImplementedException();
        }

        public void RegisterCommandType<T>(CommandRequestType commandType, Regex requestSourceMatch) where T : ICommand, new()
        {
            commandListEntries.Add(new CommandListEntry(commandType, requestSourceMatch, typeof(T)));
        }

        private class CommandListEntry
        {
            public CommandListEntry(CommandRequestType requestType, Regex requestSourceMatch, Type commandType)
            {
                RequestType = requestType;
                RequestSourceMatch = requestSourceMatch;
                CommandType = commandType;
            }
            public CommandRequestType RequestType { get; }
            public Regex RequestSourceMatch { get; }
            public Type CommandType { get; }
        }
    }
}
