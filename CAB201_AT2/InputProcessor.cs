using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_AT2.Obstacles;

namespace CAB201_AT2
{
    internal class InputProcessor
    {
        Map mapCreator = new Map();

        /// <summary>
        /// Method that reads the user's input and processes it using the InputReader method, then calls itself afterwards.
        /// </summary>
        public void StartReadInput()
        {
            Console.WriteLine("Enter command:");
            string input = Console.ReadLine();
            if (input != null)
            {
                InputReader(input);
                StartReadInput();
            }
            else
            {
                StartReadInput();
            }
        }
        /// <summary>
        /// Method to call the help text on application startup.
        /// </summary>
        public void PostHelpText()
        {
            Console.WriteLine(helpText);
        }

        // Method that splits the user's input by whitespace and executes different functions based on the first input argument.
        private void InputReader(string input)
        {
            input = input.ToLower();
            string[] inputArgs = input.Split(" ");

            switch (inputArgs[0])
            {
                // Creates a new obstacle based on the second argument and adds it to the obstacle list.
                case "add":
                    switch (inputArgs[1])
                    {
                        case "guard":
                            AddGuard(inputArgs);
                            break;

                        case "sensor":
                            AddSensor(inputArgs);
                            break;

                        case "camera":
                            AddCamera(inputArgs);
                            break;

                        case "fence":
                            AddFence(inputArgs);
                            break;
                        default:
                            Console.WriteLine("You need to specify an obstacle type.");
                            break;
                    }
                    break;
                case "check":
                    CheckCommand(inputArgs);
                    break;
                // Creates a visual text map displaying all obstacles in the specified region.
                case "map":
                    MapCommand(inputArgs);
                    break;
                case "path":
                    PathCommand(inputArgs);
                    break;
                // Reposts the help text.
                case "help":
                    Console.WriteLine(helpText);
                    break;
                // Exits out of the application.
                case "exit":
                    Console.WriteLine("Thank you for using the Threat-Master v1.62.");
                    Environment.Exit(0);
                    break;
                // Indicates to the user that their input isn't valid.
                default:
                    Console.WriteLine($"Invalid option: {inputArgs[0]}");
                    Console.WriteLine("Type 'help' to see a list of commands.");
                    break;
            }
        }

        private void AddGuard(string[] inputArgs)
        {
            // Checks if the right amount of arguments were provided.
            if (inputArgs.Count() != 4)
            {
                Console.WriteLine("Incorrect number of arguments.");
            }
            // Checks that the supplied arguments are Integers 
            else if (double.TryParse(inputArgs[2], out double x) && x % 1 != 0 || double.TryParse(inputArgs[3], out x) && x % 1 != 0)
            {
                Console.WriteLine("Coordinates are not valid integers.");
            }
            // Checks that the arguments can be successfully me parsed as Integers.
            else if (ValidateIntArgs(inputArgs[2], inputArgs[3]))
            {
                mapCreator.AddObstacle(new GuardObs(int.Parse(inputArgs[2]), int.Parse(inputArgs[3])));
                Console.WriteLine("Guard added.");
            }
            else
            {
                Console.WriteLine("X and Y positions are not valid.");
            }
        }

        private void AddSensor(string[] inputArgs)
        {
            // Checks if the right amount of arguments were provided.
            if (inputArgs.Count() != 5)
            {
                Console.WriteLine("Incorrect number of arguments.");
            }
            // Checks that the supplied arguments are Integers 
            else if (double.TryParse(inputArgs[2], out double x) && x % 1 != 0 || double.TryParse(inputArgs[3], out x) && x % 1 != 0)
            {
                Console.WriteLine("Coordinates are not valid integers.");
            }
            
            else if (ValidateIntArgs(inputArgs[2], inputArgs[3]) && ValidateDoubleArgs(inputArgs[4]))
            {
                mapCreator.AddObstacle(new SensorObs(int.Parse(inputArgs[2]), int.Parse(inputArgs[3]), double.Parse(inputArgs[4])));
                Console.WriteLine("Sensor added.");
            }
        }

