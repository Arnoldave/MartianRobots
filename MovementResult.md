# MovementResult Change Record

## Prompt

> For the Grid method `TryMoveRobot`, I would like to replace the boolean tuple return type, with the newly created `MovementResult` enum. Meaning of each value should be straight forward, but please ask if needed. Also please create a record of this prompt and its output, for the purpose of providing evidence.

## Output

`Grid.TryMoveRobot` now returns `MovementResult` rather than `(bool canMove, bool lost)`.

The enum values are used as follows:

- `MovementResult.CanMove`: the requested position is within the grid.
- `MovementResult.CannotMove`: the requested position is outside the grid, but a scent already exists at the robot's current position.
- `MovementResult.Lost`: the requested position is outside the grid and no scent exists, so the robot is lost.

`Robot.Move` now consumes the enum while preserving the existing behavior for position updates and lost state. Grid unit tests now assert the enum values directly.

## Validation

Validation completed successfully:

- The full solution build succeeded.
- All 9 `GridUnitTests` passed; 0 failed and 0 were skipped.
