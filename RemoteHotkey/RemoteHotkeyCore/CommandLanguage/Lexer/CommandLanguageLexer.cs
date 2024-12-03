using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;
using RemoteHotkeyCore.CommandLanguage.Lexer.Rounds;
using RemoteHotkeyCore.InputsController.Controllers;

namespace RemoteHotkey.CommandLanguage;

public class CommandLanguageLexer
{
    private ICommandToken[] _commandTokens;

    private CommandLexingRound _commandLexer;
    private ExpressionLexingRoundBase[] _expressionLexers;
    private ExpressionsPrepareRound _expressionsPrepareRound;

    public CommandLanguageLexer(ICommandToken[] commandTokens, IExpressionToken[] expressions, KeyboardController keyboardController)
    {
        _commandTokens = commandTokens;

        _commandLexer = new CommandLexingRound(commandTokens);
        _expressionsPrepareRound = new ExpressionsPrepareRound(expressions);

        _expressionLexers = new ExpressionLexingRoundBase[]
        {
            new LoopsLexingRound(expressions, _commandLexer),
            new LoopCheckKeyboardButtonLexingRound(expressions, _commandLexer, keyboardController)
        };
    }

    public IToken[] Tokenize(string script)
    {
        List<IToken> result = new List<IToken>();

        _expressionsPrepareRound.TryTokenize(script, out script, out IToken[] r);
        int expressionsCount = script.Count(current => current == CommandLexerConstants.EXPRESSION_START_SYMBOL);

        expressionsCount = expressionsCount == 0 ? 1 : expressionsCount + 1;

        for (int i = 0; i < expressionsCount; i++)
        {
            bool commandsCompileResult = _commandLexer.TryTokenize(script, out script, out IToken[] commandTokens);
            result.AddRange(commandTokens);

            foreach (ExpressionLexingRoundBase expressionLexer in _expressionLexers)
            {
                if (expressionLexer.TryTokenize(script, out string newScript, out IToken[] expressions))
                {
                    script = newScript;
                    result.AddRange(expressions);

                    break;
                }
            }
        }

        return result.ToArray();
    }
}