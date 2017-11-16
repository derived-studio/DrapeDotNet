using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
	public class Stat : BaseStat<StatData>, IStat
	{
		private List<Modifier> _modifiers = new List<Modifier>();
		private Dictionary<IStat, float> _dependencies = new Dictionary<IStat, float>();

		private Modifier _modTotals;

		private string ModTotalsName { get { return this.Name + " totals"; } }

		public Stat(StatData data, IRegistry registry) : base(data, registry)
		{
			// process for serialization
			if (data.Dependencies != null) {
				foreach (StatData.Dependency dep in data.Dependencies) {
					IStat stat = registry.Get<IStat>(dep.Code);
					_dependencies.Add(stat, dep.Value);
				}
			}

			ResetModifierTotals();
		}

		/// <summary>
		/// Stat value after applying modifiers and dependencies.
		/// </summary>
		public override float GetValue(float baseValue)
		{
			float depsValue = 0;
			foreach (KeyValuePair<IStat, float> entry in _dependencies) {
				IStat stat = entry.Key;
				float factor = entry.Value;
				depsValue += stat.Value * factor;
			}
			return _modTotals.GetValue(baseValue + depsValue);
		}

		/// <summary>
		/// Returns number of modifiers
		/// </summary>
		public float ModifierCount
		{
			get
			{
				return _modifiers.Count;
			}
		}

		public StatData.Dependency[] Dependencies { get { return Data.Dependencies; } }

		public void AddModifier(Modifier modifier)
		{
			if (modifier.Stat != this.Code) {
				string e = System.String.Format("Mod type mismatch. Modifier \"{0}\" for  stat: \"{1}\" is not allowed by stat: \"{2}\"", modifier.Name, modifier.Stat, this.Name);
				throw new System.Exception(e);
			}
			_modifiers.Add(modifier);
			AddModifierValues(modifier);
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
			_modTotals = new Modifier(new ModifierData() {
				Code = ModTotalsName.ToSlug(),
				Name = ModTotalsName,
				Stat = this.Code,
				RawFlat = 0,
				RawFactor = 1,
				FinalFlat = 0,
				FinalFactor = 1
			}, Registry);

			if (_modifiers != null) {
				foreach (Modifier modifier in _modifiers) {
					AddModifierValues(modifier);
				}
			}
		}

		private void AddModifierValues(Modifier modifier)
		{
			int rawFlat = _modTotals.RawFlat + modifier.RawFlat;
			float rawFactor = _modTotals.RawFactor * (1 + modifier.RawFactor);
			int finalFlat = _modTotals.FinalFlat + modifier.FinalFlat;
			float finalFactor = _modTotals.FinalFactor * (1 + modifier.FinalFactor);

			_modTotals = new Modifier(new ModifierData() {
				Code = ModTotalsName.ToSlug(),
				Name = ModTotalsName,
				Stat = this.Code,
				RawFactor = rawFactor,
				RawFlat = rawFlat,
				FinalFlat = finalFlat,
				FinalFactor = finalFactor
			}, Registry);
		}
	}
}