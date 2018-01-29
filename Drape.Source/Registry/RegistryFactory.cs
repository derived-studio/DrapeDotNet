using System.Collections;
using System.Collections.Generic;
using Drape.Interfaces;

namespace Drape
{
	/// <summary>
	/// Registry factory class for creating populated stat registries
	/// from provided stat installer or list of installers. 
	/// </summary>
	public class RegistryFactory
	{
		private List<IStatInstaller> _installers;

		public RegistryFactory(IStatInstaller installer) : this(new List<IStatInstaller> { installer })
		{
		}

		public RegistryFactory(List<IStatInstaller> installers)
		{
			_installers = installers;
		}

		public Registry Create()
		{
			Registry registry = new Registry();
			foreach (IStatInstaller installer in _installers) {
				installer.Install(registry);
			}
			return registry;
		}
	}
}