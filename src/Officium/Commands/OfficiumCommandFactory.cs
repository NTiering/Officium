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

        public ICommand BuildCommand(CommandRequestType commandType, string requestSource, Dictionary<string, string> input)
        {
            var cle = commandListEntries.FirstOrDefault(x => IsCommandListMatch(x, commandType, requestSource));
            var rtn = (cle == null) ? new NoMatchCommand() : input.ToObject(cle.CommandType);
            rtn.CommandRequestType = (cle == null) ? CommandRequestType.NoMatch : commandType;
            return rtn;
        }      

        public void RegisterCommandType<T>(CommandRequestType commandType, Regex requestSourceMatch) where T : ICommand, new()
        {
            commandListEntries.Add(new CommandListEntry(commandType, requestSourceMatch, typeof(T)));
        }

        private static bool IsCommandListMatch(CommandListEntry commandListEntry, CommandRequestType commentType, string requestSource)
        {
            return commandListEntry.RequestType == commentType && commandListEntry.RequestSourceMatch.IsMatch(requestSource);
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