        private void AddCamera(string[] inputArgs)
        {
            // Checks if the right amount of arguments were provided.
            if (inputArgs.Count() != 5)
            {
                Console.WriteLine("Incorrect number of arguments.");
            }
            // Checks that the supplied arguments are Integers 
            else if (double.TryParse(inputArgs[2], out double x) && x % 1 != 0 || double.TryParse(inputArgs[3], out x) && x % 1 != 0)
            {
                Console.WriteLine("Coordinates are not valid integers.");
            }
            // Checks that the arguments can be successfully me parsed as Integers.
            else if (ValidateIntArgs(inputArgs[2], inputArgs[3]))
            {
                string dir = ProcessDirectionInput(inputArgs[4]);
                if (dir.Contains("NOT VALID"))
                {
                    Console.WriteLine("Direction must be 'north', 'south', 'east' or 'west'.");
                }
                else
                {
                    mapCreator.AddObstacle(new CameraObs(int.Parse(inputArgs[2]), int.Parse(inputArgs[3]), int.Parse(dir)));
                    Console.WriteLine("Camera added.");
                }
            }
        }

        private void AddFence(string[] inputArgs)
        {
            // Checks if the right amount of arguments were provided.
            if (inputArgs.Count() != 6)
            {
                Console.WriteLine("Incorrect number of arguments.");
            }
            // Checks that the supplied arguments are Integers 
            else if (double.TryParse(inputArgs[2], out double x) && x % 1 != 0 || double.TryParse(inputArgs[3], out x) && x % 1 != 0)
            {
                Console.WriteLine("Coordinates are not valid integers.");
            }
            // Checks that the arguments can be successfully me parsed as Integers.
            else if (ValidateIntArgs(inputArgs[2], inputArgs[3]))
            {
                string dir = ProcessDirectionInput(inputArgs[4]);
                // Checks that the supplied orientation is either East or North.
                if (dir.Contains("NOT VALID"))
                {
                    Console.WriteLine("Orientation must be 'east' or 'north'.");
                }
                else if (int.Parse(dir) == (int)DirectionEnum.North || int.Parse(dir) == (int)DirectionEnum.East)
                {
                    int length;
                    if (int.TryParse(inputArgs[5], out length) && length < 0 || length > 0)
                    {
                        mapCreator.AddObstacle(new FenceObs(int.Parse(inputArgs[2]), int.Parse(inputArgs[3]), int.Parse(dir), length));
                        Console.WriteLine("Fence added.");
                    }
                    else Console.WriteLine("Length must be a valid integer greater than 0.");
                }
            }
        }

        private void CheckCommand(string[] inputArgs)
        {
            // Checks if the right amount of arguments were provided.
            if (inputArgs.Count() != 3)
            {
                Console.WriteLine("Incorrect number of arguments.");
            }
            // Checks that the supplied arguments are Integers 
            else if (double.TryParse(inputArgs[1], out double x) && x % 1 != 0 || double.TryParse(inputArgs[2], out x) && x % 1 != 0)
            {
                Console.WriteLine("Coordinates are not valid integers.");
            }
            // Checks that the arguments can be successfully me parsed as Integers.
            else if (ValidateIntArgs(inputArgs[1], inputArgs[2]))
            {
                new Checker(int.Parse(inputArgs[1]), int.Parse(inputArgs[2]));
            }
        }

        private void MapCommand(string[] inputArgs)
        {
            List<List<string>> visualMap = new List<List<string>>();
            // Checks if the right amount of arguments were provided.
            if (inputArgs.Count() != 5)
            {
                Console.WriteLine("Incorrect number of arguments.");
            }
            else if (double.TryParse(inputArgs[1], out double x) && x % 1 != 0 || double.TryParse(inputArgs[2], out x) && x % 1 != 0)
            {
                Console.WriteLine("Coordinates are not valid integers.");
            }
            else if (int.TryParse(inputArgs[3], out int k) && k <= 0 || int.TryParse(inputArgs[4], out k) && k <= 0)
            {
                Console.WriteLine("Width and height must be valid positive integers.");
            }
            else if (ValidateIntArgs(inputArgs[1], inputArgs[2]) && ValidateIntArgs(inputArgs[3], inputArgs[4]))
            {
                visualMap = mapCreator.GenerateVisualMap(int.Parse(inputArgs[1]), int.Parse(inputArgs[2]),
                    int.Parse(inputArgs[3]), int.Parse(inputArgs[4]));
                int mapHeight = int.Parse(inputArgs[4]);
                Console.WriteLine("Here is a map of obstacles in the selected region:");
                for (int i = mapHeight - 1; i >= 0; i--)
                {
                    foreach (List<string> mapY in visualMap)
                    {
                        Console.Write(mapY[i]);
                    }
                    Console.Write("\n");
                }
            }
        }

