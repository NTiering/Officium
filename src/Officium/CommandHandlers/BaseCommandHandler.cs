namespace Officium.CommandHandlers
{
    using Officium.Commands;

    public abstract class BaseCommandHandler<T> : ICommandHandler
        where T : class,ICommand
    {
        public virtual bool CanHandle(ICommand command)
        {
            var rtn = command is T;
            return rtn;
        }

        public void Handle(ICommand command)
        {
            var c = command as T;
            if (c == null) return;
            HandleCommand(c);
        }

        protected abstract void HandleCommand(T command);
    }
}
