using MarsRover;

var interp = new Interpreter(Console.In);

var output = interp.DoIt();
foreach (var item in output)
{
    var isLost = item.RoverState == RoverState.Lost ? "LOST":"";
    Console.WriteLine($"{item.Rover.X} {item.Rover.Y} {item.Rover.Orientation} {isLost}");
}