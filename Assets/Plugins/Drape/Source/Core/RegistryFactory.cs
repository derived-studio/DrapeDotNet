using System.Collections;
using System.Collections.Generic;
using Drape.Interfaces;

namespace Drape
{
	public class RegistryFactory
	{
		private List<IInstaller> _installers;

		public RegistryFactory(List<IInstaller> installers)
		{
			_installers = installers;
		}

		public Registry Create()
		{
			Registry registry = new Registry();
			foreach (IInstaller installer in _installers) {
				installer.Install(registry);
			}
			return registry;
		}
	}
}