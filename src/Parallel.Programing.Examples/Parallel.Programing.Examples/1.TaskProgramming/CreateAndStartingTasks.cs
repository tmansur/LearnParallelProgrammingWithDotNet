using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallels.Programing.Examples._1.TaskProgramming
{
    public static class CreateAndStartingTasks
    {
        public static void Execute()
        {
            //Testing Write(char c)
            //Task.Factory.StartNew(() => Write('.')); // Cria e inicializa a execução da task
            //var t = new Task(() => Write('?')); // Cria a task
            //t.Start(); // Inicia a execução da task
            //Write('-');

            //Testing Write(object o)
            //var t = new Task(Write, "hello");
            //t.Start();
            //Task.Factory.StartNew(Write, 123);

            //Testing TextLength(object o)
            string text1 = "testing", text2 = "this";
            var t1 = new Task<int>(TextLength, text1);
            t1.Start();
            var t2 = Task.Factory.StartNew<int>(TextLength, text2);

            Console.WriteLine($"Length of '{text1}' is {t1.Result}.");
            Console.WriteLine($"Length of '{text2}' is {t2.Result}.");

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }

        public static void Write(object o)
        {
            int i = 1000;

            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static void Write(char c)
        {
            int i = 1000;

            while (i-- > 0)
            {
                Console.Write(c);
            }
        }
    }
}
