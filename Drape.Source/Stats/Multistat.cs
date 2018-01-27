using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
	public class Multistat : BaseStat<MultistatData>, IStat
	{
		public IStat[] Stats { get; private set; }

		public Multistat(MultistatData data, IRegistry registry) : base(data, registry)
		{
			if (data.Stats != null && data.Stats.Length > 0) {
				List<IStat> list = new List<IStat>();
				foreach (string code in data.Stats) {
					list.Add(registry.Get<IStat>(code));
				}
				Stats = list.ToArray();
			}
		}

		public override float GetValue(float baseValue)
		{
			float value = baseValue;
			foreach (Stat stat in Stats) {
				value += stat.GetValue(baseValue);
			}
			return value;
		}
	}
}