using System.Collections.ObjectModel;

namespace MarsRover;

public class Interpreter(TextReader reader)
{
    public World? World { get; private set; }

    public RoverResult[] ProcessInput()
    {
        var worldSetup = reader.ReadLine() ?? throw new InvalidOperationException("world setup line not found");
        var world = BuildWorld(worldSetup);
        World = world;
        
        var (width, height) = world.WorldSize;

        var roverSetup = reader.ReadLine();
        var lostRovers = new List<Rover>();
        List<RoverResult> roverFinalStates = [];
        while (roverSetup is not null)
        {
            var rover = GetRover(roverSetup, width, height);
            var instructions = reader.ReadLine() ?? throw new InvalidOperationException("no instructions found");

            RoverResult roverResult = SendInstructionsToRover(rover, instructions, width, height, lostRovers.AsReadOnly());
            if (roverResult.RoverState == RoverState.Lost)
            {
                lostRovers.Add(roverResult.Rover);
            }
            roverFinalStates.Add(roverResult);
            roverSetup = reader.ReadLine();
        }
        return [.. roverFinalStates];
    }

    private Rover GetRover(string nextLine, int width, int height)
    {
        var rover = BuildRover(nextLine);

        return !IsOutsideMap(rover, width, height) 
            ? rover
            : throw new InvalidOperationException("rover outside of map");;
    }

    private static RoverResult SendInstructionsToRover(Rover rover, string instructions, int width, int height, ReadOnlyCollection<Rover> lostRovers)
    {
        foreach (var instruction in OnlyGoodInstructions(rover, instructions, lostRovers))
        {
            var oldRover = rover;
            rover = rover.Process(instruction);
            if (IsOutsideMap(rover, width, height))
            {
                return new RoverResult(oldRover, RoverState.Lost);
            }
        }

        return new RoverResult(rover, RoverState.Active);
    }

    private static IEnumerable<char> OnlyGoodInstructions(in Rover rover, string instructions, ReadOnlyCollection<Rover> lostRovers)
    {
        var copyRover = rover;
        return instructions.Where(i => !(i == Instructions.Forward && FoundScentOfLostRover(copyRover, lostRovers)));
    }

    private static bool FoundScentOfLostRover(in Rover rover, ReadOnlyCollection<Rover> lostRovers)
    {
        return lostRovers.Contains(rover);
    }

    private static bool IsOutsideMap(Rover rover, int width, int height) => 
        rover is { X: var x, Y: var y } &&
        (x > width || x < 0 || y > height || y < 0);

    private Rover BuildRover(string line)
    {
        var parts = line.Split(' ');
        return new Rover(int.Parse(parts[0]), int.Parse(parts[1]), parts[2][0]);
    }

    private World BuildWorld(string line) => new(ReadWorldSize(line));

    private (int Width, int Height) ReadWorldSize(string line)
    {
        var parts = line.Split(' ') 
            ?? throw new InvalidOperationException($"invalid First Line '{line}'");
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
}
