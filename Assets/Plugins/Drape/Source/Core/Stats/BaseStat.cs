using Drape.Interfaces;

namespace Drape
{
	public class BaseStat<TData> : IStat
		where TData : BaseStatData
	{
		protected TData _data;
		protected IRegistry _registry;

		public string Code { get { return _data.Code; } }

		public string Name { get { return _data.Name; } }
		
		public int BaseValue { get; set; }

		public virtual float Value { get { throw new System.NotImplementedException(); } }

		public BaseStat(TData data, IRegistry registry)
		{
			_data = data;
			_registry = registry; 
			BaseValue = _data.Value;
			registry.Add<BaseStat<TData>>(this);
		}

		public override string ToString()
		{
			return base.ToString() + " (code: " + Code + ", name: " + Name + ")";
		}

	}
}