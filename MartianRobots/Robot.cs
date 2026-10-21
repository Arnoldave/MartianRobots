namespace MartianRobots;

public class Robot
{
    public event Action<Robot>? MoveRequested;

    private Orientation Orientation { get; set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public Robot(Orientation orientation, int x, int y)
    {
        Orientation = orientation;
        X = x;
        Y = y;
    }

    public void ProcessInstruction(Instruction instruction)
    {
        switch (instruction)
        {
            case Instruction.L:
                TurnLeft();
                break;
            case Instruction.R:
                TurnRight();
                break;
            case Instruction.F:
                MoveForward();
                break;
            default:
                throw new ArgumentException($"Invalid instruction: {instruction}");
        }
    }

    private void TurnLeft()
    {
        Orientation = Orientation switch
        {
            Orientation.N => Orientation.W,
            Orientation.W => Orientation.S,
            Orientation.S => Orientation.E,
            Orientation.E => Orientation.N,
            _ => throw new ArgumentException($"Invalid orientation: {Orientation}")
        };
    }

    private void TurnRight()
    {
        Orientation = Orientation switch
        {
            Orientation.N => Orientation.E,
            Orientation.E => Orientation.S,
            Orientation.S => Orientation.W,
            Orientation.W => Orientation.N,
            _ => throw new ArgumentException($"Invalid orientation: {Orientation}")
        };
    }

    private void MoveForward()
    {
        var (x, y) = Orientation switch
        {
            Orientation.N => (X, Y + 1),
            Orientation.E => (X + 1, Y),
            Orientation.S => (X, Y - 1),
            Orientation.W => (X - 1, Y),
            _ => throw new ArgumentException($"Invalid orientation: {Orientation}")
        };

        MoveRequested?.Invoke(this);
    }
}