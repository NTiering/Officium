using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Officium.Commands;

namespace Officium.CommandValidators
{
    public class NoMatchCommandValidator : ICommandValidator
    {
        public bool CanValidate(ICommand command)
        {
            return false;
        }

        public IEnumerable<IValidationResult> Validate(ICommand command)
        {
            return Enumerable.Empty<IValidationResult>();
        }
    }
}
