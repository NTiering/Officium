using System.Collections.Generic;
using Officium.Ext;
using Officium.Widget.Commands;

namespace Officium.Widget.Data
{
    public interface IWidgetDataContext
    {
        void Add(IWidget widget);
        IEnumerable<IWidget> GetAll(PaginationRequest req = null);
        IEnumerable<IWidget> FindByName(string name, PaginationRequest req = null);
        IWidget GetById(string id);
        bool Remove(IWidget widget);
        bool Update(IWidget widget);
    }
}