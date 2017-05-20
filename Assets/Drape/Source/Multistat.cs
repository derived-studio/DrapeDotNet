using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
    public class Multistat : BaseStat<Stat, MultistatData>, IStat
    {
        private IStat[] _stats;

        public Multistat(string name, HashSet<IStat> stats) : this(name.ToSlug(), name, 0, stats) { }
        public Multistat(string name, int baseValue, HashSet<IStat> stats) : this(name.ToSlug(), name, baseValue, stats) { }
        public Multistat(string code, string name, int value, HashSet<IStat> stats) : base(new MultistatData(code, name, value))
        {
            _stats = new Stat[stats.Count];
            stats.CopyTo(_stats);

            // process for serialization
            if (_stats.Length  > 0) {
                _data.stats = new string[_stats.Length];
                List<string> statList = new List<string>();
                foreach (IStat stat in _stats) {
                    statList.Add(stat.Code);
                }
                _data.stats = statList.ToArray();
            }
        }

        /// <summary>
        /// Value by default is a sum of all stats.
        /// </summary>
        public override float Value {
            get {
                float value = BaseValue;
                foreach(Stat stat in _stats) {
                    value += stat.Value;
                }
                return value;
            }
        }
    }
}