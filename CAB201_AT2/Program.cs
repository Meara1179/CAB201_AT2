namespace CAB201_AT2
{
    internal class Program
    {
        /// <summary>
        /// Entry method called on startup.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            InputProcessor processor = new InputProcessor();
            Console.WriteLine("Welcome to the Threat-Master v1.62.");
            processor.PostHelpText();
            processor.StartReadInput();
        }
    }
}