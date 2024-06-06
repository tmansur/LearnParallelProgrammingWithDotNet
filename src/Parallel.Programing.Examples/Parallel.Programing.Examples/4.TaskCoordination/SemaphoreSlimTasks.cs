
namespace Parallel.Programing.Examples._4.TaskCoordination
{
  /// <summary>
  /// With semaphore you can increase and decrease a counter
  /// </summary>
  internal class SemaphoreSlimTasks
  {
    internal static void Execute()
    {
      Example01();
    }

    private static void Example01()
    {
      var semaphore = new SemaphoreSlim(2, 10); //Define o número mínimo e máximo de requisições concorrentes que podem ocorrer. semaphore.CurrentCount = 2

      for (int i = 0; i < 20; i++)
      {
        Task.Factory.StartNew(() =>
        {
          Console.WriteLine($"{i}) Entering task {Task.CurrentId}.");
          semaphore.Wait(); // Block execution and decrease CurrentCount counter  
          Console.WriteLine($"{i}) Processing task {Task.CurrentId}."); // Código que será executado após liberação do semaphore
        });
      }

      while(semaphore.CurrentCount <= 2)
      {
        Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}.");
        Console.ReadKey();
        semaphore.Release(2); // ReleaseCount += 2 -> permite a execução de mais 2 tasks        
      }
    }

  }
}