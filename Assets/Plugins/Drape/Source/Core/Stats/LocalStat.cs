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

		private Stat _statRef;

		private string ModTotalsName { get { return this.Name + " totals"; } }

		public new IRegistry Registry { get { return _statRef.Registry; } }

		private static StatData LocalizeData(StatData data)
		{
			return new StatData() {
				Value = data.Value,
				Code = data.Code,
				Name = data.Name,
				Dependencies = null // removes global dependencies
			};
		}

		public LocalStat(Stat stat) : base(LocalizeData(stat.Data), null)
		{
			_statRef = stat;
		}

		/// <summary>
		/// Stat value after applying modifiers and dependencies.
		/// </summary>
		public override float GetValue(float baseValue)
		{
			float localValue = base.GetValue(baseValue);
			return _statRef.GetValue(localValue);
		}
	}
}