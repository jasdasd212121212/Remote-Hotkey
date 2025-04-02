public interface IReadOnlyStateMachine
{
    IReadOnlyState GetState<T>() where T : State;
}