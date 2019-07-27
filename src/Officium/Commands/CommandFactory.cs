using System;
using System.Collections.Generic;
using System.Linq;
using Officium.Ext;
using System.Text.RegularExpressions;

namespace Officium.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly List<CommandListEntry> commandListEntries = new List<CommandListEntry>();

        public ICommand BuildCommand(CommandRequestType commandType, string requestSource, Dictionary<string, string> input)
        {
            var cle = SelectCommandListEntries(commandType, requestSource);
            var rtn = MakeCommand(cle, input);
            SetCommandType(commandType, cle, rtn);
            return rtn;
        }               

        public void RegisterCommandType<T>(CommandRequestType commandType, Regex requestSourceMatch) where T : ICommand, new()
        {
            commandListEntries.Add(new CommandListEntry(commandType, requestSourceMatch, typeof(T)));
        }

        private CommandListEntry SelectCommandListEntries(CommandRequestType commandType, string requestSource)
        {
            var rtn = commandListEntries.FirstOrDefault(x => IsCommandListMatch(x, commandType, requestSource));
            return rtn;
        }

        private static void SetCommandType(CommandRequestType commandType, CommandListEntry cle, ICommand rtn)
        {
            rtn.CommandRequestType = (cle == null) ? CommandRequestType.NoMatch : commandType;
        }

        private static ICommand MakeCommand(CommandListEntry cle, Dictionary<string, string> input)
        {
            var rtn = (cle == null) ? new NoMatchCommand() : input.ToObject(cle.CommandType);
            rtn.CommandResponse = new CommandResponse(); 
            return rtn;
        }

        private static bool IsCommandListMatch(CommandListEntry commandListEntry, CommandRequestType commentType, string requestSource)
        {
            var rtn = commandListEntry.RequestType == commentType && commandListEntry.RequestSourceMatch.IsMatch(requestSource);
            return rtn;
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
