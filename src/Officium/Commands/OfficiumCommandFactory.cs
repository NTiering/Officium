using System;
using System.Collections.Generic;
using System.Linq;
using Officium.Ext;
using System.Text.RegularExpressions;

namespace Officium.Commands
{
    public class OfficiumCommandFactory : ICommandFactory
    {
        private readonly List<CommandListEntry> commandListEntries = new List<CommandListEntry>();

        public ICommand BuildCommand(CommandRequestType commentType, string requestSource, Dictionary<string, string> input)
        {
            var cle = commandListEntries.FirstOrDefault(x => x.RequestType == commentType && x.RequestSourceMatch.IsMatch(requestSource));
            var rtn = input.ToObject(cle.CommandType);
            return rtn;
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
