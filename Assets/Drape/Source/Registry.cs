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

        public void Add<T>(T stat) where T:IStat
        {
            string type = stat.GetType().Name;
            if (!stats.ContainsKey(type)) {
                stats.Add(type, new HashSet<IStat>());
            }
            UnityEngine.Debug.Log(stat.Code + ">>>");
            stats[type].Add(stat);
        }

        public T Get<T>(string code) where T: IStat {
            string type = typeof(T).Name;
            UnityEngine.Debug.Log(type);
            if (stats.ContainsKey(type)) {
                UnityEngine.Debug.Log("y");
                foreach (T s in stats[type]) {
                    UnityEngine.Debug.Log(s.Code + " ---  " + code);
                    if (s.Code == code) {
                        return (T)s;
                    }
                }
            }

            return default(T);
        }

    }
}