public abstract class CommandBase
{
    public abstract string Name { get; }
    public string Arguments;

    public string GetParsed()
    {
        return Name + Arguments;
    }
}