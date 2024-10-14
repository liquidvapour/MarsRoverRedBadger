namespace MarsRover;

public readonly record struct Rover
{
    private const string Directions = "NESW";
    private static readonly (int X, int Y)[] directionVectors = [(0, 1), (1, 0), (0, -1), (-1, 0)];
    private static readonly (int X, int Y) NoMovement = (X: 0, Y: 0);

    private readonly int orientationIndex;

    public readonly int X { get; init; }
    public readonly int Y { get; init; }
    public readonly char Orientation { get => Directions[orientationIndex]; }

    public Rover(int x, int y, char orientation)
    {
        X = x;
        Y = y;
        orientationIndex = (byte)Directions.IndexOf(orientation);
    }
    public Rover(int x, int y, int orientationIndex)
    {
        X = x;
        Y = y;
        this.orientationIndex = orientationIndex;
    }

    public Rover Process(char instruction)
    {
        var vector = GetDirectionVector(instruction);

        return new Rover(
            X + vector.X,
            Y + vector.Y,
            GetNewOrientation(instruction, orientationIndex));
    }

    private (int X, int Y) GetDirectionVector(char instruction)
    {
        return instruction == 'F'
                    ? directionVectors[orientationIndex]
                    : NoMovement;
    }

    private int GetNewOrientation(char instruction, int orientationIndex) =>
        instruction switch
        {
            'R' => (orientationIndex += 1).Wrap(0, 3),
            'L' => (orientationIndex -= 1).Wrap(0, 3),
            _ => orientationIndex
        };
}
