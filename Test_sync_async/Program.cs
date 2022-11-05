using System;
using System.IO;//*
using System.Threading;
using System.Threading.Tasks;

namespace Test_sync_async
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //await ReadFile();
            //Console.WriteLine("---------------");
            //Console.WriteLine(await GetDataFromNetwork());

            //Thread thread = new Thread(WriteUsingThread);
            //thread.Start();
            //Thread.CurrentThread.Name = "Custom Main thread";
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.Write($"WT: {i} ");
            //}
                
            Test.testThreadPool();

            Console.ReadLine();
        }
        static void WriteUsingThread()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write($"MT: {i} ");
            }
            Console.WriteLine();
        }
        static async Task ReadFile()
        {
            var data = await File.ReadAllLinesAsync("myfile.txt");
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }

        }
        Func<Task<int>> GetDataFromNetworkViaLambda = async () =>
        {
            await Task.Delay(150);
            var res = 42;
            return res;
        };
        static async Task<int> GetDataFromNetwork()
        {
            //simulation of a network code
            await Task.Delay(150);
            var res = 42;

            return res;
        }
        public static void Test_ReadAllLinesAsync()
        {
            //Synchronous approach

            //var data = File.ReadAllLines("myfile.txt");
            //foreach (var item in data)
            //{
            //    Console.WriteLine(item);
            //    Thread.Sleep(200);
            //}
            //Console.WriteLine("task finished");
            //-----------------------
            // var fileReadTask = File.ReadAllLinesAsync("myfile.txt");
            //fileReadTask.Wait();//bolck the thread
            //var lines = fileReadTask.Result; //wait but blocks the cpu

            File.ReadAllLinesAsync("myfile.txt") //old fassion
               .ContinueWith(t =>
               {

                   if (t.IsFaulted)
                   {
                       Console.Error.WriteLine(t.Exception);
                       return;
                   }


                    //task will be completed | we can here safely access the task
                   var lines = t.Result;
                   foreach (var item in lines)
                   {
                       Console.WriteLine(item);
                   }

               });

            Console.ReadKey();
        }

        //static async Task Main(string[] args)
        //{
        //    Task<int> result = LongProcess();

        //    ShortProcess();

        //    var val = await result; // wait untile get the return value

        //    Console.WriteLine("Result: {0}", val);

        //    Console.ReadKey();
        //}

        //static async Task<int> LongProcess()
        //{
        //    Console.WriteLine("LongProcess Started");

        //    await Task.Delay(4000); // hold execution for 4 seconds

        //    Console.WriteLine("LongProcess Completed");

        //    return 10;
        //}

        //static void ShortProcess()
        //{
        //    Console.WriteLine("ShortProcess Started");

        //    //do something here

        //    Console.WriteLine("ShortProcess Completed");
        //}


    }
}
