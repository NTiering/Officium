namespace Officium.CommandValidators
{
    using System.Collections.Generic;
    using System.Linq;
    using Officium.Commands;
    public class NoMatchCommandValidator : ICommandValidator
    {
        public bool CanValidate(ICommand command)
        {
            return false;
        }

        public IEnumerable<IValidationResult> Validate(ICommand command)
        {
            return new IValidationResult[0];
        }
    }
}
