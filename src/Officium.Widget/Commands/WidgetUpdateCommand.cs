using Officium.Attributes;
using Officium.Commands;

namespace Officium.Widget.Commands
{
    [CommandHandlerRouting(RequestType = CommandRequestType.HttpPut, Path = "api/v1/widget")]
    public sealed class WidgetUpdateCommand : WidgetCommand { }
}
