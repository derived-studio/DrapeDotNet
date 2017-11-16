using Drape.Interfaces;

namespace Drape
{
	public class BaseStat<TData> : IStat
		where TData : BaseStatData
	{
		public TData Data { get; protected set; }
		public IRegistry Registry { get; protected set; }

		public string Code { get { return Data.Code; } }
		public string Name { get { return Data.Name; } }

		public int BaseValue { get; set; }

		// should be equivalent to GetValue() with no parameter
		public virtual float Value { get { return GetValue(); } }
		public virtual float GetValue() { return GetValue(BaseValue); }
		public virtual float GetValue(float baseValue) { return baseValue; }

		public BaseStat(TData data, IRegistry registry)
		{
			Data = data;
			Registry = registry; 
			BaseValue = Data.Value;
			registry.Add<BaseStat<TData>>(this);
		}

		public override string ToString()
		{
			return base.ToString() + " (code: " + Code + ", name: " + Name + ")";
		}

	}
}