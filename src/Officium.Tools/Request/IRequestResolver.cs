using Officium.Tools.Response;

namespace Officium.Tools.Request
{
    public interface IRequestResolver
    {
        IResponseContent Execute(IRequestContext req);
    }
}