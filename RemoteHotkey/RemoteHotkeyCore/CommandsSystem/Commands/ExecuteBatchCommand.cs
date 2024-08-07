using RemoteHotkey.CommandSystem;
using RemoteHotkey.InputsConstrollSystem;
using System.Diagnostics;

namespace RemoteHotkey.CommandsSystem;

public class ExecuteBatchCommand : IInputCommand
{
    private string[] _commands;

    private const string TEMP_FILE_PATCH = "D:/tempBatFile.bat";

    public ExecuteBatchCommand(string[] commands)
    {
        _commands = commands; 
    }

    public async void Execute(InputModel inputModel)
    {
        File.WriteAllLines(TEMP_FILE_PATCH, _commands);
        Process cmd = Process.Start("cmd.exe", $"/k \"{TEMP_FILE_PATCH}\"");

        await Task.Delay(_commands.Length);

        cmd.Kill();
        File.Delete(TEMP_FILE_PATCH);
    }
}