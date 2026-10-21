namespace MartianRobots;

public class Grid
{
    public event Action<string>? RobotLost;

    private readonly int height;
    private readonly int width;
    private readonly Dictionary<(int, int), Robot> robots;
    private readonly HashSet<(int, int)> scentPositions;

    public Grid(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("Grid dimensions must be positive integers.");
        }
        this.width = width;
        this.height = height;
        robots = new Dictionary<(int, int), Robot>();
        scentPositions = [];
    }

    public void AddRobot(Robot robot)
    {
        ArgumentNullException.ThrowIfNull(robot);
        var position = (robot.X, robot.Y);
        if (robots.ContainsKey(position))
        {
            throw new InvalidOperationException($"A robot already exists at position {position}.");
        }
        robots[position] = robot;
        robot.MoveRequested += OnRobotMoved;
    }

    public void OnRobotMoved(Robot robot)
    {
        if (robot.X < 0 || robot.X > width || robot.Y < 0 || robot.Y > height)
        {
            var position = (robot.X, robot.Y);
            if (!scentPositions.Contains(position))
            {
                scentPositions.Add(position);
                RobotLost?.Invoke($"Robot lost at position {position}.");
            }
        }
    }
}
