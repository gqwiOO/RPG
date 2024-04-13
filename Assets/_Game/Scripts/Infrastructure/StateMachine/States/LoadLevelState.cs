using _Game.Scripts.Services.Input;
using Cinemachine;
using UnityEngine;

namespace _Game.Scripts.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IExitableState, IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private const string PLAYER_PREFAB_PATH = "Prefabs/Player";
        private const string CAMERA_PREFAB = "Prefabs/PlayerCamera";

        private GameObject _player;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string name)
        {
            _sceneLoader.Load(name, OnLoaded);
        }

        private void OnLoaded()
        {
            _player = Instantiate(PLAYER_PREFAB_PATH);
            _player.AddComponent<KeyboardInput>().playerModel = _player.transform.GetChild(0);
            CameraFollow();
            
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private void CameraFollow()
        {
            var camera = Instantiate(CAMERA_PREFAB);
            camera.GetComponentInChildren<CinemachineVirtualCamera>().Follow = _player.transform;
            camera.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = _player.transform.GetChild(0);
        }

        public void Exit()
        {
            
        }
    }
}