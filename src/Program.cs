using System;
using System.Threading.Tasks;
using src;

namespace auto_challenge
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Clear();
            Console.WriteLine("Program Started, Please wait..");

            IChallenge autoChallenge = new AutoChallenge();
            var status = autoChallenge.Execute().Result;

            Console.WriteLine($"Message: {status.Message}");
            Console.WriteLine($"Success: {status.Success}");
            Console.WriteLine($"MilliSeconds: {status.MilliSeconds}");
            Console.WriteLine($"Seconds: {status.MilliSeconds/1000}");

            Console.WriteLine("Press any key to exit..");
            Console.ReadLine();
        }
    }
}
