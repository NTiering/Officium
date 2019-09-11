namespace Officium.Plugins
{
    /// <summary>
    /// Standard context to use if one is not supplied
    /// </summary>
    internal class DefaultPluginContext : IPluginContext
    {
        public bool HaltExecution { get; set; }
    }
}
