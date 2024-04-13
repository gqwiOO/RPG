using System;
using System.Collections.Generic;
using _Game.Scripts.Infrastructure.StateMachine.States;

namespace _Game.Scripts.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type,IExitableState> _states;

        private IExitableState _currentState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoaded>(TPayLoaded payload) where TState : class, IPayLoadedState<TPayLoaded>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState :class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        public void Exit()
        {
            
        }
    }
}