namespace Officium.Example.Commands
{
    using Officium.CommandValidators;
    using System.Collections.Generic;
    public class HelloWorldCommandValidator : BaseCommandValidator<HelloWorldCommand>
    {        
        protected override void Validate(List<IValidationResult> validatioResults, HelloWorldCommand cmd)
        {
            if (string.IsNullOrEmpty(cmd.Name))
            {
                AddValidationError(validatioResults, "Name", "Please supply a name");
            }
        }
    }
}
