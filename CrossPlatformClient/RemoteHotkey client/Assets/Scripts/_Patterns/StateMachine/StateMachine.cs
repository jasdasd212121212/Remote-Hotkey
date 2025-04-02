using System.Linq;
using UnityEngine;

public class StateMachine : IReadOnlyStateMachine
{
    private State[] _states;

    private State _currentState;

    public IReadOnlyState CurrentState => _currentState;

    public StateMachine(params State[] states)
    {
        _states = states;
        ChangeState(_states[0]);
    }

    public void ChangeState(State state)
    {
        if (state == null)
        {
            Debug.LogError($"Critical error -> can`t change to null state");
            return;
        }

        if (!_states.Contains(state))
        {
            Debug.LogError($"Critical error -> can`t change to not registred state: {state}");
            return;
        }

        if (_currentState == state)
        {
            return;
        }

        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public IReadOnlyState GetState<T>() where T : State
    {
        foreach (State state in _states)
        {
            if (state.GetType() == typeof(T))
            {
                return state;
            }
        }

        Debug.LogError($"Critical error -> can`t get not registred state: {typeof(T)}");
        return null;
    }
}