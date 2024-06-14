using System.Collections.Concurrent;

namespace Parallels.Programing.Examples._3.ConcurrentCollections
{
  /// <summary>
  /// Bag -> no ordering
  /// </summary>
  public static class ConcurrentBag
  {
    public static void Execute()
    {
      var bag = new ConcurrentBag<int>();
      var tasks = new List<Task>();

      for (int i = 0; i < 10; i++)
      {
        var iLocal = i;
        tasks.Add(Task.Factory.StartNew(() =>
        {
          bag.Add(iLocal);
          Console.WriteLine($"{Task.CurrentId} has added {iLocal}");

          int result;
          if(bag.TryPeek(out result))
          {
            Console.WriteLine($"{Task.CurrentId} has peeked the value {result}");
          }
        }));
      }

      Task.WaitAll(tasks.ToArray());

      int last;
      if(bag.TryTake(out last))
      {
        Console.WriteLine($"I got {last}");
      }
    }
  }
}
