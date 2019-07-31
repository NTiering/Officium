using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Widget.Commands
{
    public interface IWidget
    {
        string Name { get; set; }
        string Id { get; set; }
        DateTime LastUpdated { get; set; }
    }
}
