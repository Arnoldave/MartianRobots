using MartianRobots;

var inputLines = await File.ReadAllLinesAsync("input.txt");

var gridSize = inputLines[0].Split(' ');
var grid = new Grid(int.Parse(gridSize[0]), int.Parse(gridSize[1]));
var robotInstructions = new Dictionary<Robot, string>();

for (var i = 1; i < inputLines.Length; i += 2)
{
    var robotPosition = inputLines[i].Split(' ');

    var x = int.Parse(robotPosition[0]);
    var y = int.Parse(robotPosition[1]);
    var orientation = Enum.Parse<Orientation>(robotPosition[2]);

    var robot = new Robot(orientation, x, y);
    grid.AddRobot(robot);
}

var outputLines = new List<string>();

foreach (var (robot, instructions) in robotInstructions)
{
    var output = "";

    foreach (var instructionChar in instructions)
    {
        var instruction = Enum.Parse<Instruction>(instructionChar.ToString());
        output = robot.ProcessInstruction(instruction);
        if (output.EndsWith("LOST"))
        {
            break;
        }
    }

    outputLines.Add(output);
}

Console.WriteLine(string.Join(Environment.NewLine, outputLines));
