using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Request;

namespace Officium.Tools.Helpers
{
    public static class ServiceCollectionExtMethods
    {
        public static void AddInternalServices(this IServiceCollection collection)
        {
            collection?.AddSingleton<IRequestContext, RequestContext>();
            collection?.AddSingleton<IValueExtractor,ValueExtractor>();
        }
    }
}
