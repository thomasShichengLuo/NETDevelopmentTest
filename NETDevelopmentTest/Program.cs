using Booster.CodingTest.Library;

namespace NETDevelopmentTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processing Stream...");
            var wordStream = new WordStream();
            var textProcesser = new TextProcesser();
            Task.Run(() => textProcesser.ProcessStream(wordStream));

            while(true)
            {
                Console.WriteLine("Display result? (press y + Enter)  -- press(q + Enter) to quit.");
                var pressedCommand = Console.ReadLine()?.Trim().ToLower();
                if (pressedCommand == "y")
                {
                    textProcesser.DisplayResults();
                }
                else if (pressedCommand == "q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown command.");
                }

            }
        }

    }
}
