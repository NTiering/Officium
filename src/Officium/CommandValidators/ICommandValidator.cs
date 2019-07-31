namespace Officium.CommandValidators
{
    using System.Collections.Generic;
    using Officium.Commands;
    public interface ICommandValidator
    {
        bool CanValidate(ICommand command, ICommandContext context);
        IEnumerable<IValidationResult> Validate(ICommand command, ICommandContext context);
    }
}
