
namespace Parallels.Programing.Examples._2.DataSharingAndSynchronization
{
    /*
     * Spin lock ajuda a controlar o acesso a variáveis com operações críticas (não atômicas)
     * Não é possível chamar recursivamente uma classe que utiliza o spin lock, ou seja, ele não suporta lock recursion
     */
    internal class SpinLockingAndLockRecursion
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
            var spinLock = new SpinLock();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var lockTaken = false;

                        try
                        {
                            spinLock.Enter(ref lockTaken);
                            account.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                spinLock.Exit();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var lockTaken = false;

                        try
                        {
                            spinLock.Enter(ref lockTaken);
                            account.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                spinLock.Exit();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {account.Balance}.");
        }
    }
}