namespace Officium.Plugins.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class PluginStepOrderTool
    {
        public static PluginStepOrderTool Instance = new PluginStepOrderTool();
        public IEnumerable<PluginStepOrder> GetSteps { get; private set; }
        public IEnumerable<PluginStepOrder> PostSteps { get; private set; }
        public IEnumerable<PluginStepOrder> PutSteps { get; private set; }
        public IEnumerable<PluginStepOrder> DeleteSteps { get; private set; }
        public IEnumerable<PluginStepOrder> AfterAllSteps { get; private set; }
        public IEnumerable<PluginStepOrder> BeforeAllSteps { get; private set; }

        private PluginStepOrderTool()
        {
            GetSteps = ExtractSteps("Get");
            PostSteps = ExtractSteps("Post");
            PutSteps = ExtractSteps("Put");
            DeleteSteps = ExtractSteps("Delete");
            BeforeAllSteps = new[] { PluginStepOrder.AlwaysFirst, PluginStepOrder.BeforeAll }.OrderBy(x => x);
            AfterAllSteps = new[] { PluginStepOrder.AlwaysLast, PluginStepOrder.AfterAll }.OrderBy(x => x);
        }

        private static IEnumerable<PluginStepOrder> ExtractSteps(string prefix)
        {
            var values = Enum.GetValues(typeof(PluginStepOrder))
               .Cast<PluginStepOrder>()
               .Where(x => x.ToString().Contains(prefix))
               .OrderBy(x => (int)x);

            return values;
        }
    }
}