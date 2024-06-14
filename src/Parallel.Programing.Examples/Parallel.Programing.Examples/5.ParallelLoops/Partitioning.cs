
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;

namespace Parallels.Programing.Examples._5.ParallelsLoops
{
  public class Partitioning
  {
    public void Execute()
    {
      var summary = BenchmarkRunner.Run<Partitioning>();
      Console.WriteLine(summary);
    }

    [Benchmark]
    public void SquareEachValue()
    {
      const int count = 100000;
      var values = Enumerable.Range(0, count);
      var results = new int[count];

      Parallel.ForEach(values, value =>
      {
        results[value] = (int)Math.Pow(value, 2);
      });
    }

    [Benchmark]
    public void SquareEachValueChunked()
    {
      const int count = 100000;
      var values = Enumerable.Range(0, count);
      var results = new int[count];
      var part = Partitioner.Create(0, count, 100000);

      Parallel.ForEach(part, range =>
      {
        for (int i = range.Item1; i < range.Item2; i++)
        {
          results[i] = (int)Math.Pow(i, 2);
        }
      });
    }
  }
}