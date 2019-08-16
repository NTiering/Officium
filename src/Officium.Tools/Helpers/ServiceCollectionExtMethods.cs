namespace Officium.Tools.Helpers
{
    using Microsoft.Extensions.DependencyInjection;
    using Officium.Tools.Request;

    public static class ServiceCollectionExtMethods
    {
        public static void AddInternalServices(this IServiceCollection collection)
        {
            collection?.AddSingleton<IRequestContext, RequestContext>();
            collection?.AddSingleton<IValueExtractor,ValueExtractor>();
        }
    }
}
