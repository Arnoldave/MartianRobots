namespace MartianRobots;

public class Robot
{
    private Grid? grid;

    public Robot(Orientation orientation, int x, int y)
    {
        Lost = false;
        Orientation = orientation;
        X = x;
        Y = y;
    }

    public bool Lost { get; private set; }
    public Orientation Orientation { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public void AddToGrid(Grid grid)
    {
        ArgumentNullException.ThrowIfNull(grid);
        this.grid = grid;
    }

    public string ProcessInstruction(Instruction instruction)
    {
        return instruction switch
        {
            Instruction.L => TurnLeft(),
            Instruction.R => TurnRight(),
            Instruction.F => MoveForward(),
            _ => throw new ArgumentException($"Invalid instruction: {instruction}"),
        };
    }

    private string TurnLeft()
    {
        Orientation = Orientation switch
        {
            Orientation.N => Orientation.W,
            Orientation.W => Orientation.S,
            Orientation.S => Orientation.E,
            Orientation.E => Orientation.N,
            _ => throw new ArgumentException($"Invalid orientation: {Orientation}")
        };

        return ToString();
    }

    private string TurnRight()
    {
        Orientation = Orientation switch
        {
            Orientation.N => Orientation.E,
            Orientation.E => Orientation.S,
            Orientation.S => Orientation.W,
            Orientation.W => Orientation.N,
            _ => throw new ArgumentException($"Invalid orientation: {Orientation}")
        };

        return ToString();
    }

    private string MoveForward()
    {
        var (x, y) = Orientation switch
        {
            Orientation.N => (X, Y + 1),
            Orientation.E => (X + 1, Y),
            Orientation.S => (X, Y - 1),
            Orientation.W => (X - 1, Y),
            _ => throw new ArgumentException($"Invalid orientation: {Orientation}")
        };

        return Move(x, y);
    }

    private string Move(int x, int y)
    {
        var (canMove, lost) = grid?.TryMoveRobot(this, (x, y)) ?? throw new InvalidOperationException("Robot is not on a grid.");

        Lost = lost;
        if (canMove)
        {
            X = x;
            Y = y;
        }

        return ToString();
    }

    public override string ToString()
    {
        return $"{X} {Y} {Orientation}{(Lost ? " LOST" : "")}";
    }
}