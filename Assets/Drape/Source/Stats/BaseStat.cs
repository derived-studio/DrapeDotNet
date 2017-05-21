using Drape.Interfaces;
using Drape.TinyJson;

namespace Drape
{
    public class BaseStat<TStat, TData>: IStat
        where TStat: IStat
        where TData : BaseStatData
    {
        protected TData _data;

        public string Code { get { return _data.code; } }

        public string Name { get { return _data.name; } }

        public int BaseValue { get { return _data.value; } }

        public virtual float Value { get { throw new System.NotImplementedException(); } }

        public BaseStat(TData data, IRegistry registry) 
        {
            _data = data;
            registry.Add<BaseStat<TStat, TData>>(this);
        }

        /// <summary>
        /// Serializes stat to JSON using plane stat Data class
        /// </summary>
        /// <returns></returns>
        public virtual string ToJSON() { return _data.ToJson(); }

        /// <summary>
        /// Factory method generating stats from Array of stat Data serialized to JSON
        /// </summary>
        /// <typeparam name="TStat"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TStat[] FromJSONArray<TData>(string json)
        {
            TData[] dataArr = json.FromJson<TData[]>();
            TStat[] statArr = new TStat[dataArr.Length];
            for (int i = 0; i < dataArr.Length; i++) {
                statArr[i] = (TStat)System.Activator.CreateInstance(typeof(TStat), new object[] { dataArr[i] });
            }
            return statArr;
        }

        /// <summary>
        /// Factory method generating stat from stat Data serialized to JSON
        /// </summary>
        /// <typeparam name="TStat"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TStat FromJSON<TData>(string json)
        {
            TData data = json.FromJson<TData>();
            TStat stat = (TStat)System.Activator.CreateInstance(typeof(TStat), new object[] { data });
            return stat;
        }
    }
}