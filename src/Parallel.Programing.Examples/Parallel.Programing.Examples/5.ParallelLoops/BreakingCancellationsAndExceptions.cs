

using System.Diagnostics;

namespace Parallels.Programing.Examples._5.ParallelsLoops
{
  internal class BreakingCancellationsAndExceptions
  {
    private static ParallelLoopResult _result;

    internal static void Execute()
    {
      Example01();

      try
      {
        Example02();
      }
      catch (OperationCanceledException oce)
      {
        Console.WriteLine("Error: " + oce.Message);
      }
    }
  
    private static void Example01()
    {
      _result = Parallel.For(0, 20, (x, state) =>
      {
        Console.WriteLine($"{x} [{Task.CurrentId}]");

        if(x == 10)
        {
          //state.Stop(); //Para o loop assim que possível
          state.Break(); //Menos imediato que Stop
        }
      });

      Console.WriteLine();
      Console.WriteLine($"Was loop completed? {_result.IsCompleted}");

      if(_result.LowestBreakIteration.HasValue)
      {
        Console.WriteLine($"Lowest break iteration is {_result.LowestBreakIteration}");
      }

    }

    private static void Example02()
    {
      var cts = new CancellationTokenSource();
      var po = new ParallelOptions();
      po.CancellationToken = cts.Token;

      _result = Parallel.For(0, 20, po, (x, state) =>
      {
        Console.WriteLine($"{x} [{Task.CurrentId}]");

        if (x == 10)
        {
          cts.Cancel();
        }
      });
    }


  }
}