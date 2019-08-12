using Officium.Tools.ReqRes;

namespace Officium.Tools.Handlers
{
    public interface IRequestResolver
    {
        ResponseContent Execute(RequestContext req);
    }
}