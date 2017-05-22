using Drape.Interfaces;
using Drape.TinyJson;

namespace Drape
{
    class JSONInstaller<TStat, TStatData> : IInstaller
        where TStat : IStat
        where TStatData : BaseStatData
    {

        private TStatData[] statDataArr;
        public string JSON { get; private set; }

        public JSONInstaller(string json)
        {
            JSON = json;
            statDataArr = JSON.FromJson<TStatData[]>();
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