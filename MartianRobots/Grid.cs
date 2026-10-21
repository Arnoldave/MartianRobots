namespace MartianRobots;

public class Grid
{
    private readonly int height;
    private readonly int width;
    private readonly Dictionary<Robot, (int, int)> robotPositions;
    private readonly HashSet<(int, int)> scentPositions;

    public Grid(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("Grid dimensions must be positive integers.");
        }
        this.width = width;
        this.height = height;
        robotPositions = [];
        scentPositions = [];
    }

    public void AddRobot(Robot robot)
    {
        ArgumentNullException.ThrowIfNull(robot);
        var position = (robot.X, robot.Y);

        if (IsOutOfBounds(position))
        {
            throw new ArgumentException($"Robot position {position} is out of grid bounds.");
        };
        if (robotPositions.ContainsValue(position))
        {
            throw new InvalidOperationException($"A robot already exists at position {position}.");
        }

        robotPositions[robot] = position;
        robot.AddToGrid(this);
    }

    public (bool canMove, bool lost) TryMoveRobot(Robot robot, (int X, int Y) newPosition)
    {
        ArgumentNullException.ThrowIfNull(robot);
        if (!robotPositions.ContainsKey(robot))
        {
            throw new InvalidOperationException("Robot not found in the grid.");
        }
        if (IsOutOfBounds(newPosition))
        {
            if (scentPositions.Contains((robot.X, robot.Y)))
            {
                return (false, false); // Robot cannot move but is not lost
            }
            else
            {
                scentPositions.Add((robot.X, robot.Y));
                robotPositions.Remove(robot);
                return (false, true); // Robot is lost
            }
        }
        robotPositions[robot] = newPosition;
        return (true, false); // Robot moved successfully
    }

    private bool IsOutOfBounds((int X, int Y) position)
    {
        return position.X < 0 || position.X > width || position.Y < 0 || position.Y > height;
    }
}
