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
}
