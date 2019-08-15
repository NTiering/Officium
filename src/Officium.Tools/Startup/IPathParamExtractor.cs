using System.Collections.Generic;

namespace Officium.Tools.Handlers
{
    public interface IPathParamExtractor
    {
        Dictionary<string, int> MakePathParams(string pathSelector);
    }
}