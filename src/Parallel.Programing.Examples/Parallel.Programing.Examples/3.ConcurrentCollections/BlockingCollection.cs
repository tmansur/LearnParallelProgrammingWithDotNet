using System.Collections.Concurrent;

namespace Parallel.Programing.Examples._3.ConcurrentCollections
{
  /// <summary>
  /// It can actually block on the elements that you are consuming and not let you add anu more elements.
  /// </summary>
  public static class BlockingCollection
  {
    private static Random _random = new();
    private static CancellationTokenSource _cancellationTokenSource = new();
    private static BlockingCollection<int> _messages = new(new ConcurrentBag<int>(), 10);    

    public static void Execute()
    {
      Task.Factory.StartNew(ProduceAndConsume, _cancellationTokenSource.Token);

      Console.ReadKey();
      _cancellationTokenSource.Cancel();
    }

    private static void ProduceAndConsume()
    {
      var producer = Task.Factory.StartNew(RunProducer);
      var consumer = Task.Factory.StartNew(RunConsumer);

      try
      {
        Task.WaitAll(new[] { producer, consumer }, _cancellationTokenSource.Token);
      }
      catch (AggregateException ex)
      {
        ex.Handle(e => true);
      }
    }

    private static void RunConsumer()
    {
      foreach(var item in _messages.GetConsumingEnumerable())
      {
        _cancellationTokenSource.Token.ThrowIfCancellationRequested();

        Console.WriteLine($"-{item}\t");

        Thread.Sleep(_random.Next(1000));
      }
    }

    private static void RunProducer()
    {
      while(true)
      {
        _cancellationTokenSource.Token.ThrowIfCancellationRequested();

        int i = _random.Next(100);
        _messages.Add(i);

        Console.WriteLine($"+{i}\t");

        Thread.Sleep(_random.Next(10));
      }
    }
  }
}
