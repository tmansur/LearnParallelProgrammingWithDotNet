
namespace Parallels.Programing.Examples._1.TaskProgramming
{
    public static class WaitingForTimeToPass
    {
        internal static void Execute()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm, you have 5 seconds");

                bool cancelled = token.WaitHandle.WaitOne(5000);

                Console.WriteLine(cancelled ? "Bomb disarmed." : "BOOM!!!");
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.ReadKey();
            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}