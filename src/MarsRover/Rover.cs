namespace MarsRover;

public readonly record struct Rover
{
    private const string DIRECTIONS = "NESW";
    private static readonly (int X, int Y)[] directionVectors = [(0, 1), (1, 0), (0, -1), (-1, 0)];

    private readonly byte orientationIndex;

    public readonly int X { get; init; }
    public readonly int Y { get; init; }
    public readonly char Orientation { get => DIRECTIONS[orientationIndex]; }

    public Rover(int x, int y, char orientation)
    {
        X = x;
        Y = y;
        orientationIndex = (byte)DIRECTIONS.IndexOf(orientation);
    }
    public Rover(int x, int y, byte orientationIndex)
    {
        X = x;
        Y = y;
        this.orientationIndex = orientationIndex;
    }

    public Rover Process(char instruction)
    {
        var vector = instruction == 'F'
            ? directionVectors[orientationIndex]
            : (X: 0, Y: 0);

        return new Rover(
            X + vector.X,
            Y + vector.Y,
            GetNewOrientation(instruction, orientationIndex));
    }

    private byte GetNewOrientation(char instruction, byte orientationIndex) =>
        instruction switch
        {
            'R' => (orientationIndex += 1).Wrap(0, 3),
            'L' => (orientationIndex -= 1).Wrap(0, 3),
            _ => orientationIndex
        };
}
