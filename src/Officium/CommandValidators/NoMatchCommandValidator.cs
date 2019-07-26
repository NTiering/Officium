using System;
using System.Collections.Generic;
using System.Text;
using Officium.Commands;

namespace Officium.CommandValidators
{
    public class NoMatchCommandValidator : ICommandValidator
    {
        public bool CanValidate(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}
