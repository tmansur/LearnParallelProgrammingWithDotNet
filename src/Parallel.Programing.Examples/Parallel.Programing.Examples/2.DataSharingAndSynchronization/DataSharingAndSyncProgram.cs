using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel.Programing.Examples._2.DataSharingAndSynchronization
{
    /*
     * - Critical Sections
     *      Parte do código que deve ser executado por apenas uma thread de cada vez.
     *      Se uma thread está executando aquele bloco de código a outra deve aguardar.
     */
    public static class DataSharingAndSyncProgram
    {
        public static void DataSharingAndSyncMain()
        {
            //CriticalSections.Execute();
            InterlockedOperations.Execute();
        }
    }
}
