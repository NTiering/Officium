namespace Officium.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Officium.Ext;
    using System.Text.RegularExpressions;
    public class CommandFactory : ICommandFactory
    {
        private static readonly List<CommandListEntry> commandListEntries = new List<CommandListEntry>();

        public CommandFactory()
        {

        }

        public CommandFactory(bool removeCommandListEntries)
        {
            if (removeCommandListEntries)
            {
                lock (typeof(CommandFactory))
                {
                    commandListEntries.Clear();
                }

            }
        }

        public ICommand BuildCommand(CommandRequestType commandType, string requestSource, Dictionary<string, string> input)
        {
            var cle = SelectCommandListEntries(commandType, requestSource);
            var rtn = MakeCommand(cle, input);
            SetCommandType(commandType, cle, rtn);
            return rtn;
        }

        public bool TryRegisterCommandType<T>(CommandRequestType commandType, string requestSourceMatch) where T : ICommand, new()
        {
            return TryAdd(new CommandListEntry(commandType, requestSourceMatch, typeof(T)));
        }



        public bool TryRegisterCommandType(CommandRequestType commandType, string requestSourceMatch, Type t)
        {
            return TryAdd(new CommandListEntry(commandType, requestSourceMatch, t));
        }

        private CommandListEntry SelectCommandListEntries(CommandRequestType commandType, string requestSource)
        {
            var rtn = commandListEntries.FirstOrDefault(x => IsCommandListMatch(x, commandType, requestSource));
            return rtn;
        }

        private bool TryAdd(CommandListEntry commandListEntry)
        {
            var existsAlready = commandListEntries.Contains(commandListEntry);
            if (existsAlready == false)
            {
                commandListEntries.Add(commandListEntry);
            }
            return !existsAlready;
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

        private class CommandListEntry : IEquatable<CommandListEntry>
        {
            public CommandListEntry(CommandRequestType requestType, string requestSourceMatch, Type commandType)
            {
                RequestType = requestType;           
                CommandType = commandType;
                RequestSourceMatch = new Regex(requestSourceMatch);
                RequestSourceString = requestSourceMatch;
            }
            public CommandRequestType RequestType { get; }
            public Regex RequestSourceMatch { get; }
            public Type CommandType { get; }
            public string RequestSourceString { get; }

            public bool Equals(CommandListEntry other)
            {
                if (RequestType != other.RequestType) return false;
                if (CommandType != other.CommandType) return false;
                if (RequestSourceString != other.RequestSourceString) return false;
                return true;
            }
        }
    }
}
