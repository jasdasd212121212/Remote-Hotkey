public class ArgumentsBuilder
{
    private string _currentArgumentSctring = "";

    private const char ARGUMENTS_SEPARATOR_CHAR = ',';
    private const char ARGUMENTS_START_CHAR = '(';
    private const char ARGUMENTS_END_CHAR = ')';
    private const char COMMAND_END_CHAR = ';';

    public ArgumentsBuilder AddArgument(string argument)
    {
        _currentArgumentSctring += $"{(_currentArgumentSctring == "" ? ARGUMENTS_START_CHAR : ARGUMENTS_SEPARATOR_CHAR)} {argument}";

        return this;
    }

    public void Reset()
    {
        _currentArgumentSctring = "";
    }

    public string GetBuildedArgumentsString()
    {
        return _currentArgumentSctring + $"{ARGUMENTS_END_CHAR}{COMMAND_END_CHAR}";
    }
}