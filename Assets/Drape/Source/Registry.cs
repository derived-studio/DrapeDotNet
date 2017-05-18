using System.Collections;
using System.Collections.Generic;
using Drape.Interfaces;

namespace Drape {
    public class Registry
    {
        Dictionary<string, HashSet<IStat>> stats = new Dictionary<string, HashSet<IStat>>();
        //HashSet<IStat> stats;
        HashSet<Modifier> modifier;
        // skills
        // abilities

        /// <summary>
        /// Normaly only single instance of Registry is required.
        /// It is, however, possible to have as many Registries as you need;
        /// </summary>
        /// <param name="loader"></param>
        public Registry(ILoader loader)
        {

        }

        public void Add<T>(T stat) where T:IStat
        {
            string type = stat.GetType().Name;
            if (!stats.ContainsKey(type)) {
                stats.Add(type, new HashSet<IStat>());
            }
            stats[type].Add(stat);
        }

        public T Get<T>(string code) where T: IStat {
            string type = typeof(T).Name;
            if (stats.ContainsKey(type)) {
                foreach (T s in stats[type]) {
                    if (s.Code == code) {
                        return (T)s;
                    }
                }
            }

            return default(T);
        }

    }
}