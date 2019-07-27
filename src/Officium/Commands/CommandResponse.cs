using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Officium.CommandValidators;

namespace Officium.Commands
{
    public class CommandResponse : ICommandResponse
    {
        private readonly Dictionary<string, string> values = new Dictionary<string, string>();
        public IValidationResult[] ValidationResults { get; set; }
        public Dictionary<string, string> Values => values.ToDictionary(x => x.Key, x => x.Value);

        public void AddValue(string name, string value, bool allowOverwrite = false)
        {
            if (values.ContainsKey(name) && !allowOverwrite)
            {
                throw new InvalidOperationException($"Cannot add duplicate value '{name}' ");
            }
            values[name] = value;
        }

        public bool HasValue(string name)
        {
            return values.ContainsKey(name);
        }
    }
}
