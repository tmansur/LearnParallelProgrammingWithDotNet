
namespace Parallels.Programing.Examples._4.TaskCoordination
{
  internal class CountdownEventTasks
  {
    private static readonly int _taskCount = 5;
    private static readonly CountdownEvent _countdownEvent = new(_taskCount);

    internal static void Execute()
    {
      Console.WriteLine("Exemplo 1:");
      Example01();
    }

    private static void Example01()
    {
      for (int i = 0; i < _taskCount; i++)
      {
        Task.Factory.StartNew(() =>
        {
          Console.WriteLine($"Entering task {Task.CurrentId}.");
          Thread.Sleep(2000);
          _countdownEvent.Signal();

          Console.WriteLine($"Exiting task {Task.CurrentId}.");
        });
      }

      var finalTask = Task.Factory.StartNew(() =>
      {
        Console.WriteLine($"Waiting for others tasks to complete in {Task.CurrentId}.");
        _countdownEvent.Wait(); //Bloqueia a executação da finalTask até o contador chegar a zero
        Console.WriteLine("All tasks completed");
      });

      finalTask.Wait();
    }
  }
}