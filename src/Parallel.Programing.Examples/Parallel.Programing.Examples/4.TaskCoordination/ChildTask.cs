using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel.Programing.Examples._4.TaskCoordination
{
  public static class ChildTask
  {
    public static void Execute()
    {
      Console.WriteLine("Exemplo 01: ");
      Example01();

      Console.WriteLine();
      Console.WriteLine("Exemplo 02: ");
      Example02();
    }

    private static void Example01()
    {
      var parent = new Task(() =>
      {
        var child = new Task(() =>
        {
          Console.WriteLine("Child task starting");
          Thread.Sleep(3000);
          Console.WriteLine("Child task finishing");
        }, TaskCreationOptions.AttachedToParent); //Vincula a task filha a pai

        child.Start();
      });

      parent.Start();

      try
      {
        parent.Wait(); //Espera pela task pai e pela filha
      }
      catch (AggregateException ex) 
      {
        ex.Handle(e => true);
      }
    }

    private static void Example02()
    {
      var parent = new Task(() =>
      {
        var child = new Task(() =>
        {
          Console.WriteLine("Child task starting");
          Thread.Sleep(3000);
          Console.WriteLine("Child task finishing");
          //throw new Exception();
        }, TaskCreationOptions.AttachedToParent); //Vincula a task filha a pai

        var completionHandler = child.ContinueWith(t => //Executa se a execução da task filha finalizar com sucesso
        {
          Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
        }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

        var failHandler = child.ContinueWith(t => //Executa se ocorrer erro na execução da task filha
        {
          Console.WriteLine($"Opss, task {t.Id}'s state is {t.Status}");
        }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

        child.Start();
      });

      parent.Start();

      try
      {
        parent.Wait(); //Espera pela task pai e pela filha
      }
      catch (AggregateException ex)
      {
        ex.Handle(e => true);
      }
    }
  }
}
