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

		public StatData.Dependency[] Dependencies
		{
			get
			{
				List<StatData.Dependency> dependencies = new List<StatData.Dependency>();
				// cast each element
				foreach (KeyValuePair<IStat, float> dependency in _dependencies) {
					dependencies.Add(new StatData.Dependency(dependency.Key.Code, dependency.Value));
				}
				return dependencies.ToArray();
			}
		}

		public bool ContainsDependency(string statCode)
		{
			IStat[] stats = new IStat[_dependencies.Count];
			_dependencies.Keys.CopyTo(stats, 0);
			foreach (IStat stat in stats) {
				if (stat.Code == statCode) {
					return true;
				}
			}
			return false;
		}

		public bool ContainsDependency(IStat stat)
		{
			return _dependencies.ContainsKey(stat);
		}

		public void AddDependency(IStat stat, float value)
		{
			if (_dependencies.ContainsKey(stat)) {
				throw new System.Exception("Stat " + stat.Name + " (code: " + stat.Code + ") already added as dependency to " + Name + " (code: " + Code + ")");
			}
			_dependencies.Add(stat, value);
		}

		public void RemoveDependency(string statCode)
		{
			IStat[] stats = new IStat[_dependencies.Count];
			_dependencies.Keys.CopyTo(stats, 0);
			foreach (IStat stat in stats) {
				if (stat.Code == statCode) {
					RemoveDependency(stat);
					return;
				}
			}
		}

		public void RemoveDependency(IStat stat)
		{
			_dependencies.Remove(stat);
		}

		public void UpdateDependency(string statCode, float value)
		{
			IStat[] stats = new IStat[_dependencies.Count];
			_dependencies.Keys.CopyTo(stats, 0);
			foreach (IStat stat in stats) {
				if (stat.Code == statCode) {
					UpdateDependency(stat, value);
					return;
				}
			}
		}

		public void UpdateDependency(IStat stat, float value)
		{
			_dependencies[stat] = value;
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
				RawFactor = 0,
				FinalFlat = 0,
				FinalFactor = 0
			}, null);

			if (_modifiers != null) {
				foreach (Modifier modifier in _modifiers) {
					AddModifierValues(modifier);
				}
			}
		}

		private void AddModifierValues(Modifier modifier)
		{
			int rawFlat = _modTotals.RawFlat + modifier.RawFlat;
			float rawFactor = _modTotals.RawFactor + modifier.RawFactor;
			int finalFlat = _modTotals.FinalFlat + modifier.FinalFlat;
			float finalFactor = _modTotals.FinalFactor + modifier.FinalFactor;

			_modTotals = new Modifier(new ModifierData() {
				Code = ModTotalsName.ToSlug(),
				Name = ModTotalsName,
				Stat = this.Code,
				RawFactor = rawFactor,
				RawFlat = rawFlat,
				FinalFlat = finalFlat,
				FinalFactor = finalFactor
			}, null);
		}
	}
}