        private void PathCommand(string[] inputArgs)
        {
            if (inputArgs.Count() != 5)
            {
                Console.WriteLine("Incorrect number of arguments.");
            }
            else if (!ValidateIntArgs(inputArgs[1], inputArgs[2])) Console.WriteLine("Agent coordinates are not valid integers.");
            else if (!ValidateIntArgs(inputArgs[3], inputArgs[4])) Console.WriteLine("Objective coordinates are not valid integers.");
            else if (ValidateIntArgs(inputArgs[1], inputArgs[2]) && ValidateIntArgs(inputArgs[3], inputArgs[4]))
            {
                if (int.Parse(inputArgs[1]) == int.Parse(inputArgs[3]) && int.Parse(inputArgs[2]) == int.Parse(inputArgs[4]))
                {
                    Console.WriteLine("Agent, you are already at the objective.");
                }
                else if (mapCreator.CheckIfDanger(int.Parse(inputArgs[3]), int.Parse(inputArgs[4])))
                {
                    Console.WriteLine("The objective is blocked by an obstacle and cannot be reached.");
                }
                else
                {
                    Pathfinder pathfinder = new Pathfinder();
                    Tuple<Point, Point> startAndTarget = pathfinder.CreateStartAndTarget(int.Parse(inputArgs[1]), int.Parse(inputArgs[2]),
                    int.Parse(inputArgs[3]), int.Parse(inputArgs[4]));

                    List<Node> path = pathfinder.StartPath(startAndTarget.Item1, startAndTarget.Item2);
                    if (path.Count > 0) pathfinder.ProcessPath(path);
                }
            }
        }

        // Private method that tests if the inputs can successfully be parsed as an int.
        private bool ValidateIntArgs(string xArg, string yArg)
        {
            bool validArgs = int.TryParse(xArg, out int y);
            validArgs = int.TryParse(yArg, out y) && validArgs;

            return validArgs;
        }
        // Private method that tests if the input can successfully be parsed as a double.
        private bool ValidateDoubleArgs(string xArg)
        {
            bool validArgs = double.TryParse(xArg, out double x);
            return validArgs;
        }
        // Overload method that also tests if the fifth input can successfully be parsed as a double.
        private bool ValidateNumArgs(string xArg, string yArg, string radArg)
        {
            bool validArgs = int.TryParse(xArg, out int x);
            validArgs = int.TryParse(yArg, out int y) && validArgs;
            validArgs = double.TryParse(radArg, out double rad) && validArgs;
            return validArgs;
        }
        // Private method that checks the direction input and returns the direction's equivalent numeric value as a string.
        private string ProcessDirectionInput(string input)
        {
            switch (input)
            {
                case "north":
                    return $"{(int)DirectionEnum.North}";
                case "east":
                    return $"{(int)DirectionEnum.East}";
                case "south":
                    return $"{(int)DirectionEnum.South}";
                case "west":
                    return $"{(int)DirectionEnum.West}";
                default:
                    return "NOT VALID";
            }
        }

        // The help text as a constant string.
        const string helpText = "Valid commands are:\n" +
        "add guard <x> <y>: registers a guard obstacle\n" +
        "add fence <x> <y> <orientation> <length>: registers a fence obstacle. Orientation must be 'east' or 'north'.\n" +
        "add sensor <x> <y> <radius>: registers a sensor obstacle\n" +
        "add camera <x> <y> <direction>: registers a camera obstacle. Direction must be 'north', 'south', 'east' or 'west'.\n" +
        "check <x> <y>: checks whether a location and its surroundings are safe\n" +
        "map <x> <y> <width> <height>: draws a text-based map of registered obstacles\n" +
        "path <agent x> <agent y> <objective x> <objective y>: finds a path free of obstacles\n" +
        "help: displays this help message\n" +
        "exit: closes this program";
    }
}
