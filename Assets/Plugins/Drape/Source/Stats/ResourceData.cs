using Drape.Slug;

namespace Drape
{
	[System.Serializable]
	public class ResourceData : BaseStatData
	{
		public string Capacity { get; set; }
		public string Output { get; set; }
	}
}