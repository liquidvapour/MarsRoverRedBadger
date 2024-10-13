namespace MarsRover.Tests;

public class RoverTests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void CanMoveNorth()
    {
        var r = new Rover(0, 0, 'N');
        
        r = r.Process('F');

        Assert.That(r.X, Is.EqualTo(0));
        Assert.That(r.Y, Is.EqualTo(1));
    }

    [Test]
    public void CanMoveSouth()
    {
        var r = new Rover(5, 5, 'S');
        
        r = r.Process('F');

        Assert.That(r.X, Is.EqualTo(5));
        Assert.That(r.Y, Is.EqualTo(4));
    }

    [Test]
    public void CanMoveWest()
    {
        var r = new Rover(5, 5, 'W');
        
        r = r.Process('F');

        Assert.That(r.X, Is.EqualTo(4));
        Assert.That(r.Y, Is.EqualTo(5));
    }

    [Test]
    public void CanMoveEast()
    {
        var r = new Rover(5, 5, 'E');
        
        r = r.Process('F');

        Assert.That(r.X, Is.EqualTo(6));
        Assert.That(r.Y, Is.EqualTo(5));
    }

    [Test, Sequential]
    public void RoverCanTurnRight(
        [Values('N', 'E', 'S', 'W')]char startOrientation,
        [Values('R', 'R', 'R', 'R')]char instruction, 
        [Values('E', 'S', 'W', 'N')]char endOrientation)
    {
        var x = 5;
        var y = 5;
        var r = new Rover(x, y, startOrientation);
        
        r = r.Process(instruction);

        Assert.That(r.X, Is.EqualTo(x));
        Assert.That(r.Y, Is.EqualTo(y));
        Assert.That(r.Orientation, Is.EqualTo(endOrientation));

    }

    [Test, Sequential]
    public void RoverCanTurnLeft(
        [Values('N', 'E', 'S', 'W')]char startOrientation,
        [Values('L', 'L', 'L', 'L')]char instruction, 
        [Values('W', 'N', 'E', 'S')]char endOrientation)
    {
        var x = 5;
        var y = 5;
        var r = new Rover(x, y, startOrientation);
        
        r = r.Process(instruction);

        Assert.That(r.X, Is.EqualTo(x));
        Assert.That(r.Y, Is.EqualTo(y));
        Assert.That(r.Orientation, Is.EqualTo(endOrientation));

    }
}