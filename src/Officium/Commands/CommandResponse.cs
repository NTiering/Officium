namespace Officium.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Officium.CommandValidators;
    public class CommandResponse : ICommandResponse
    {
        private readonly Dictionary<string, object> values = new Dictionary<string, object>();
        public IValidationResult[] ValidationResults { get; set; }
        public Dictionary<string, object> Values => values.ToDictionary(x=>x.Key,x=>x.Value);

        public void AddValue(string name, object value, bool allowOverwrite = false)
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
