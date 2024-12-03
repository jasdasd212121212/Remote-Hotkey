using RemoteHotkey.CommandLanguage.Helpers;

namespace RemoteHotkey.CommandLanguage;

public class CommandLexingRound : LexingRoundBase
{
    private ArgumentExtractor _argumentExtractor;

    public CommandLexingRound(IToken[] tokens) : base(tokens)
    {
        _argumentExtractor = new ArgumentExtractor();
    }

    public override bool TryTokenize(string script, out string erasedScript, out IToken[] result)
    {
        List<IToken> commandTokens = new List<IToken>();

        script = script.Trim();
        string[] commands = script.Split(CommandLexerConstants.SEPARATING_SYMBOL);

        for (int i = 0; i < commands.Length; i++)
        {
            if (commands[i].Contains(CommandLexerConstants.EXPRESSION_START_SYMBOL))
            {
                string usedCommands = "";

                for (int j = 0; j <= i; j++)
                {
                    if (commands[j].Contains(CommandLexerConstants.EXPRESSION_START_SYMBOL) == false)
                    {
                        usedCommands += commands[j];
                    }
                }

                result = commandTokens.ToArray();

                script = script.Trim();
                erasedScript = script.Remove(0, usedCommands.Length + i);

                return false;
            }

            commandTokens.AddRange(TokenizeCommand(commands[i]));
        }

        result = commandTokens.ToArray();
        erasedScript = script;

        return true;
    }

    private IToken[] TokenizeCommand(string command)
    {
        List<IToken> result = new List<IToken>();
        string commandName = command.Split(CommandLexerConstants.ARGUMENT_SECTION_START_CHAR)[0].Trim();

        foreach (ICommandToken expression in Tokens)
        {
            IToken token = expression.CreateNew();

            if (token.Name == commandName)
            {
                result.Add(token);
                token.Arguments = _argumentExtractor.GetArguments(command);
            }
        }

        return result.ToArray();
    }
}