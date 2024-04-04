using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel.Programing.Examples._2.DataSharingAndSynchronization
{
    public class BankAccount
    {
        public object padlock = new object();
        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            // +=
            // op1: temp <- get_Balance() + amount
            // op2: set_Balance(temp)
            lock(padlock)
            {
                Balance += amount;
            }            
        }

        public void Withdraw(int amount)
        {
            lock (padlock)
            {
                Balance -= amount;
            }
        }
    }

    internal class CriticalSections
    {      
        public static void Execute()
        {
            var tasks = new List<Task>();
            var account = new BankAccount();

            for(int i = 0; i < 10; i++)
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
