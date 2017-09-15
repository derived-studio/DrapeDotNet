using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
    public class Modifier : BaseStat<ModifierData>, IStat
    {
        public string Stat {  get { return _data.Stat;  } }

        public int RawFlat {
            get { return _data.RawFlat; }
        }

        public float RawFactor {
            get { return _data.RawFactor; }
        }

        public int FinalFlat {
            get { return _data.FinalFlat; }
        }

        public float FinalFactor {
            get { return _data.FinalFactor; }
        }

        /// <summary>
        /// Relative modifier value calculated for base value equals 1.
        /// Shouldn't be used to anything else but comparing modifiers.
        /// </summary>
        public new float Value { get { return GetValue(1); } }

        public Modifier(ModifierData data, IRegistry registry) : base(data, registry) { }

        /// <summary>
        /// Calculate value for baseValue affected by modifier.
        /// </summary>
        public float GetValue(float baseValue)
        {
            // todo: unit test type casting int-> float
            return ((baseValue + _data.RawFlat) * _data.RawFactor + _data.FinalFlat) * _data.FinalFactor;
        }
    }
}