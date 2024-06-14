

namespace Parallels.Programing.Examples._5.ParallelsLoops
{
  internal class ParallelInvokeForAndForeach
  {
    internal static void Execute()
    {
      Example01();
      Example02();
      Example03();
      Example04();
    }    

    private static void Example01()
    {
      var action1 = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
      var action2 = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
      var action3 = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));

      //Executando as actions de forma concorrente com Invoke
      Parallel.Invoke(action1, action2, action3); 
    }

    private static void Example02()
    {
      Parallel.For(1, 11, i => Console.WriteLine($"{i}"));
    }

    private static void Example03()
    {
      string[] words = { "oh", "what", "a", "night", "sea", "lyon" };

      Parallel.ForEach(words, word => Console.WriteLine($"{word} has length {word.Length} (task {Task.CurrentId})."));
    }

    private static void Example04()
    {
      Parallel.ForEach(Range(1, 20, 2), Console.WriteLine);
    }

    private static IEnumerable<int> Range(int start, int end, int increment)
    {
      for (int i = start; i < end; i += increment)
      {
         yield return i;
      }
    }
  }
}