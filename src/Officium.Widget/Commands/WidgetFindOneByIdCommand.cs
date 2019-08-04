using Officium.Attributes;
using Officium.Commands;

namespace Officium.Widget.Commands
{
    [CommandHandlerRouting(RequestType = CommandRequestType.HttpGet, Path = "api/v1/widget/[^name]")]
    public sealed class WidgetFindOneByIdCommand : WidgetCommand { }
}
