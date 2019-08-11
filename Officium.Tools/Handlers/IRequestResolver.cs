using Officium.Tools.ReqRes;

namespace Officium.Tools.Handlers
{
    public interface IRequestResolver
    {
        void Execute(RequestContext req, ResponseContent res);
    }
}