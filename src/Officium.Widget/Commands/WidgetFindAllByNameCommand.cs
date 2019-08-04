using Officium.Attributes;
using Officium.Commands;

namespace Officium.Widget.Commands
{
    [CommandHandlerRouting(RequestType = CommandRequestType.HttpGet, Path = "api/v1/widget/name/.")]
    public sealed class WidgetFindAllByNameCommand : ICommand
    {
        public string Id { get; set; }
    }
}
