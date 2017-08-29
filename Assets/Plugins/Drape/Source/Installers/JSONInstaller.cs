using Drape.Interfaces;
using Drape.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Drape
{
	class JSONInstaller<TStat, TStatData> : IInstaller
		where TStat : IStat
		where TStatData : BaseStatData
	{
		private TStatData[] statDataArr;
		public string JSON { get; private set; }

		private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};

		public JSONInstaller(string jsonString)
		{
			jsonString = System.Text.RegularExpressions.Regex.Replace(jsonString, @"\s+", "");

			if (jsonString.StartsWith("[") && jsonString.EndsWith("]")) {
				try {
					statDataArr = JsonConvert.DeserializeObject<TStatData[]>(jsonString, Settings);
				} catch (System.Exception e) {
					throw new InvalidJSONException("Couldn't parse JSON string for: ", jsonString + ", exception: " + e.ToString());
				}
			} else if (jsonString.StartsWith("{") && jsonString.EndsWith("}")) {
				throw new InvalidJSONException("JSON String is an object but should be an array", jsonString);
			} else {
				throw new InvalidJSONException("Couldn't parse JSON string", jsonString);
			}

			JSON = jsonString;
		}

		public void Install(Registry registry)
		{
			foreach (TStatData statData in statDataArr) {
				TStat stat = (TStat)System.Activator.CreateInstance(typeof(TStat), new object[] { (TStatData)statData, registry });
				registry.Add<TStat>(stat);
			}
		}

		public class LowercaseContractResolver : DefaultContractResolver
		{
			protected override string ResolvePropertyName(string propertyName)
			{
				return propertyName.ToLower();
			}
		}

	
	}
}