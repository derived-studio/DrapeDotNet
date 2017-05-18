using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
    public class Multistat : Stat, IStat
    {
        public IStat[] Stats { get; private set; }

        /*public Multistat(string name, HashSet<IStat> stats) : this(name.ToSlug(), name, stats) { }*/
        public Multistat(string code, string name, HashSet<IStat> stats) : base(code, name, 0)
        {
            this.Stats = new Stat[stats.Count];
            stats.CopyTo(this.Stats);
        }

        /// <summary>
        /// Value by default is a sum of all stats.
        /// </summary>
        public override float Value {
            get {
                float value = 0;
                foreach(Stat stat in Stats) {
                    value += stat.Value;
                }
                return value;
            }
        }
    }
}