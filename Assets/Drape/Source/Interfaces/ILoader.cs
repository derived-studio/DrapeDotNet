using System.Collections.Generic;

namespace Drape
{
    public interface ILoader
    {
        Dictionary<string, Stat> Load();
    }
}