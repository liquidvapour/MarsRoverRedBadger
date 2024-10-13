namespace MarsRover.Tests;

public class InterpreterTests
{
    [Test]
    public void CanSetupWorld()
    {

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        
        writer.WriteLine("50 30");
        writer.WriteLine("1 1 N");
        writer.WriteLine("");
        
        writer.Flush();
        stream.Position = 0;
        using var input = new StreamReader(stream);
        var interpreter = new Interpreter(input);
        interpreter.ProcessInput();

        Assert.That(interpreter.World, Is.Not.Null);
        Assert.That(interpreter.World!.WorldSize, Is.EqualTo((Width: 50,Height: 30)));
    }

    [Test]
    public void CanRunInstructions()
    {

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        
        writer.WriteLine("5 3");
        writer.WriteLine("1 1 E");
        writer.WriteLine("RFRFRFRF");
        writer.WriteLine("3 2 N");
        writer.WriteLine("FRRFLLFFRRFLL");
        writer.WriteLine("0 3 W");
        writer.WriteLine("LLFFFLFLFL");
        writer.Flush();
        stream.Position = 0;
        using var input = new StreamReader(stream);
        var interpreter = new Interpreter(input);
        var result = interpreter.ProcessInput();

        Assert.That(result[0].Rover, Is.EqualTo(new Rover(1, 1, 'E')));
        Assert.That(result[1].Rover, Is.EqualTo(new Rover(3, 3, 'N')));
        Assert.That(result[1].RoverState, Is.EqualTo(RoverState.Lost));
        Assert.That(result[2].Rover, Is.EqualTo(new Rover(2, 3, 'S')));
    }


    [Test]
    public void CanSetupRover()
    {

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        
        writer.WriteLine("50 50");
        writer.WriteLine("1 1 N");
        writer.WriteLine("");
        writer.Flush();
        stream.Position = 0;
        using var input = new StreamReader(stream);
        var interpreter = new Interpreter(input);
        var result = interpreter.ProcessInput();

        Assert.That(result[0].Rover, Is.EqualTo(new Rover(1, 1, 'N')));
    }

    [Test]
    public void CanSetupWorldWithInstructions()
    {

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        
        writer.WriteLine("50 50");
        writer.WriteLine("1 1 N");
        writer.WriteLine("F");
        
        writer.Flush();
        stream.Position = 0;
        using var input = new StreamReader(stream);
        var interpreter = new Interpreter(input);
        var result = interpreter.ProcessInput();

        Assert.That(result[0].Rover, Is.EqualTo(new Rover(1, 2, 'N')));
    }

    [Test]
    public void SetupRoverOutsideMapFails()
    {

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        
        writer.WriteLine("10 10");
        writer.WriteLine("15 15 N");
        writer.WriteLine("F");
        writer.Flush();
        stream.Position = 0;
        using var input = new StreamReader(stream);
        var interpreter = new Interpreter(input);
        
        
        Assert.Throws<InvalidOperationException>(() => interpreter.ProcessInput());

    }

    [Test]
    public void SetupRoverOutsideMapFailsBottomLeft()
    {

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);

        writer.WriteLine("10 10");
        writer.WriteLine("-10 -10 N");
        writer.WriteLine("F");
        writer.Flush();
        stream.Position = 0;
        using var input = new StreamReader(stream);
        var interpreter = new Interpreter(input);

        Assert.Throws<InvalidOperationException>(() => interpreter.ProcessInput());

    }


    [Test]
    public void SetupRoverOutsideMapFailsTooFarLeft()
    {

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);

        writer.WriteLine("10 10");
        writer.WriteLine("-1 5 N");
        writer.WriteLine("F");
        writer.Flush();
        stream.Position = 0;
        using var input = new StreamReader(stream);
        var interpreter = new Interpreter(input);

        Assert.Throws<InvalidOperationException>(() => interpreter.ProcessInput());

    }
}
