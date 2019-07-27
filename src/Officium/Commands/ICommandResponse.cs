using Officium.CommandValidators;
using System.Collections.Generic;

namespace Officium.Commands
{
    public interface ICommandResponse
    {
        IValidationResult[] ValidationResults { get; set; }
        Dictionary<string, string> Values { get; }
        void AddValue(string name, string value, bool allowOverwrite = false);
        bool HasValue(string name);
    }
}
