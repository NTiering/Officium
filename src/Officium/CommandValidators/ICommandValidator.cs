using System;
using System.Collections.Generic;
using System.Text;
using Officium.Commands;

namespace Officium.CommandValidators
{
    public interface ICommandValidator
    {
        bool CanValidate(ICommand command);
    }
}
