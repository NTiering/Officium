namespace Officium.Example.Commands
{
    using Officium.CommandFilters;
    public class HelloWorldCommandFilter : BaseCommandFilter<HelloWorldCommand>
    {
        protected override void AfterHandle(HelloWorldCommand cmd)
        {
            
        }

        protected override void BeforeHandle(HelloWorldCommand cmd)
        {
            if (string.IsNullOrEmpty(cmd.Name)) return;
            cmd.Name = cmd.Name.Trim().ToUpper();
        }
    }
}
