
namespace Parallel.Programing.Examples._2.DataSharingAndSynchronization
{
    internal class InterlockedOperations
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
                Interlocked.Add(ref _balance, amount);
            }

            public void Withdraw(int amount)
            {
                Interlocked.Add(ref _balance, -amount);
            }
        }

        internal static void Execute()
        {
            var tasks = new List<Task>();
            var account = new BankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        account.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        account.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {account.Balance}.");
        }
    }
}