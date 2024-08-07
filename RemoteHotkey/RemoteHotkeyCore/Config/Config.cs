namespace RemoteHotkeyCore;

public class Config
{
    public string Ip { get; private set; }
    public string Username { get; private set; }

    public Config()
    {
        PathFinder pathFinder = new PathFinder();

        IEnumerable<string> config = File.ReadAllLines($"{pathFinder.PathToRoot}\\Config.txt");
        int currentIndex = 0;

        foreach (string line in config)
        {
            switch (currentIndex)
            {
                case 0:
                    Ip = line;
                    break;

                case 1:
                    Username = line;
                    break;

                default: throw new ArgumentException($"Undefinedline: {currentIndex}");
            }

            currentIndex++;
        }
    }
}