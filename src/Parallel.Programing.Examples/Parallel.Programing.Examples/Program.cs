using Parallel.Programing.Examples._1.TaskProgramming;
using Parallel.Programing.Examples._2.DataSharingAndSynchronization;
using Parallel.Programing.Examples._3.ConcurrentCollections;
using Parallel.Programing.Examples._4.TaskCoordination;

namespace Parallel.Programing.Examples
{
  internal class Program
  {
    static void Main(string[] args)
    {
      TaskProgrammingProgram.TaskProgrammingMain();
      DataSharingAndSyncProgram.DataSharingAndSyncMain();
      ConcurrentCollectionsProgram.ConcurrentCollectionsProgramMain();
      TaskCoordinationProgram.TaskCoordinationMain();
    }
  }
}
