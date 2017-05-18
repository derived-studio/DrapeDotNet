using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
    public class Multistat : Stat, IStat
    {
        public IStat[] Stats { get; private set; }

        public Multistat(string name, HashSet<IStat> stats) : this(name.ToSlug(), name, 0, stats) { }
        public Multistat(string name, int baseValue, HashSet<IStat> stats) : this(name.ToSlug(), name, baseValue, stats) { }
        public Multistat(string code, string name, int baseValue, HashSet<IStat> stats) : base(code, name, baseValue)
        {
            this.Stats = new Stat[stats.Count];
            stats.CopyTo(this.Stats);
        }

        /// <summary>
        /// Value by default is a sum of all stats.
        /// </summary>
        public override float Value {
            get {
                float value = BaseValue;
                foreach(Stat stat in Stats) {
                    value += stat.Value;
                }
                return value;
            }
        }
    }
}