namespace MarsRover.Tests;

public class InterpreterTests
{
    [Test]
    public void CanSetupWorld()
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
        interpreter.DoIt();

        Assert.That(interpreter.World, Is.Not.Null);
        Assert.That(interpreter.World!.WorldSize, Is.EqualTo((Width: 50,Height: 50)));
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
        interpreter.DoIt();

        Assert.That(interpreter.Rover, Is.Not.Null);
        Assert.That(interpreter.Rover!, Is.EqualTo(new Rover(1, 1, 'N')));
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
        interpreter.DoIt();

        Assert.That(interpreter.Rover, Is.Not.Null);
        Assert.That(interpreter.Rover!, Is.EqualTo(new Rover(1, 2, 'N')));
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
        
        
        Assert.Throws<InvalidOperationException>(() => interpreter.DoIt());

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

        Assert.Throws<InvalidOperationException>(() => interpreter.DoIt());

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

        Assert.Throws<InvalidOperationException>(() => interpreter.DoIt());

    }
}
