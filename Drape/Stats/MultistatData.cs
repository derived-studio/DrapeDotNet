namespace Drape
{
	[System.Serializable]
	public class MultistatData : BaseStatData
	{
		private string[] _stats;
		public string[] Stats
		{
			get { return _stats; }
			set { _stats = value; }
		}
	}
}