
namespace Parallels.Programing.Examples._1.TaskProgramming
{
    internal class ExceptionHandling
    {
        internal static void Execute()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this!") { Source = "t1" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ex)
            {
                foreach(var e in ex.InnerExceptions)
                {
                    Console.WriteLine($"Exception {e.GetType()} from {e.Source}");
                }
            }

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }
    }
}