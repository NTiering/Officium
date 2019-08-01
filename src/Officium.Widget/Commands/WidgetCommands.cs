using Officium.Attributes;
using Officium.Commands;
using System;

namespace Officium.Widget.Commands
{
  
    public class WidgetCommand : ICommand, IWidget
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public DateTime LastUpdated { get; set; }       
    }

    [CommandHandlerRouting(RequestType = CommandRequestType.HttpPost, Path = "api/v1/widget")]
    public sealed class WidgetAddCommand : WidgetCommand { }

    [CommandHandlerRouting(RequestType = CommandRequestType.HttpPut, Path = "api/v1/widget")]
    public sealed class WidgetUpdateCommand : WidgetCommand { }

    [CommandHandlerRouting(RequestType = CommandRequestType.HttpDelete, Path = "api/v1/widget")]
    public sealed class WidgetRemoveCommand : WidgetCommand { }

    [CommandHandlerRouting(RequestType = CommandRequestType.HttpGet, Path = "api/v1/widget/[^name]")]
    public sealed class WidgetGetByIdCommand : WidgetCommand { }

    [CommandHandlerRouting(RequestType = CommandRequestType.HttpGet, Path = "api/v1/widget/name/.")]
    public sealed class WidgetFindAllByNameCommand : ICommand
    {
        public string Id { get; set; }
    }
}
