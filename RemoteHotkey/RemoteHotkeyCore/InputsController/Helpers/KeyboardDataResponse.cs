namespace RemoteHotkeyCore.InputsController.Helpers;

public struct KeyboardDataResponse
{
    public KeyboardOperationCode OperationCode;
    public string Key;

    public KeyboardDataResponse(KeyboardOperationCode operationCode, string key)
    {
        OperationCode = operationCode; 
        Key = key;
    }
}