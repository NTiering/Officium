namespace Officium.CommandHandlers
{
    using Officium.Commands;

    public abstract class BaseCommandHandler<T> : ICommandHandler
        where T : class,ICommand
    {
        public virtual bool CanHandle(ICommand command, ICommandContext context)
        {
            var rtn = command is T;
            return rtn;
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
