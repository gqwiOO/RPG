using _Game.Scripts.Infrastructure.StateMachine;

namespace _Game.Scripts.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineHandler coroutineHandler)
        {
            StateMachine = new( new SceneLoader(coroutineHandler));
        }
    }
}