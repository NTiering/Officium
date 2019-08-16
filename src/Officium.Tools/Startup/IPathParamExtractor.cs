namespace Officium.Tools.Handlers
{
    using System.Collections.Generic;

    public interface IPathParamExtractor
    {
        Dictionary<string, int> MakePathParams(string pathSelector);
    }
}