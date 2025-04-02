using System;

public interface IReadOnlyState
{
    event Action entered;
    event Action exited;
}