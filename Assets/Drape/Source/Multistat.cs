using System.Collections.Generic;
using Drape.Interfaces;

namespace Drape
{
    public class Multistat : Stat, IStat
    {
        public Stat[] Stats { get; private set; }

        public Multistat(string name, HashSet<Stat> stats) : base(name, 0)
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