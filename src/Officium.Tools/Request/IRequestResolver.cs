using Officium.Tools.Response;

namespace Officium.Tools.Request
{
    public interface IRequestResolver
    {
        ResponseContent Execute(RequestContext req);
    }
}