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

            token.Register(() =>
            {
                Console.WriteLine("Cancelation has been requested.");
            });

            var t = new Task(() =>
            {
                int i = 0;

                while (true)
                {
                    if (token.IsCancellationRequested)
                        //break; //soft exit
                        throw new OperationCanceledException();
                    else
                        Console.WriteLine($"{i++}\t");
                    
                    // Mesmo comportamento do if anterior mas escrito de outra maneira
                    //token.ThrowIfCancellationRequested();
                    //Console.WriteLine($"{i++}\t");
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
