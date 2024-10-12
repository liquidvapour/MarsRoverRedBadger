namespace MarsRover.Tests;

public class Tests
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

    [Test]
    public void CanTurnRightFromNorthToEast()
    {
        var r = new Rover(5, 5, 'N');
        
        r = r.Process('R');

        Assert.That(r.X, Is.EqualTo(5));
        Assert.That(r.Y, Is.EqualTo(5));
        Assert.That(r.Orientation, Is.EqualTo('E'));

    }

    [Test]
    public void CanTurnRightFromEastToSouth()
    {
        var r = new Rover(5, 5, 'E');
        
        r = r.Process('R');

        Assert.That(r.X, Is.EqualTo(5));
        Assert.That(r.Y, Is.EqualTo(5));
        Assert.That(r.Orientation, Is.EqualTo('S'));

    }

    [Test]
    public void CanTurnRightFromSouthToWest()
    {
        var r = new Rover(5, 5, 'S');
        
        r = r.Process('R');

        Assert.That(r.X, Is.EqualTo(5));
        Assert.That(r.Y, Is.EqualTo(5));
        Assert.That(r.Orientation, Is.EqualTo('W'));

    }
    
    [Test]
    public void CanTurnRightFromWestToNorth()
    {
        var r = new Rover(5, 5, 'W');
        
        r = r.Process('R');

        Assert.That(r.X, Is.EqualTo(5));
        Assert.That(r.Y, Is.EqualTo(5));
        Assert.That(r.Orientation, Is.EqualTo('N'));

    }
}