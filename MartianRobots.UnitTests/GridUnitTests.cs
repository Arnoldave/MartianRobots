using AwesomeAssertions;

namespace MartianRobots.UnitTests;

public class GridUnitTests
{
    [Fact]
    public void AddRobot_Throws_ArgumentNullException_If_Robot_Is_Null()
    {
        var grid = new Grid(5, 5);

        var addRobotAction = () => grid.AddRobot(null!);

        addRobotAction.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddRobot_Throws_ArgumentException_If_Robot_Position_Is_Out_Of_Bounds()
    {
        var grid = new Grid(5, 5);
        var robot = new Robot(Orientation.N, 6, 6);

        var addRobotAction = () => grid.AddRobot(robot);

        addRobotAction.Should().Throw<ArgumentException>().WithMessage("Robot position (6, 6) is out of grid bounds.");
    }

    [Fact]
    public void AddRobot_Throws_InvalidOperationException_If_Robot_Position_Is_Already_Occupied()
    {
        var grid = new Grid(5, 5);
        var robot1 = new Robot(Orientation.N, 2, 2);
        var robot2 = new Robot(Orientation.E, 2, 2);

        grid.AddRobot(robot1);

        var addRobotAction = () => grid.AddRobot(robot2);

        addRobotAction.Should().Throw<InvalidOperationException>().WithMessage("A robot already exists at position (2, 2).");
    }

    [Fact]
    public void AddRobot_Adds_Robot_Successfully()
    {
        var grid = new Grid(5, 5);
        var robot = new Robot(Orientation.N, 2, 2);

        var addRobotAction = () => grid.AddRobot(robot);

        addRobotAction.Should().NotThrow();

        // Verify that the robot is in the grid and its position is correct
        var result = grid.TryMoveRobot(robot, (2, 2));
        result.Should().Be(MovementResult.CanMove);
    }

    [Fact]
    public void TryMoveRobot_Throws_ArgumentNullException_If_Robot_Is_Null()
    {
        var grid = new Grid(5, 5);

        var tryMoveAction = () => grid.TryMoveRobot(null!, (1, 1));

        tryMoveAction.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void TryMoveRobot_Throws_InvalidOperationException_If_Robot_Is_Not_In_Grid()
    {
        var grid = new Grid(5, 5);
        var robot = new Robot(Orientation.N, 1, 1);

        var tryMoveAction = () => grid.TryMoveRobot(robot, (1, 2));

        tryMoveAction.Should().Throw<InvalidOperationException>().WithMessage("Robot not found in the grid.");
    }

    [Fact]
    public void TryMoveRobot_Disallows_Movement_If_Robot_Would_Move_Out_Of_Bounds_And_Scent_Exists()
    {
        var grid = new Grid(5, 5);
        var robot1 = new Robot(Orientation.N, 5, 5);

        grid.AddRobot(robot1);
        
        // Move robot1 out of bounds to create a scent
        robot1.ProcessInstruction(Instruction.F);
        
        var robot2 = new Robot(Orientation.N, 5, 5);
        grid.AddRobot(robot2);

        // Now try to move robot2 out of bounds where the scent exists
        var result = grid.TryMoveRobot(robot2, (5, 6));

        result.Should().Be(MovementResult.CannotMove);
    }

    [Fact]
    public void TryMoveRobot_Allows_Movement_But_Marks_Robot_As_Lost_If_Robot_Would_Move_Out_Of_Bounds_And_Scent_Does_Not_Exist()
    {
        var grid = new Grid(5, 5);
        var robot = new Robot(Orientation.N, 5, 5);

        grid.AddRobot(robot);

        var result = grid.TryMoveRobot(robot, (5, 6));

        result.Should().Be(MovementResult.Lost);
    }

    [Fact]
    public void TryMoveRobot_Allows_Movement_If_Robot_Would_Move_Within_Bounds()
    {
        var grid = new Grid(5, 5);
        var robot = new Robot(Orientation.N, 5, 5);

        grid.AddRobot(robot);

        var result = grid.TryMoveRobot(robot, (5, 4));

        result.Should().Be(MovementResult.CanMove);
    }
}
