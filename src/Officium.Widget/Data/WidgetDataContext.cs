using Officium.Ext;
using Officium.Widget.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Officium.Widget.Data
{
    public class WidgetDataContext : IWidgetDataContext
    {
        private static readonly List<IWidget> widgetStore = new List<IWidget>();
        public void Add(IWidget widget)
        {
            widget.Id = Guid.NewGuid().ToString();
            widget.LastUpdated = DateTime.Now;
            widgetStore.Add(widget);
        }

        public bool Remove(IWidget widget)
        {
            var t = widgetStore.FindIndex(x => x.Id == widget.Id);
            if (t < 0) return false;
            widgetStore.RemoveAt(t);
            return true;
        }

        public bool Update(IWidget widget)
        {
            var t = widgetStore.FindIndex(x => x.Id == widget.Id);
            if (t < 0) return false;
            widgetStore.RemoveAt(t);
            widgetStore.Add(widget);
            return true;
        }

        public IWidget GetById(string id)
        {
            var t = widgetStore.FirstOrDefault(x => x.Id == id);
            return t;
        }

        public IEnumerable<IWidget> GetAll(PaginationRequest req = null)
        {
            var t = widgetStore.AsQueryable().Paginate(req);
            return t;
        }

        public IEnumerable<IWidget> FindByName(string name, PaginationRequest req = null)
        {
            var t = widgetStore.Where(x => x.Name == name).Paginate(req);
            return t;
        }        
    }
}
