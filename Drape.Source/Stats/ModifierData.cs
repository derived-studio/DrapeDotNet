using Drape.Slug;
namespace Drape
{
	[System.Serializable]
	public class ModifierData : BaseStatData
	{
		public string Stat { get; set; }
		public int RawFlat { get; set; }
		public float RawFactor { get; set; }
		public int FinalFlat { get; set; }
		public float FinalFactor { get; set; }
	}
}