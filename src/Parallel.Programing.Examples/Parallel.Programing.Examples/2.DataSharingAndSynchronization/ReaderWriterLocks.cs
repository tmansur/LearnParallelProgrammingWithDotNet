using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel.Programing.Examples._2.DataSharingAndSynchronization
{
    internal class ReaderWriterLocks
    {
        static ReaderWriterLockSlim _lock = new();
        static Random _random = new();

        internal static void Execute()
        {
            int x = 0;
            var tasks = new List<Task>();

            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    _lock.EnterReadLock();

                    Console.WriteLine($"Entered read lock, x = {x}");
                    Thread.Sleep(5000);

                    _lock.ExitReadLock();

                    Console.WriteLine($"Exited read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch(AggregateException ex)
            {
                ex.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while(true)
            {
                Console.ReadKey();
                _lock.EnterWriteLock();

                Console.Write("Write lock acquired");

                int newValue = _random.Next(10);
                x = newValue;

                Console.WriteLine($"Set x = {x}");

                _lock.ExitWriteLock();

                Console.WriteLine("Write lock released");
            }
        }
    }
}
