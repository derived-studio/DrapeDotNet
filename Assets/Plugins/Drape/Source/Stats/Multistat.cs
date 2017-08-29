using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
	public class Multistat : BaseStat<Stat, MultistatData>, IStat
	{
		public IStat[] stats { get; private set; }

		public Multistat(MultistatData data, IRegistry registry) : base(data, registry)
		{
			if (data.stats != null && data.stats.Length > 0) {
				List<IStat> list = new List<IStat>();
				foreach (string code in data.stats) {
					list.Add(registry.Get<IStat>(code));
				}
				stats = list.ToArray();
			}
		}

		/// <summary>
		/// Value by default is a sum of all stats.
		/// </summary>
		public override float Value
		{
			get
			{
				float value = BaseValue;
				foreach (Stat stat in stats) {
					value += stat.Value;
				}
				return value;
			}
		}
	}
}