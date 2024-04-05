
namespace Parallel.Programing.Examples._2.DataSharingAndSynchronization
{
    internal class MutexSynchronization
    {
        public class BankAccount
        {
            private int _balance;

            public int Balance
            {
                get { return _balance; }
                private set { _balance = value; }
            }

            public void Deposit(int amount)
            {
                // Balance += amount
                // op1: temp <- get_Balance() + amount
                // op2: set_Balance(temp)
                Balance += amount;
            }

            public void Withdraw(int amount)
            {
                Balance -= amount;
            }
        }

        internal static void Execute()
        {
            var tasks = new List<Task>();
            var account = new BankAccount();
            var mutex = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var haveLock = mutex.WaitOne();

                        try
                        {
                            account.Deposit(100);
                        }
                        finally
                        {
                            if(haveLock)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var haveLock = mutex.WaitOne();

                        try
                        {
                            account.Withdraw(100);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {account.Balance}.");
        }
    }
}