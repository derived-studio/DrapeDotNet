using Drape.Slug;
namespace Drape
{
	[System.Serializable]
	public class ModifierData : BaseStatData
	{
		public string Stat { get; private set; }
		public int RawFlat { get; private set; }
		public float RawFactor { get; private set; }
		public int FinalFlat { get; private set; }
		public float FinalFactor { get; private set; }

		public ModifierData(
			string code,
			string name,
			string stat,
			int rawFlat,
			float rawFactor,
			int finalFlat,
			float finalFactor
		) : base(code, name, 0)
		{
			this.Stat = stat;
			this.RawFlat = rawFlat;
			this.RawFactor = rawFactor;
			this.FinalFlat = finalFlat;
			this.FinalFactor = finalFactor;
		}
	}
}