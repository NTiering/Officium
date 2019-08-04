using System.Collections.Generic;
using Officium.Ext;
using Officium.Widget.Commands;

namespace Officium.Widget.Data
{
    public interface IWidgetDataContext
    {
        void Add(IWidget widget);
        IEnumerable<IWidget> FindAll(PaginationRequest req = null);
        IEnumerable<IWidget> FindAllByName(string name, PaginationRequest req = null);
        IWidget FindOneById(string id);
        bool Remove(IWidget widget);
        bool Update(IWidget widget);
    }
}