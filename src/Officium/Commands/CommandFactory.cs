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

        public bool TryRegisterCommandType<T>(CommandRequestType commandType, Regex requestSourceMatch) where T : ICommand, new()
        {
            return TryAdd(new CommandListEntry(commandType, requestSourceMatch, typeof(T)));            
        }

      

        public bool TryRegisterCommandType(CommandRequestType commandType, Regex requestSourceMatch , Type t) 
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
            var existsAlready = commandListEntries.Any(x=> x.CompareTo(commandListEntry) == 1);
            if (existsAlready == false)
            {
                commandListEntries.Add(commandListEntry);
            }
            return existsAlready;
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

        private class CommandListEntry : IComparable<CommandListEntry>
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
           
            public int CompareTo(CommandListEntry other)
            {
                if (RequestType != other.RequestType) return 0;
                if (RequestSourceMatch != other.RequestSourceMatch) return 0;
                if (CommandType != other.CommandType) return 0;
                return 1;
            }
        }
    }
}
