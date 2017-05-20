using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
    public class Stat: BaseStat<Stat, StatData>, IStat
    {
        private List<Modifier> _modifiers = new List<Modifier>();
        private Dictionary<IStat, float> _dependencies = new Dictionary<IStat, float>();

        private Modifier.ModifierProps _modTotals;

        public Stat(StatData data) : base(data) { }
        public Stat(string name) : this(name.ToSlug(), name, 0) { }
        public Stat(string name, Dictionary<IStat, float> dependencies = null) : this(name.ToSlug(), name, 0, dependencies) { }
        public Stat(string name, int value, Dictionary<IStat, float> dependencies = null) : this(name.ToSlug(), name, value, dependencies) { }
        public Stat(string code, string name, int value, Dictionary<IStat, float> dependencies = null) : base(new StatData(code, name, value))
        {

            // process for serialization
            if (dependencies != null) { 
                List<StatData.Dependency> deps = new List<StatData.Dependency>();
                foreach(KeyValuePair<IStat, float> dep in dependencies) {
                    deps.Add(new StatData.Dependency() {
                        code = dep.Key.Code,
                        value = dep.Value
                    });
                }

                _data.dependencies = deps.ToArray();
            }

            ResetModifierTotals();
        }

        /// <summary>
        /// Stat value after applying modifiers and dependencies.
        /// </summary>
        public override float Value {
            get {
                float depsValue = 0;
                // todo: is there more efficient way?
                foreach (KeyValuePair<IStat, float> entry in _dependencies) {
                    IStat stat = entry.Key;
                    float factor = entry.Value;
                    depsValue += stat.Value * factor;
                }
                float baseValue = base.BaseValue + depsValue;
                float value = _modTotals.GetValue(baseValue);
                return value;
            }
        }

        public StatData.Dependency[] dependencies { get { return _data.dependencies; } }

        public void AddMod(Modifier mod)
        {
            if (mod.Stat.Name != this.Name) {
                string e = System.String.Format("Mod type mismatch. Modifier \"{0}\" for  stat: \"{1}\" is not allowed by stat: \"{2}\"", mod.Name, mod.Stat.Name, this.Name);
                throw new System.Exception(e);
            }
            _modifiers.Add(mod);
            AddModifierValues(mod);
        }

        public void RemoveMod(Modifier mod)
        {
            _modifiers.Remove(mod);
            ResetModifierTotals();
        }

        public void ClearMods()
        {
            _modifiers.Clear();
            ResetModifierTotals();
        }

        private void ResetModifierTotals()
        {
            _modTotals = new Modifier.ModifierProps(0, 1, 0, 1);
            if (_modifiers != null) {
                foreach (Modifier modifier in _modifiers) {
                    AddModifierValues(modifier);
                }
            }
        }

        private void AddModifierValues(Modifier modifier)
        {
            _modTotals = new Modifier.ModifierProps(
                _modTotals.rawFlat + modifier.props.rawFlat,
                _modTotals.rawFactor * (1 + modifier.props.rawFactor),
                _modTotals.finalFlat + modifier.props.finalFlat,
                _modTotals.finalFactor * (1 + modifier.props.finalFactor)
            );
        }
    }
}