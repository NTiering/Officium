using Officium.Attributes;
using Officium.Commands;

namespace Officium.Widget.Commands
{
    [CommandHandlerRouting(RequestType = CommandRequestType.HttpDelete, Path = "api/v1/widget")]
    public sealed class WidgetRemoveCommand : WidgetCommand { }
}
