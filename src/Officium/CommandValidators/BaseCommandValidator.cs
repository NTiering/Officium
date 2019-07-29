namespace Officium.CommandValidators
{
    using Officium.Commands;
    using System.Collections.Generic;
    using System.Linq;
    public abstract class BaseCommandValidator<T> : ICommandValidator
       where T : class, ICommand
    {
        public bool CanValidate(ICommand command)
        {
            var rtn = command is T;
            return rtn;
        }

        public IEnumerable<IValidationResult> Validate(ICommand command)
        {
            var cmd = command as T;
            if (cmd == null) return Enumerable.Empty<IValidationResult>();
            var rtn = new List<IValidationResult>();
            Validate(rtn, cmd);

            if (command.CommandResponse.ValidationResults != null)
            {
                rtn.AddRange(rtn);
            }

            return rtn;
        }

        protected void AddValidationError(List<IValidationResult> validatioResults, string name, string value)
        {
            validatioResults.Add(new ValidationResult { PropertyName = name, PropertyValue = value });
        }

        class ValidationResult : IValidationResult
        {
            public string PropertyName { get; set; }

            public string PropertyValue { get; set; }
        }
        protected abstract void Validate(List<IValidationResult> rtn, T cmd);
    }
}
