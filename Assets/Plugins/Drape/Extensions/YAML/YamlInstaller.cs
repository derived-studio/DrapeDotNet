using System.Collections.Generic;
using Drape.Interfaces;

namespace Drape.Extensions.YAML
{
	public class YamlInstaller<TStat, TStatData> : IInstaller
		where TStat : IStat
		where TStatData : BaseStatData
	{
		private TStatData[] statDataArr;
		private YamlLoader yamlLoader = new YamlLoader();

		public YamlInstaller(string data)
		{
			List<TStatData> statData = new List<TStatData>();
			statData = yamlLoader.FromString<TStatData>(data);
			statDataArr = statData.ToArray();
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