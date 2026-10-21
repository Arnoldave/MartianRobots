namespace MartianRobots.UnitTests;

public class RobotUnitTests
{
    /* Tests for AddToGrid method:
     * - Grid is null -> ArgumentNullException
     * - Robot is added to grid successfully -> no exception thrown
     */

    /* Tests for ProcessInstruction method:
     * - Instruction is null -> ArgumentNullException
     * - Instruction is invalid -> ArgumentException
     * - Instruction is valid -> Three cases:
     *    - L -> Returns new orientation and position unchanged
     *    - R -> Returns new orientation and position unchanged
     *    - F -> Three cases:
     *      - Robot can move forward -> Returns new position and orientation unchanged
     *      - Robot cannot move forward but is not lost (scent exists) -> Returns position and orientation unchanged
     *      - Robot can move forward but is lost (no scent) -> Returns position and orientation unchanged, Lost is true
     */
}
