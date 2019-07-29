namespace Officium.Commands
{
    using Officium.CommandValidators;
    using System.Collections.Generic;
    public interface ICommandResponse
    {
        IValidationResult[] ValidationResults { get; set; }
        Dictionary<string, object> Values { get; }
        void AddValue(string name, object value, bool allowOverwrite = false);
        bool HasValue(string name);
    }
}
