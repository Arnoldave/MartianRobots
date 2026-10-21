using AwesomeAssertions;

namespace MartianRobots.UnitTests;

public class RobotUnitTests
{
    [Fact]
    public void AddToGrid_Throws_ArgumentNullException_If_Grid_Is_Null()
    {
        var robot = new Robot(Orientation.N, 1, 1);

        var addToGridAction = () => robot.AddToGrid(null!);

        addToGridAction.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddToGrid_Does_Not_Throw_An_Exception_If_Grid_Is_Not_Null()
    {
        var robot = new Robot(Orientation.N, 1, 1);
        var grid = new Grid(3, 3);

        var addToGridAction = () => robot.AddToGrid(grid);

        addToGridAction.Should().NotThrow();
    }

    [Theory]
    [InlineData(Orientation.N, Orientation.W)]
    [InlineData(Orientation.W, Orientation.S)]
    [InlineData(Orientation.S, Orientation.E)]
    [InlineData(Orientation.E, Orientation.N)]
    public void ProcessInstruction_Turns_Robot_Left_If_Instruction_Is_L(Orientation initialOrientation, Orientation expectedOrientation)
    {
        var robot = new Robot(initialOrientation, 1, 1);

        robot.ProcessInstruction(Instruction.L);

        robot.Orientation.Should().Be(expectedOrientation);
    }

    [Theory]
    [InlineData(Orientation.N, Orientation.E)]
    [InlineData(Orientation.W, Orientation.N)]
    [InlineData(Orientation.S, Orientation.W)]
    [InlineData(Orientation.E, Orientation.S)]
    public void ProcessInstruction_Turns_Robot_Right_If_Instruction_Is_R(Orientation initialOrientation, Orientation expectedOrientation)
    {
        var robot = new Robot(initialOrientation, 1, 1);

        robot.ProcessInstruction(Instruction.R);

        robot.Orientation.Should().Be(expectedOrientation);
    }

    [Theory]
    [InlineData(Orientation.N, 1, 1, "1 2 N")]
    [InlineData(Orientation.E, 1, 1, "2 1 E")]
    [InlineData(Orientation.S, 1, 1, "1 0 S")]
    [InlineData(Orientation.W, 1, 1, "0 1 W")]
    public void ProcessInstruction_Moves_Robot_Forward_If_Instruction_Is_F_And_Robot_Would_Move_Within_Bounds(Orientation orientation, int initialX, int initialY, string expectedResult)
    {
        var robot = new Robot(orientation, initialX, initialY);
        var grid = new Grid(3, 3);
        grid.AddRobot(robot);

        var result = robot.ProcessInstruction(Instruction.F);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(Orientation.N, 1, 3, "1 4 N LOST")]
    [InlineData(Orientation.E, 3, 1, "4 1 E LOST")]
    [InlineData(Orientation.S, 1, 0, "1 -1 S LOST")]
    [InlineData(Orientation.W, 0, 1, "-1 1 W LOST")]
    public void ProcessInstruction_Moves_Robot_Forward_And_Marks_It_As_Lost_If_Instruction_Is_F_And_Robot_Would_Move_Out_Of_Bounds(Orientation orientation, int initialX, int initialY, string expectedResult)
    {
        var robot = new Robot(orientation, initialX, initialY);
        var grid = new Grid(3, 3);
        grid.AddRobot(robot);

        var result = robot.ProcessInstruction(Instruction.F);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(Orientation.N, 1, 3, "1 3 N")]
    [InlineData(Orientation.E, 3, 1, "3 1 E")]
    [InlineData(Orientation.S, 1, 0, "1 0 S")]
    [InlineData(Orientation.W, 0, 1, "0 1 W")]
    public void ProcessInstruction_Does_Not_Move_Robot_Forward_If_Instruction_Is_F_And_Robot_Would_Move_Out_Of_Bounds_But_A_Scent_Exists(Orientation orientation, int initialX, int initialY, string expectedResult)
    {
        var grid = new Grid(3, 3);

        // By creating a previous robot which starts at the same position, and makes the same movement, it will be lost, and the scent will be created on the grid
        var lostRobot = new Robot(orientation, initialX, initialY);
        grid.AddRobot(lostRobot);
        lostRobot.ProcessInstruction(Instruction.F);

        var robot = new Robot(orientation, initialX, initialY);
        grid.AddRobot(robot);

        var result = robot.ProcessInstruction(Instruction.F);

        result.Should().Be(expectedResult);
    }
}
