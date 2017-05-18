using Drape.Delegates;
using Drape.Interfaces;

namespace Drape
{
    public interface ILoader
    {
        event OnLoadCompleteHandler OnLoadComplete;
        event OnLoadErrorHandler OnLoadError;

        void Load<T>(string path) where T : IStat;
    }
}

namespace Drape.Delegates
{
    public delegate void OnLoadCompleteHandler();
    public delegate void OnLoadErrorHandler(System.Exception e);
}