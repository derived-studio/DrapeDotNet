using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
    public class Modifier : BaseStat<Modifier, ModifierData>, IStat
    {
        public IStat Stat { get; private set; }

        public int RawFlat {
            get { return _data.rawFlat; }
            internal set { _data.rawFlat = value; }
        }

        public float RawFactor {
            get { return _data.rawFactor; }
            internal set { _data.rawFactor = value; }
        }

        public int FinalFlat {
            get { return _data.finalFlat; }
            internal set { _data.finalFlat = value; }
        }

        public float FinalFactor {
            get { return _data.finalFactor; }
            internal set { _data.finalFactor = value; }
        }

        /// <summary>
        /// Relative modifier value calculated for base value equals 1.
        /// Shouldn't be used to anything else but comparing modifiers.
        /// </summary>
        public new float Value { get { return GetValue(1); } }

        public Modifier(string name, IStat stat, int rawFlat, float rawFactor, int finalFlat, float finalFactor) 
            : this(name.ToSlug(), name, stat, rawFlat, rawFactor, finalFlat, finalFactor) {}

        public Modifier(string code, string name, IStat stat, int rawFlat, float rawFactor, int finalFlat, float finalFactor) 
            : base(new ModifierData(code, name, stat.Name, rawFlat, rawFactor, finalFlat, finalFactor))
        {
            this.Stat = stat;
        }

        /// <summary>
        /// Calculate value for baseValue affected by modifier.
        /// </summary>
        public float GetValue(float baseValue)
        {
            // todo: unit test type casting int-> float
            return ((baseValue + _data.rawFlat) * _data.rawFactor + _data.finalFlat) * _data.finalFactor;
        }
    }
}