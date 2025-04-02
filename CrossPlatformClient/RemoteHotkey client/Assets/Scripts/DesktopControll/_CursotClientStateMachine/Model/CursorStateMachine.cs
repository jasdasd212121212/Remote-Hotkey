public class CursorStateMachine
{
    private CursorFreeState _free;
    private CursorLockState _lock;

    private StateMachine _stateMachine;

    public IReadOnlyState CurrentState => _stateMachine.CurrentState;
    public IReadOnlyStateMachine StateMachine => _stateMachine;

    public CursorStateMachine()
    {
        _free = new CursorFreeState();
        _lock = new CursorLockState();

        _stateMachine = new(_free, _lock);
    }

    public void Lock()
    {
        _stateMachine.ChangeState(_lock);
    }

    public void Unlock()
    {
        _stateMachine.ChangeState(_free);
    }
}