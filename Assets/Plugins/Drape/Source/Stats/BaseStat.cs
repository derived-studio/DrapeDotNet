using Drape.Interfaces;
using Drape.TinyJson;

namespace Drape
{
	public class BaseStat<TData> : IStat
		where TData : BaseStatData
	{
		protected TData _data;

		public string Code { get { return _data.code; } }

		public string Name { get { return _data.name; } }

		public int BaseValue { get { return _data.value; } }

		public virtual float Value { get { throw new System.NotImplementedException(); } }

		public BaseStat(TData data, IRegistry registry)
		{
			_data = data;
			registry.Add<BaseStat<TData>>(this);
		}

		/// <summary>
		/// Serializes stat to JSON using plane stat Data class
		/// </summary>
		/// <returns></returns>
		public virtual string ToJSON() { return _data.ToJson(); }
	}
}