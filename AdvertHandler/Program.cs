using System;
using System.Threading;

namespace AdvertHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HandlerFunction function = new HandlerFunction();
            function.Config();

            while (true)
            {
                try
                {
                    Console.WriteLine($"Waking up Now to go to Work! @ {DateTime.Now}");
                    function.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception Thrown: {ex}");
                    Console.WriteLine("Now Get back to work!");
                }

                Console.WriteLine($"Going to Sleep for 1 mintue {DateTime.Now}");
                //Thread.Sleep(60000); // 60 seconds
                Thread.Sleep(10000); // 10 seconds
            }
        }
    }
}
