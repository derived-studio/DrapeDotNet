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

		public BaseStatData(string code, string name, int value)
		{
			this.Code = code;
			this.Name = name;
			this.Value = value;
		}
	}
}