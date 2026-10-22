# Project Description

This project is a programmatic representation of a fleet of robots traversing the surface of Mars, represented in the form of a 2 dimensional grid.
Inputs are represented in the following form:

```
10 10
1 1 N
RLFFLR
```

Where the first line represents the size of the grid that represents the surface of Mars, the second line the starting position and orientation of the robot, and the third line the set of instructions that it follows. Lines 2 and 3 can be repeated any number of times with new values to introduce additional robots to the simulation.

Outputs are represented in the following form:

```
4 3 S
11 10 E LOST
1 1 N
```

Each line represents the final position of each robot, returned in the same order that the robots are initialized in the input. If a robot has moved beyond the bounds of the grid, it is marked as lost, and this will be outlined in the output.

# Running the Project

To run the project, simply do one of the following:

1) Open the solution in the Integrated Development Environment (IDE) of your choice (E.G. Visual Studio, Rider), and run the `MartianRobots` project
2) Run the following in the commandline:

```
cd {Install Location}/MartianRobots/MartianRobots // e.g. cd C://Users/Dan/source/repos/MartianRobots/MartianRobots
dotnet run MartianRobots.csproj
```

## Configuring the input

Due to time constraints, the only way to configure the input at this time is to open the file `input.txt`, located at the same directory as the project file.
Given more time to work on the project further, I would implement the ability to provide an input file path as an argument when running the application.

## Running Tests

To run the unit tests in the command line, run the following:

```
cd {Install Location}/MartianRobots/MartianRobots.UnitTests // e.g. cd C://Users/Dan/source/repos/MartianRobots/MartianRobots.UnitTests
dotnet test MartianRobots.UnitTests.csproj
```

# Technical Decisions



# Taking the Solution further



# AI Usage

Visual Studio's inbuilt Copilot tools were used to assist in development. For the most part, it was used in the form of the file editor auto-complete tool, as I would begin implementing a method or property, it would predictively complete the implementation. I would then review that suggestion, and make changes whenever I felt appropriate.

In some cases, I would also use the agent chat client, usually to ask specific questions, akin to how a developer would look up a StackOverflow thread in the past. For example, I asked for a reminder of how delegates were declared, when I was considering them for an event based implementation.

In one case, I asked the agent to implement a small change for me, specifically changing the return type of a method from two booleans, to an enum type, and fixing the resultant syntax errors. The evidence of this interaction has been committed to the repo.
