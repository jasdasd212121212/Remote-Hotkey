namespace RemoteHotkey.CommandLanguage.Helpers;

public class ExpressionExtractor
{
    public string ExtractExpressions(string script, out string erasedScript, bool isGetParent)
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

            char required = isGetParent ? 'E' : CommandLexerConstants.EXPRESSION_BODY_END_SYMBOL;

            if (script[i] == /*CommandLexerConstants.EXPRESSION_BODY_END_SYMBOL*/ required)
            {
                adding = false;
            }
        }

        return expression;
    }

    public string ExtractExpressionBody(string expression, out string erased)
    {
        string extracted = "";
        erased = "";
        bool isAdding = false;

        for (int i = 0; i < expression.Length; i++)
        {
            if (isAdding == true)
            {
                extracted += expression[i];
            }
            else
            {
                erased += expression[i];
            }

            if (expression[i] == CommandLexerConstants.EXPRESSION_BODY_START_SYMBOL && isAdding == false)
            {
                isAdding = true;
            }
        }

        return extracted;
    }
}