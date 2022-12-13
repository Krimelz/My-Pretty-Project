using UnityEngine;
using Zenject;

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