using Drape.Interfaces;
using Drape.TinyJson;
using Drape.Exceptions;

namespace Drape
{
    class JSONInstaller<TStat, TStatData> : IInstaller
        where TStat : IStat
        where TStatData : BaseStatData
    {
        private TStatData[] statDataArr;
        public string JSON { get; private set; }

        public JSONInstaller(string jsonString)
        {
            jsonString = System.Text.RegularExpressions.Regex.Replace(jsonString, @"\s+", "");

            if (jsonString.StartsWith("[") && jsonString.EndsWith("]")) {
                try {
                    statDataArr = jsonString.FromJson<TStatData[]>();
                    JSON = jsonString;
                } catch (System.Exception) {
                    throw new InvalidJSONException("Couldn't parse JSON string", jsonString);
                }
            } else if (jsonString.StartsWith("{") && jsonString.EndsWith("}")) {
                throw new InvalidJSONException("JSON String is an object but should be an array", jsonString);
            }

            throw new InvalidJSONException("Couldn't parse JSON string", jsonString);
        }

        public void Install(Registry registry)
        {
            foreach (TStatData statData in statDataArr) {
                TStat stat = (TStat)System.Activator.CreateInstance(typeof(TStat), new object[] { (TStatData)statData, registry });
                registry.Add<TStat>(stat);
            }
        }
    }
}