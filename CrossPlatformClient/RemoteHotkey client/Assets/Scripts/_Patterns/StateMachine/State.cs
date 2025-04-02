using System;

public abstract class State : IReadOnlyState
{
    public event Action entered;
    public event Action exited;

    public void Enter()
    {
        OnEnter();
        entered?.Invoke();
    }

    public void Exit() 
    {
        OnExit();
        exited?.Invoke();
    }

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
}