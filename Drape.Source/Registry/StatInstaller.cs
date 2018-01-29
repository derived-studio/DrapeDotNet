using System.Collections.Generic;
using Drape.Interfaces;

namespace Drape
{
	/// <summary>
	/// It creates and installs stats into registry from given stat data
	/// </summary>
	/// <typeparam name="TStat"></typeparam>
	/// <typeparam name="TStatData"></typeparam>
	public class StatInstaller<TStat, TStatData> : IStatInstaller
		where TStat : IStat
		where TStatData : BaseStatData
	{
		private TStatData[] _statDataArr;

		public StatInstaller(TStatData[] statDataArr)
		{
			_statDataArr = statDataArr;
		}

		public void Install(Registry registry)
		{
			foreach (TStatData statData in _statDataArr) {
				TStat stat = (TStat)System.Activator.CreateInstance(typeof(TStat), new object[] { (TStatData)statData, registry });
				registry.Add<TStat>(stat);
			}
		}
	}
}