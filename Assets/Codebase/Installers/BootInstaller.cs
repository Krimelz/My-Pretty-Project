using Codebase.Services;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
	public class BootInstaller : MonoInstaller
	{
		[SerializeField]
		private InputService inputServicePrefab;

		public override void InstallBindings()
		{
			BindInputService();
		}

		private void BindInputService()
		{
			Container
				.Bind<IInputService>()
				.To<InputService>()
				.FromComponentInNewPrefab(inputServicePrefab)
				.AsSingle()
				.NonLazy();
		}
	}
}