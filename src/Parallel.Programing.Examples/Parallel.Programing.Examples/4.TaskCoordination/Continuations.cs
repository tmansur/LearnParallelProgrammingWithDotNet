using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel.Programing.Examples._4.TaskCoordination
{
  /// <summary>
  /// Execução de duas ou mais tasks de forma sequencial (a próxima task só executa quando a anterior
  /// terminar sua execução
  /// </summary>
  public static class Continuations
  {
    public static void Execute()
    {
      Console.WriteLine("Exemplo 01: ");
      Example01();

      Console.WriteLine("");
      Console.WriteLine("Exemplo 02: ");
      Example02();

      Console.WriteLine("");
      Console.WriteLine("Exemplo 03: ");
      Example03();
    }

    private static void Example01()
    {
      var task = Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Boiling water");
      });

      var task2 = task.ContinueWith(t =>
      {
        Console.WriteLine($"Completed task {t.Id}, pour water into cup.");
      });

      task2.Wait();
    }

    private static void Example02()
    {
      var task1 = Task.Factory.StartNew(() => "Task1");
      var task2 = Task.Factory.StartNew(() => "Task2");
      var task3 = Task.Factory.ContinueWhenAll(
        new[] {task1, task2},
        tasks =>
        {
          Console.WriteLine("Tasks completed: ");
          foreach(var task in tasks)
          {
            Console.WriteLine(" - " + task.Result);
          }

          Console.WriteLine("All tasks done");
        }
      );

      task3.Wait();
    }

    private static void Example03()
    {
      var task1 = Task.Factory.StartNew(() => "Task1");
      var task2 = Task.Factory.StartNew(() => "Task2");
      var task3 = Task.Factory.ContinueWhenAny(
        new[] { task1, task2 },
        task =>
        {
          Console.WriteLine($"Task {task.Result} completed: ");
        }
      );

      task3.Wait();
    }
  }
}
