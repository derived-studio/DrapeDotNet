using Drape.Slug;
namespace Drape
{
	/// <summary>
	/// Plane data class facillitating stat serialization
	/// </summary>
	[System.Serializable]
	public class BaseStatData
	{
		private string _code;
		public string Code
		{
			get { return _code != null ? _code : Name.ToSlug(); }
			set { _code = value; }
		}

		public string Name { get; set; }
		public int Value { get; set; }
	}
}