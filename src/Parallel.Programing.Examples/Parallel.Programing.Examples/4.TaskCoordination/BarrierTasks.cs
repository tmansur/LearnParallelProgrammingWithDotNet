using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Parallels.Programing.Examples._4.TaskCoordination
{
  public static class BarrierTasks
  {
    private static Barrier _barrier = new(2, b =>
    {
      Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
    });

    public static void Execute()
    {
      Console.WriteLine("Exemplo 01: ");
      Example01();

    }

    private static void Example01()
    {
      var water = Task.Factory.StartNew(Water);
      var cup = Task.Factory.StartNew(Cup);
      var tea = Task.Factory.ContinueWhenAll(
        new[] { water, cup },
        tasks =>
        {
          Console.WriteLine("Enjoy your tea");
        }
      );

      tea.Wait();
    }

    private static void Water()
    {
      Console.WriteLine("Putting the kettle on (long action)");
      Thread.Sleep(2000);
      _barrier.SignalAndWait(); //2º)participantCount = 2 -> Finaliza fase 0
      Console.WriteLine("Pouring water into cup"); //3º)participantCount = 0
      _barrier.SignalAndWait(); //4º)participantCount = 1 
      Console.WriteLine("Putting the kettle away");
    }

    private static void Cup()
    { 
      Console.WriteLine("Finding the nicest cup of tea (fast action)");
      _barrier.SignalAndWait(); //1º)participantCount = 1 
      Console.WriteLine("Adding tea to the cup");
      _barrier.SignalAndWait(); //5º)participantCount = 2 -> Finaliza fase 1 
      Console.WriteLine("Adding sugar");
      _barrier.SignalAndWait();
    }
  }
}
