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
                function.Run();

                Thread.Sleep(300000);
            }
        }
    }
}
