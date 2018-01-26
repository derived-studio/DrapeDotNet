using Drape.Slug;
using System.Collections.Generic;

namespace Drape
{
	public class StatData : BaseStatData
	{
		[System.Serializable]
		public class Dependency
		{
			public string Code { get; set; }
			public float Value { get; set; }

			public Dependency(string code, float value)
			{
				this.Code = code;
				this.Value = value;
			}
		}

		Dependency[] _dependencies = new Dependency[0];

		public Dependency[] Dependencies {
			get { return _dependencies; }
			set { _dependencies = value; }
		}
	}
}