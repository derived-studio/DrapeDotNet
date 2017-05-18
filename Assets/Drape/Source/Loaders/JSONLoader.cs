using Drape.TinyJson;
using Drape.Interfaces;

namespace Drape.Loaders
{
    public class JSONLoader: LoaderBase, ILoader
    {
        public JSONLoader(Registry registry) : base(registry)
        {
        }

        public override void Load<T>(string path)
        {
            try {
                string json;
                json = System.IO.File.ReadAllText(path);

                T[] stats = json.FromJson<T[]>();
                foreach (T stat in stats) {
                    Registry.Add<T>(stat);
                }

                TriggerLoadingComplete();
            } catch (System.IO.FileNotFoundException  e) {
                TriggerLoadingError(e);
            }
        }
    }

}
