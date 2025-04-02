using UnityEngine;

[CreateAssetMenu(fileName = "CursorStateMachineSettings", menuName = "Inputs/CursorStateMachineSettings")]
public class CursorStateMachineViewSettings : ScriptableObject
{
    [SerializeField] private KeyCode[] _exitKeyCodeCombination;

    public KeyCode[] ExitKeyCodeCombination => _exitKeyCodeCombination;
}