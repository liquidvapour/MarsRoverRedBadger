
namespace MarsRover;

public readonly record struct RoverResult(Rover Rover, RoverState RoverState);

public enum RoverState
{
    Lost,
    Active
}

public class Interpreter(TextReader reader)
{
    private readonly TextReader reader = reader;
    public World? World { get; set; } = null;

    public RoverResult[] DoIt()
    {

        string line = reader.ReadLine() ?? throw new InvalidOperationException("world setup line not found");
        World = BuildWorld(line);
        var width = World.WorldSize.Width;
        var height = World.WorldSize.Height;

        var nextLine = reader.ReadLine();
        var lostRovers = new List<Rover>();
        List<RoverResult> result = [];
        while (nextLine is not null)
        {
            var rover = BuildRover(nextLine);

            if (IsOutsideMap(rover, width, height))
            {
                throw new InvalidOperationException("rover outside of map");
            }
            var instructions = reader.ReadLine() ?? throw new InvalidOperationException("no instructions found");
            RoverResult roverResult = new(rover, RoverState.Active);
            for (int i = 0; i < instructions.Length; i++)
            {
                var oldRover = rover;
                var instruction = instructions[i];
                if (instruction == 'F' && lostRovers.Contains(rover))
                {
                    continue;
                }

                rover = rover.Process(instruction);
                if (IsOutsideMap(rover, width, height))
                {
                    roverResult = new RoverResult(oldRover, RoverState.Lost);
                    break;
                }
                roverResult = new RoverResult(rover, RoverState.Active);
            }
            if (roverResult.RoverState == RoverState.Lost)
            {
                lostRovers.Add(roverResult.Rover);
            }
            result.Add(roverResult);
            nextLine = reader.ReadLine();
        }
        return [.. result];
    }

    private static bool IsOutsideMap(Rover rover, int width, int height) => 
        rover is { X: var x, Y: var y } &&
        (x > width || x < 0 || y > height || y < 0);

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
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
}
