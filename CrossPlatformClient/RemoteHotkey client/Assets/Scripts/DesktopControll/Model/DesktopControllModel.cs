using UnityEngine;

public class DesktopControllModel
{
    private ClientPresenter _presenter;
    private ArgumentsBuilder _argumentsBuilder;

    private CommandBase[] _commands;

    public DesktopControllModel(ClientPresenter presenter, params CommandBase[] commands)
    {
        _presenter = presenter;
        _commands = commands;

        _argumentsBuilder = new ArgumentsBuilder();
    }

    public void ExecuteCommand<TCommand>(params string[] arguments) where TCommand : CommandBase
    {
        CommandBase command = FindCommand<TCommand>();

        command.Arguments = BuildArguments(arguments);
        string buildedCommand = command.GetParsed();

        _presenter.SendCustomScript(buildedCommand);
    }

    private CommandBase FindCommand<TCommand>() where TCommand : CommandBase
    {
        foreach (CommandBase command in _commands)
        {
            if (typeof(TCommand) == command.GetType())
            {
                return command;
            }
        }

        Debug.LogError($"Critical error -> unable to find command: {typeof(TCommand)}");
        return null;
    }

    private string BuildArguments(string[] arguments)
    {
        _argumentsBuilder.Reset();

        foreach (string argument in arguments)
        {
            _argumentsBuilder.AddArgument(argument);
        }

        return _argumentsBuilder.GetBuildedArgumentsString();
    }
}