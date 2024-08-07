namespace RemoteHotkey.CommandLanguage.Helpers;

public class ExpressionExtractor
{
    public string ExtractExpressions(string script, out string erasedScript)
    {
        erasedScript = "";

        string expression = "";

        bool adding = false;
        bool canBeAddingStart = true;

        for (int i = 0; i < script.Length; i++)
        {
            if (script[i] == CommandLexerConstants.EXPRESSION_START_SYMBOL && canBeAddingStart)
            {
                adding = true;
                canBeAddingStart = false;
            }

            if (adding == true)
            {
                expression += script[i];
            }
            else
            {
                erasedScript += script[i];
            }

            if (script[i] == CommandLexerConstants.EXPRESSION_BODY_END_SYMBOL)
            {
                adding = false;
            }
        }

        return expression;
    }
}