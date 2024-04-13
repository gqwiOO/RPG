using _Game.Scripts.Infrastructure.StateMachine;
using UnityEngine;

namespace _Game.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineHandler
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
