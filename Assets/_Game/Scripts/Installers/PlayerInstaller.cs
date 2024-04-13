using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId("PlayerPrefab").FromInstance(playerPrefab);
    }
}   