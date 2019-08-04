﻿namespace Officium.CommandHandlers
{
    using Officium.Commands;

    public abstract class BaseCommandHandler<T> : ICommandHandler
        where T : class,ICommand
    {
        public virtual bool CanHandle(ICommand command, ICommandContext context)
        {
            var rtn = command is T;
            return rtn && CanHandleRequest(command,context);
        }

        protected virtual bool CanHandleRequest(ICommand command, ICommandContext context)
        {
            return true;
        }

        public void Handle(ICommand command, ICommandContext context)
        {
            var c = command as T;
            if (c == null) return;
            HandleCommand(c, context);
        }

        protected abstract void HandleCommand(T command, ICommandContext context);
    }
}
