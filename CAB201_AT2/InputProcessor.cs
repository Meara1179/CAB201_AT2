using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2
{
    internal class InputProcessor
    {
        Obstacle obCreator = new Obstacle();
        Map mapCreator = new Map();

        /// <summary>
        /// Method that reads the user's input and processes it using the InputReader method, then calls itself afterwards.
        /// </summary>
        public void StartReadInput()
        {
            Console.WriteLine("Please input command...");
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
                            if (ValidateNumArgs(inputArgs[2], inputArgs[3]))
                            {
                                mapCreator.AddObstacle(obCreator.CreateGuard(int.Parse(inputArgs[2]), int.Parse(inputArgs[3])));
                                Console.WriteLine("Guard added.");
                            }
                            else Console.WriteLine("X and Y positions are not valid.");
                            break;

                        case "sensor":
                            if (ValidateNumArgs(inputArgs[2], inputArgs[3], inputArgs[4]))
                            {
                                mapCreator.AddObstacle(obCreator.CreateSensor(int.Parse(inputArgs[2]), int.Parse(inputArgs[3]), double.Parse(inputArgs[4])));
                                Console.WriteLine("Sensor added.");
                            }
                            break;

                        case "camera":
                            if (ValidateNumArgs(inputArgs[2], inputArgs[3]))
                            {
                                string dir = ProcessDirectionInput(inputArgs[4]);
                                if (dir.Contains("NOT VALID"))
                                {
                                    Console.WriteLine("Direction must be 'north', 'south', 'east' or 'west'.");
                                }
                                else
                                {
                                    mapCreator.AddObstacle(obCreator.CreateCamera(int.Parse(inputArgs[2]), int.Parse(inputArgs[3]), int.Parse(inputArgs[4])));
                                    Console.WriteLine("Camera added.");
                                }
                            }
                            break;

                        case "fence":
                            if (ValidateNumArgs(inputArgs[2], inputArgs[3]))
                            {
                                string dir = ProcessDirectionInput(inputArgs[4]);
                                if (dir.Contains("NOT VALID"))
                                {
                                    Console.WriteLine("Direction must be 'north' or 'east'.");
                                }
                                else if (int.Parse(dir) != (int)DirectionEnum.North || int.Parse(dir) != (int)DirectionEnum.East)
                                {
                                    Console.WriteLine("Direction must be 'north' or 'east'.");
                                }
                                else
                                {
                                    int length;
                                    if (int.TryParse(inputArgs[5], out length))
                                    {
                                        mapCreator.AddFence(obCreator.CreateFence(int.Parse(inputArgs[2]), int.Parse(inputArgs[3]), int.Parse(dir), length));
                                        Console.WriteLine("Fence added.");
                                    }
                                    else Console.WriteLine("Length not valid.");
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("You need to specify an obstacle type.");
                            break;
                    }
                    break;
                case "check":
                    // TODO Implement Check method in Map
                    if (ValidateNumArgs(inputArgs[1], inputArgs[2]))
                    {
                        new Checker(int.Parse(inputArgs[1]), int.Parse(inputArgs[2]), int.Parse(inputArgs[1]), int.Parse(inputArgs[2]), 
                            int.Parse(inputArgs[1]), int.Parse(inputArgs[2]), null, 0);
                    }
                    break;
                // Creates a visual text map displaying all obstacles in the specified region.
                case "map":
                    List<List<string>> visualMap = new List<List<string>>();
                    if (ValidateNumArgs(inputArgs[1], inputArgs[2]) && ValidateNumArgs(inputArgs[3], inputArgs[4]))
                    {
                        visualMap = mapCreator.GenerateVisualMap(int.Parse(inputArgs[1]), int.Parse(inputArgs[2]), 
                        int.Parse(inputArgs[3]), int.Parse(inputArgs[4]));
                        int mapHeight = int.Parse(inputArgs[4]);
                        Console.WriteLine("Here is a map of obstacles in the selected region:");
                        for (int i = mapHeight - 1; i >=0; i--)
                        {
                            foreach(List<string> mapY in visualMap)
                            {
                                Console.Write(mapY[i]);
                            }
                            Console.Write("\n");
                        }
                        
                    }
                    break;
                case "path":
                    // TODO Implement Path method in Map
                    Console.WriteLine("Not Implemented");
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
                    Console.WriteLine($"Invalid option: {input[0]}");
                    Console.WriteLine("Type 'help' to see a list of commands.");
                    Console.WriteLine("Enter command: ");
                    break;
            }
        }

        // Private method that tests if the third and fourth inputs can successfully be parsed as an int.
        private bool ValidateNumArgs(string xArg, string yArg)
        {
            bool validArgs = int.TryParse(xArg, out int x);
            validArgs = int.TryParse(yArg, out int y) && validArgs;
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
