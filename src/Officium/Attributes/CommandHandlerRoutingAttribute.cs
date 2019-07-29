namespace Officium.Attributes
{
    using Officium.Commands;
    using System;
    public class CommandHandlerRoutingAttribute : Attribute
    {
        public CommandRequestType RequestType { get; set; }
        public string Path { get; set; }
    }
}
