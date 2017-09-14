using Drape.Slug;
using System.Collections.Generic;

namespace Drape
{
	public class StatData : BaseStatData
	{
		[System.Serializable]
		public class Dependency
		{
			public string Code { get; private set; }
			public float Value { get; private set; }

			public Dependency(string code, float value)
			{
				this.Code = code;
				this.Value = value;
			}
		}

		public Dependency[] dependencies = new Dependency[0];

		public StatData(string code, string name, int value, Dictionary<string, float> dependencies) : base(code, name, value)
		{
			List<Dependency> list = new List<Dependency>();
			if (dependencies != null) {
				foreach (KeyValuePair<string, float> pair in dependencies) {
					list.Add(new Dependency(pair.Key, pair.Value));
				}
			}
			this.dependencies = list.ToArray();
		}
	}
}