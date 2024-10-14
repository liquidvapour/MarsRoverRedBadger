
using System.Collections.ObjectModel;

namespace MarsRover;

public class Interpreter(TextReader reader)
{
    private readonly TextReader reader = reader;
    public World? World { get; set; } = null;

    public RoverResult[] ProcessInput()
    {
        string worldSetup = reader.ReadLine() ?? throw new InvalidOperationException("world setup line not found");
        World = BuildWorld(worldSetup);
        
        var width = World.WorldSize.Width;
        var height = World.WorldSize.Height;

        var roverSetup = reader.ReadLine();
        var lostRovers = new List<Rover>();
        List<RoverResult> result = [];
        while (roverSetup is not null)
        {
            var rover = GetRover(roverSetup, width, height);
            var instructions = reader.ReadLine() ?? throw new InvalidOperationException("no instructions found");

            RoverResult roverResult = SendInstructionsToRover(rover, instructions, width, height, lostRovers.AsReadOnly());
            if (roverResult.RoverState == RoverState.Lost)
            {
                lostRovers.Add(roverResult.Rover);
            }
            result.Add(roverResult);
            roverSetup = reader.ReadLine();
        }
        return [.. result];
    }

    private Rover GetRover(string nextLine, int width, int height)
    {
        var rover = BuildRover(nextLine);

        if (IsOutsideMap(rover, width, height))
        {
            throw new InvalidOperationException("rover outside of map");
        }

        return rover;
    }

    private static RoverResult SendInstructionsToRover(Rover rover, string instructions, int width, int height, ReadOnlyCollection<Rover> lostRovers)
    {
        for (int i = 0; i < instructions.Length; i++)
        {
            var instruction = instructions[i];
            if (FoundScentOfLostRover(rover, lostRovers, instruction))
            {
                continue;
            }

            var oldRover = rover;
            rover = rover.Process(instruction);
            if (IsOutsideMap(rover, width, height))
            {
                return new RoverResult(oldRover, RoverState.Lost);
            }
        }

        return new RoverResult(rover, RoverState.Active);
    }

    private static bool FoundScentOfLostRover(Rover rover, ReadOnlyCollection<Rover> lostRovers, char instruction)
    {
        return instruction == 'F' && lostRovers.Contains(rover);
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
