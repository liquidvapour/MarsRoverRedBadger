
namespace MarsRover;

public static class MathExtentions
{
    public static int Wrap(this int value, int min, int max)
    {
        int range = max - min + 1;
        if (range <= 0) throw new ArgumentException("max must be greater than min.");

        return (int)(((value - min) % range + range) % range + min);
    }
}
