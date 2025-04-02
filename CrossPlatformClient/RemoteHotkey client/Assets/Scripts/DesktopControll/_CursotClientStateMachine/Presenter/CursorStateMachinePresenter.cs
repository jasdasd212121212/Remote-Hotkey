public class CursorStateMachinePresenter
{
    private CursorStateMachine _model;
    private IReadOnlyState _lockState;

    public bool IsLocked => _model.CurrentState == _lockState;

    public CursorStateMachinePresenter(CursorStateMachine model)
    {
        _model = model;
        _lockState = _model.StateMachine.GetState<CursorLockState>();
    }

    public void Lock() => _model.Lock();
    public void Unlock() => _model.Unlock();
}