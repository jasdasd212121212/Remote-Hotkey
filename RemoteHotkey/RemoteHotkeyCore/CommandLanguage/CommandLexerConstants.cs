namespace RemoteHotkey.CommandLanguage;

public static class CommandLexerConstants
{
    public static readonly char SEPARATING_SYMBOL = ';';

    public static readonly char ARGUMENT_SECTION_START_CHAR = '(';
    public static readonly char ARGUMENT_SECTION_END_CHAR = ')';

    public static readonly char ARGUMENT_SEPARATING_SYMBOL = ',';

    public static readonly char EXPRESSION_START_SYMBOL = '$';
    public static readonly char EXPRESSION_BODY_START_SYMBOL = '{';
    public static readonly char EXPRESSION_BODY_END_SYMBOL = '}';
}