namespace MarsRover.Tests;

public class InterpreterTests
{
    [Test]
    public void CanSetupWorld()
    {

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.WriteLine("50, 50");
        writer.Flush();
        stream.Position = 0;
        using var input = new StreamReader(stream);
        var interpreter = new Interpreter(input);
        interpreter.DoIt();

        Assert.That(interpreter.World, Is.Not.Null);
        Assert.That(interpreter.World!.WorldSize, Is.EqualTo((Width: 50,Height: 50)));
    }
}
