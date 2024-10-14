
namespace MarsRover;

public static class MathExtentions
{
    public static byte Wrap(this byte value, byte min, byte max)
    {
        int range = max - min + 1;
        if (range <= 0) throw new ArgumentException("max must be greater than min.");

        return (byte)(((value - min) % range + range) % range + min);
    }
}
