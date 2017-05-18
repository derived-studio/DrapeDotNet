using Drape.Interfaces;

namespace Drape
{
    public class Modifier : Stat, IStat
    {
        public IStat stat { get; private set; }
        public ModifierProps props { get; private set; } 

        public Modifier(string name, IStat stat, ModifierProps props) : base(name, 1) {
            this.props = props;
            this.stat = stat;
        }

        /// <summary>
        /// Relative modifier value calculated for base value equals 1.
        /// Shouldn't be used to anything else but comparing modifiers.
        /// </summary>
        public new float Value { get { return props.GetValue(1); } }

        public struct ModifierProps
        {
            public readonly int rawFlat;
            public readonly float rawFactor;
            public readonly int finalFlat;
            public readonly float finalFactor;

            public ModifierProps(int rawFlat, float rawFactor, int finalFlat, float finalFactor)
            {
                this.rawFlat = rawFlat;
                this.rawFactor = rawFactor;
                this.finalFlat = finalFlat;
                this.finalFactor = finalFactor;
            }

            /// <summary>
            /// Calculate value for baseValue affected by modifier.
            /// </summary>
            public float GetValue(float baseValue)
            {
                // todo: unit test type casting int-> float
                return ((baseValue + rawFlat) * rawFactor + finalFlat) * finalFactor;
            }
        }
    }
}