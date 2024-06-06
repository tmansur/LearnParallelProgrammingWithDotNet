
namespace Parallel.Programing.Examples._4.TaskCoordination
{
  internal class ManualResetEventAndAutoResetEvent
  {
    internal static void Execute()
    {
      Console.WriteLine("Exemplo 1:");
      Example01();

      Console.WriteLine("");
      Console.WriteLine("Exemplo 2:");
      Example02();
    }

    private static void Example01()
    {
      var eventSlin = new ManualResetEventSlim(); // work with 0 or 1

      Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Boiling water");
        eventSlin.Set(); //Envia sinal informando que a task foi processada
      });

      var makeTea = Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Waiting for water...");
        eventSlin.Wait(); //Aguarda sinal do Event Slim para continuar o processamento
        Console.WriteLine("Here is your tea.");

        var ok = eventSlin.Wait(1000);
        if (ok)
        {
          Console.WriteLine("Enjoy your tea.");
        }
        else
        {
          Console.WriteLine("There is a fly in your tea, sorry.");
        }
      });

      makeTea.Wait(); 
    }

    private static void Example02()
    {
      var evt = new AutoResetEvent(false); //false

      Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Boiling water");
        evt.Set(); //true
      });

      var makeTea = Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Waiting for water...");
        evt.WaitOne(); //Aguarda valor true para continuar a execução e seta o sinal para false
        Console.WriteLine("Here is your tea.");
        
        var ok = evt.WaitOne(1000);
        if(ok)
        {
          Console.WriteLine("Enjoy your tea.");
        }
        else
        {
          Console.WriteLine("There is a fly in your tea, sorry.");
        }
      });

      makeTea.Wait();
    }
  }
}