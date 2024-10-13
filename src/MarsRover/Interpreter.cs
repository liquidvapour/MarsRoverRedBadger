
namespace MarsRover;

public class Interpreter(TextReader reader)
{
    private readonly TextReader reader = reader;
    public World? World { get; set; } = null;
    public Rover? Rover{ get; set; } = null;

    public void DoIt()
    {
        
        string line = reader.ReadLine() ?? throw new InvalidOperationException("world setup line not found");
        World = BuildWorld(line);

        line = reader.ReadLine() ?? throw new InvalidOperationException("rover setup missing");
        var rover =  BuildRover(line);

        var width = World.WorldSize.Width;
        var height = World.WorldSize.Height;
        if (rover is {X: var x, Y: var y} && 
            (x >= width || x < 0 || y >= height || y < 0))
        {
            throw new InvalidOperationException("rover outside of map");
        }

        var instructions = reader.ReadLine()?? throw new InvalidOperationException("no instructions found");
        for (int i = 0; i < instructions.Length; i++)
        {
            rover = rover.Process(instructions[i]);
        }
        Rover = rover;
    }

    private Rover BuildRover(string line)
    {
        var parts = line.Split(' ');
        return new Rover(int.Parse(parts[0]), int.Parse(parts[1]), parts[2][0]);
    }

    private World BuildWorld(string line) => new World { WorldSize = ReadWorldSize(line) };

    private (int Width, int Height) ReadWorldSize(string line)
    {
        var parts = line.Split(' ') 
            ?? throw new InvalidOperationException($"invalid First Line '{line}'");
        return (int.Parse(parts[0]), int.Parse(parts[0]));
    }
}
