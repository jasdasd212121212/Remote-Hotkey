using RemoteHotkey.CommandLanguage.SyntaxVisitor;

namespace RemoteHotkey.CommandLanguage;

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
                throw new ArgumentException();
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
        List<IToken> result = new List<IToken>();
        int.TryParse(Arguments[0].Argument, out int iterations);

        for (int i = 0; i < iterations; i++)
        {
            result.AddRange(_contaimendTokens);
        }

        return result.ToArray();
    }
}