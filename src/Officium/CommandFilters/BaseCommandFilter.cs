namespace Officium.CommandFilters
{
    using Officium.Commands;
    public abstract class BaseCommandFilter<T> : ICommandFilter
        where T : class, ICommand
    {
        public void AfterHandleEvent(ICommand command, ICommandContext context)
        {
            var cmd = command as T;
            if (cmd == null) return;
            AfterHandle(cmd, context);
        }

        public void BeforeHandleEvent(ICommand command, ICommandContext context)
        {
            var cmd = command as T;
            if (cmd == null) return;
            BeforeHandle(cmd, context);
        }

        public bool CanFilter(ICommand command , ICommandContext context)
        {
            var rtn = command is T;
            return rtn;
        }
        protected abstract void AfterHandle(T cmd, ICommandContext context);
        protected abstract void BeforeHandle(T cmd, ICommandContext context);

    }
}
