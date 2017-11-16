using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
    public class Modifier : BaseStat<ModifierData>, IStat
    {
        public string Stat {  get { return Data.Stat;  } }

        public int RawFlat {
            get { return Data.RawFlat; }
        }

        public float RawFactor {
            get { return Data.RawFactor; }
        }

        public int FinalFlat {
            get { return Data.FinalFlat; }
        }

        public float FinalFactor {
            get { return Data.FinalFactor; }
        }

        public Modifier(ModifierData data, IRegistry registry) : base(data, registry) { }

        /// <summary>
        /// Calculate value for baseValue affected by modifier.
        /// </summary>
        public override float GetValue(float baseValue)
        {
            // todo: unit test type casting int-> float
            return ((baseValue + Data.RawFlat) * Data.RawFactor + Data.FinalFlat) * Data.FinalFactor;
        }
    }
}