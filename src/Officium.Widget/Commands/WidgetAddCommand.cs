using Officium.Attributes;
using Officium.Commands;

namespace Officium.Widget.Commands
{
    [CommandHandlerRouting(RequestType = CommandRequestType.HttpPost, Path = "api/v1/widget")]
    public sealed class WidgetAddCommand : WidgetCommand { }
}
