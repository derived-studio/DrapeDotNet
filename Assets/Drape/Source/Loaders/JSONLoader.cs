using Drape.TinyJson;
using Drape.Interfaces;
using UnityEngine;

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
                //string json;
                //json = System.IO.File.ReadAllText(path);
                //Application.dataPath

                TextAsset bindata = Resources.Load(path) as TextAsset;
                T[] stats = bindata.text.FromJson<T[]>();
                
                foreach (T stat in stats) {
                    Debug.Log(">>>>" + stats.ToJson());
                    Registry.Add<T>(stat);
                }

                TriggerLoadingComplete();
            } catch (System.IO.FileNotFoundException  e) {
                TriggerLoadingError(e);
            }
        }
    }
}