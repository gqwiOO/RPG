using _Game.Scripts.Infrastructure.StateMachine.States;
using _Game.Scripts.Services.Input;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Infrastructure.StateMachine
{
    class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private GameObject _playerPrefab;
        

        private readonly SceneLoader _sceneLoader;
        
        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load("Initial", EnterLoadScene);
            // Register Services
            

            CreatePlayer();
        }

        private void EnterLoadScene()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("Main");
        }

        private void CreatePlayer()
        {
            // var player = Object.Instantiate(_playerPrefab);
            //
            // #if UNITY_STANDALONE_WIN
            //     player.AddComponent<KeyboardInput>();
            // #else
            //     player.AddComponent<MobileInput>();
            // #endif
        }

        public void Exit()      
        {
            
        }
    }
}