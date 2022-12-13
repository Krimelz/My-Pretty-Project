using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private Transform spawnPoint;
    
        public override void InstallBindings()
        {
            Container.InstantiatePrefab(playerPrefab, spawnPoint.position, Quaternion.identity, null);
        }
    }
}