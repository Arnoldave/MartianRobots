namespace MartianRobots.UnitTests;

public class GridUnitTests
{
    /* Tests for AddRobot method:
     * - Robot is null -> ArgumentNullException
     * - Robot position is out of bounds -> ArgumentException
     * - Robot position is already occupied -> InvalidOperationException
     * - Robot is added successfully -> Robot is in grid and position is correct
     */

    /* Tests for TryMoveRobot method:
     * - Robot is null -> ArgumentNullException
     * - Robot is not in the grid -> InvalidOperationException
     * - New position is out of bounds and scent exists -> (false, false)
     * - New position is out of bounds and no scent exists -> (false, true) and robot is removed
     * - New position is valid -> (true, false) and robot position is updated
     */
}
