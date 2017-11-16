using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
	// Local stat is a variation of a Stat
	public class LocalStat : Stat, IStat
	{
		private List<Modifier> _modifiers = new List<Modifier>();
		private Dictionary<IStat, float> _dependencies = new Dictionary<IStat, float>();

		private Modifier _modTotals;
		private Stat _globalStat;

		private string ModTotalsName { get { return this.Name + " totals"; } }

		public LocalStat(StatData data, Stat globalStat) : base(data, globalStat.Registry)
		{
			_globalStat = globalStat;
		}

		/// <summary>
		/// Stat value after applying modifiers and dependencies.
		/// </summary>
		public override float GetValue(float baseValue)
		{
			float localValue = base.GetValue(baseValue);
			return _globalStat.GetValue(localValue);
		}
	}
}