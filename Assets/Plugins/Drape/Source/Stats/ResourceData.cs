using Drape.Slug;

namespace Drape
{
	[System.Serializable]
	public class ResourceData : BaseStatData
	{
		public string Capacity { get; private set; }
		public string Output { get; private set; }

		public ResourceData(string code, string name, int value, string capacity, string output)
			: base(code, name, value)
		{
			this.Capacity = capacity;
			this.Output = output;
		}
	}
}