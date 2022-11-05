using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_sync_async
{
    internal class Test_cancellationTasks
    {
        public static void Test()
        {
            Console.WriteLine("Starting application...");

            CancellationTokenSource source = new CancellationTokenSource();
            var task = new CancellableTaskTest().CancellableTask(source.Token);
            Console.WriteLine("Heavy process invoked");
            Console.WriteLine("press c to cancel");
            Console.WriteLine();
            char ch = Console.ReadKey().KeyChar;
            if (ch == 'c' || ch == 'C')
            {
                source.Cancel();
                Console.WriteLine("\nTask cancellation requested.");
            }
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                
            }
        }
    }
    public class CancellableTaskTest
    {
        public Task CancellableTask(CancellationToken ct)
        {
            return Task.Factory.StartNew(() => CancellableWork(ct), ct);
        }
        public void CancellableWork(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Cancelled work before sart");
                cancellationToken.ThrowIfCancellationRequested();
            }
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine($"cancelled on iteration # {i + 1}");
                    cancellationToken.ThrowIfCancellationRequested();
                }
                Console.WriteLine($"iteration # {i + 1} completed");
            }
        }
    }
}
