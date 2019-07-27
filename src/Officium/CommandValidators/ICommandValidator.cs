namespace Officium.CommandValidators
{
    using System.Collections.Generic;
    using Officium.Commands;
    public interface ICommandValidator
    {
        bool CanValidate(ICommand command);
        IEnumerable<IValidationResult> Validate(ICommand command);
    }
}
