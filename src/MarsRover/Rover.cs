
namespace MarsRover;

public record struct Rover(int X, int Y, char Orientation)
{
    public Rover Process(char instruction)
    {

        var vector = instruction == 'F' ? GetDirectionVector(this.Orientation): (X: 0, Y: 0);
        return new Rover(this.X + vector.X, this.Y + vector.Y, GetNewOrientation(instruction, this.Orientation));
    }

    private static readonly Dictionary<char,char> right = new Dictionary<char, char> 
    { 
        ['N'] = 'E', 
        ['E'] = 'S', 
        ['S'] = 'W',
        ['W'] = 'N' 
    };

    private static readonly Dictionary<char,char> left = new Dictionary<char, char> 
    { 
        ['N'] = 'W', 
        ['W'] = 'S', 
        ['S'] = 'E',
        ['E'] = 'N' 
    };

    private char GetNewOrientation(char instruction, char orientation)
    {
        return instruction switch
        {
            'R' => right[orientation],
            'L' => left[orientation],
            _ => orientation
        };
    }

    private (int X, int Y) GetDirectionVector(char orientation)
    {
        return orientation switch
        {
            'N' => (0, 1),
            'S' => (0, -1),
            'W' => (-1, 0),
            'E' => (1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(orientation))
        };
        
    }
}