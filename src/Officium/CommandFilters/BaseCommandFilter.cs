namespace Officium.CommandFilters
{
    using Officium.Commands;
    public abstract class BaseCommandFilter<T> : ICommandFilter
        where T : class, ICommand
    {
        public void AfterHandleEvent(ICommand command)
        {
            var cmd = command as T;
            if (cmd == null) return;
            AfterHandle(cmd);
        }

        public void BeforeHandleEvent(ICommand command)
        {
            var cmd = command as T;
            if (cmd == null) return;
            BeforeHandle(cmd);
        }

        public bool CanFilter(ICommand command)
        {
            var rtn = command is T;
            return rtn;
        }
        protected abstract void AfterHandle(T cmd);
        protected abstract void BeforeHandle(T cmd);

    }
}
