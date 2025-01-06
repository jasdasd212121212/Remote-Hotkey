namespace RemoteHotkey.CommandLanguage;

public class CommandArgumentToken
{
    public string Argument { get; private set; }

    public CommandArgumentToken(string argument)
    {
        Argument = argument;
    }
}