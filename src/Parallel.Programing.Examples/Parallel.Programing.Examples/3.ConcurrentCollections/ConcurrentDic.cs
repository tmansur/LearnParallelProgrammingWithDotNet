using System.Collections.Concurrent;

namespace Parallel.Programing.Examples._3.ConcurrentCollections
{
  public class ConcurrentDic
  {
    private static ConcurrentDictionary<string, string> _capitals = new();

    public static void Execute()
    {
      AccessConcurrentlyAndControlDictionary();
      UpdateCapital();
      GetOrAddCapital();
      RemoveCapital();
    }

    public static void AddParis()
    {
      bool success = _capitals.TryAdd("France", "Paris");
      string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Maind thread";

      Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element");
    }

    /// <summary>
    /// Exemplo de como acessar de forma concorrente um dicionário e fazer o controle das operações
    /// </summary>
    private static void AccessConcurrentlyAndControlDictionary()
    {
      Task.Factory.StartNew(AddParis).Wait(); // Executado pela thread que será criada
      AddParis(); // Executado pela thread principal      
    }

    private static void UpdateCapital()
    {
      _capitals["Russia"] = "Leningrad"; //add uma nova capital ao dicionário
      //_capitals["Russia"] = "Moscow"; // faz update da capital, mas existe uma forma mais segura para fazer esse update
      _capitals.AddOrUpdate("Russia", "Moscow", (key, oldCapital) => oldCapital + " --> Moscow");
      Console.WriteLine($"The capital of Russia is {_capitals["Russia"]}");
    }

    private static void GetOrAddCapital()
    {
      _capitals["Brazil"] = "Brasilia";
      var capitalOfBrazil = _capitals.GetOrAdd("Brazil", "Rio de Janeiro");

      Console.WriteLine($"The capital of Brazil is {capitalOfBrazil}");
    }

    private static void RemoveCapital()
    {
      string capitalToRemove = "Russia";
      string removed;

      bool didRemove = _capitals.TryRemove(capitalToRemove, out removed);

      if (didRemove)
      {
        Console.WriteLine($"We just removed {removed}");
      }
      else
      {
        Console.WriteLine($"Failed to remove the capital of {capitalToRemove}");
      }
    }
  }
}
