using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel.Programing.Examples._1.TaskProgramming
{
    public static class CancellingTasks
    {
        public static void Execute()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                int i = 0;

                while (true)
                {
                    if (token.IsCancellationRequested)
                        break;
                    else
                        Console.WriteLine($"{i++}\t");
                }
            }, token);

            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }
    }
}
