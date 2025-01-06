namespace RemoteHotkey.CommandLanguage.Helpers;

public class ArgumentExtractor
{
    public CommandArgumentToken[] GetArguments(string command)
    {
        string[] stringArguments = GetStringArguments(command);

        stringArguments[stringArguments.Length - 1] = stringArguments[stringArguments.Length - 1].Split(CommandLexerConstants.ARGUMENT_SECTION_END_CHAR)[0].Trim();
        CommandArgumentToken[] argumentsToken = new CommandArgumentToken[stringArguments.Length];

        for (int i = 0; i < stringArguments.Length; i++)
        {
            argumentsToken[i] = new CommandArgumentToken(stringArguments[i]);
        }

        return argumentsToken;
    }

    private string[] GetStringArguments(string command)
    {
        return command.Split(CommandLexerConstants.ARGUMENT_SECTION_START_CHAR)[1].Trim().
            Split(CommandLexerConstants.ARGUMENT_SEPARATING_SYMBOL);
    }
}