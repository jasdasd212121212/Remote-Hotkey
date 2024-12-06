using RemoteHotkey.CommandLanguage;
using RemoteHotkey.CommandLanguage.SyntaxVisitor;
using RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions.__Base;

namespace RemoteHotkeyCore.CommandLanguage.CommandExpressions.Expressions;

public class LoopExpressionToken : IExpressionToken
{
    private CommandArgumentToken[] _arguments;
    private IToken[] _contaimendTokens;

    public string Name => "Loop";

    public CommandArgumentToken[] Arguments
    {
        get => _arguments;

        set
        {
            if (value[0] != null && int.TryParse(value[0].Argument, out int result) == false)
            {
                throw new ArgumentException(value[0].Argument);
            }

            _arguments = value;
        }
    }

    public LoopExpressionToken(IToken[] tokens)
    {
        _contaimendTokens = tokens;
    }

    public void Accept(ISyntaxVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IToken CreateNew()
    {
        return new LoopExpressionToken(_contaimendTokens);
    }

    public IToken[] ExtractTokens()
    {
        List<IToken> rawTokens = new List<IToken>();
        List<IToken> result = new List<IToken>();
        int.TryParse(Arguments[0].Argument, out int iterations);

        for (int i = 0; i < _contaimendTokens.Length; i++)
        {
            if (typeof(IExpressionToken).IsAssignableFrom(_contaimendTokens[i].GetType()))
            {
                rawTokens.AddRange((_contaimendTokens[i] as IExpressionToken).ExtractTokens());
            }
            else
            {
                rawTokens.Add(_contaimendTokens[i]);
            }
        }

        for (int i = 0; i < iterations; i++)
        {
            result.AddRange(rawTokens);
        }

        return result.ToArray();
    }

    public IToken[] GetRawTokens()
    {
        return _contaimendTokens;
    }
}