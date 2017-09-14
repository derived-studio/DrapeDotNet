namespace Drape
{
	/// <summary>
	/// Plane data class facillitating stat serialization
	/// </summary>
	[System.Serializable]
	public class BaseStatData
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public int Value { get; set; }
	}
}