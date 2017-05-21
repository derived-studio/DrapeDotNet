using System.Collections;
using System.Collections.Generic;
using Drape.Interfaces;

namespace Drape {
    public class Registry: IRegistry
    {
        Dictionary<string, HashSet<IStat>> _groupedStats = new Dictionary<string, HashSet<IStat>>();
        Dictionary<string, IStat> _stats = new Dictionary<string, IStat>();

        public bool Add<T>(T stat) where T:IStat
        {
            if (_stats.ContainsKey(stat.Code)) {
                return false;
            }

            // catalog by code
            _stats.Add(stat.Code, stat);

            // catalog by explicit type
            string type = stat.GetType().Name;
            if (!_groupedStats.ContainsKey(type)) {
                _groupedStats.Add(type, new HashSet<IStat>());
            }
            _groupedStats[type].Add(stat);
            return true;
        }

        /// <summary>
        /// Returns stats by explicit type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] Get<T>() where T : IStat
        {
            string type = typeof(T).Name;

            // try to return by type
            if (_groupedStats.ContainsKey(type)) {
                IStat[] arr = new IStat[_groupedStats.Count];
                _groupedStats[type].CopyTo(arr);
                return System.Array.ConvertAll(arr, (p => (T)p));
            }

            return new T[0];
        }

        /// <summary>
        /// Returs stat by code casting it to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public T Get<T>(string code) where T: IStat {
            try {
                IStat stat = Get(code);
                return (T)stat;
            } catch (System.Exception e) { }
            return default(T);
        }

        /// <summary>
        /// Returs stat by code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public IStat Get(string code)
        {
            if (_stats.ContainsKey(code)) {
                return _stats[code];
            }
            return null;
        }
    }
}