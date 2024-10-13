
namespace MarsRover;

public class Interpreter(TextReader reader)
{
    private readonly TextReader reader = reader;
    public World? World { get; set; } = null;

    public void DoIt()
    {
        string? line;
        do
        {
            line = reader.ReadLine();
            if (line is {})
                World = BuildWorld(line);
        }

        while (line is not null);
    }

    private World BuildWorld(string line) => new World { WorldSize = ReadWorldSize(line) };

    private (int Width, int Height) ReadWorldSize(string line)
    {
        var parts = line.Split(',') 
            ?? throw new InvalidOperationException($"invalid First Line '{line}'");
        return (int.Parse(parts[0]), int.Parse(parts[0]));
    }
}
