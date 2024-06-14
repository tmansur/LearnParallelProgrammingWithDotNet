using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallels.Programing.Examples._3.ConcurrentCollections
{
  public static class ConcurrentQueue
  {
    public static void Execute()
    {
      var queue = new ConcurrentQueue<int>();
      queue.Enqueue(1); // <- front
      queue.Enqueue(2);

      int result;
      if (queue.TryDequeue(out result))  //dequeue -> remove the element that was on the front of the queue
      {
        Console.WriteLine($"Removed element {result}");
      }
      
      if (queue.TryPeek(out result)) //peek -> to look at what's in the front of the queue
      {
        Console.WriteLine($"Front element is {result}");
      }

    }
  }
}
