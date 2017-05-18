using Drape.TinyJson;
using Drape.Interfaces;

namespace Drape.Loaders
{
    public class LoaderBase : ILoader
    {
        public event Delegates.OnLoadCompleteHandler OnLoadComplete;
        public event Delegates.OnLoadErrorHandler OnLoadError;

        public Registry Registry { get; private set; }

        public LoaderBase(Registry registry)
        {
            Registry = registry;
        }

        public virtual void Load<T>(string path) where T: IStat
        {
            throw new System.NotImplementedException();
        }

        internal void TriggerLoadingComplete()
        {
            if (OnLoadComplete != null) {
                OnLoadComplete();
            }
        }

        internal void TriggerLoadingError(System.Exception e)
        {
            if (OnLoadError != null) {
                OnLoadError(e);
            }
        }
    }
}