using RemoteHotkey.CommandSystem;

namespace RemoteHotkey.CommandLanguage;

public interface ICommandToken : IToken
{
    IInputCommand ConstructCommand();
}