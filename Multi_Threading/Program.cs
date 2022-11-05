using System;
using System.Diagnostics;
using System.Threading;

namespace Multi_Threading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("process Id: " + Process.GetCurrentProcess().Id);
            //Console.WriteLine("Thread Id: " + Thread.CurrentThread.ManagedThreadId);
            //Console.WriteLine("Processor Id: " + Thread.GetCurrentProcessorId());
            
            Thread.CurrentThread.Name = "Main thread";
            Console.WriteLine(Thread.CurrentThread.Name);
            Console.WriteLine($"Backgroung thread: {Thread.CurrentThread.IsBackground}");

            var wallet1 = new Wallet("hamed", 80);
            
            Thread t1 = new Thread(wallet1.RandomTransactions);
            t1.Name = "t1";
            Console.WriteLine($"T1 Backgroung thread: {t1.IsBackground}");
            Console.WriteLine($"after declaration {t1.Name} state is: {t1.ThreadState}");
            t1.Start();
            Console.WriteLine($"after start {t1.Name} state is: {t1.ThreadState}");

            t1.Join(); // wait t1 to finish and execute other thread

            Console.WriteLine("---------------");
            Thread t2 = new Thread(new ThreadStart(wallet1.RandomTransactions));
            t2.Name = "t2";
            t2.Start();
            Console.WriteLine($"after start {t2.Name} state is: {t2.ThreadState}");

            
            //Console.WriteLine("---------------");
            //Thread t3 = new Thread(new ThreadStart(wallet1.RandomTransactions));
            //t3.Name = "t3";
            //t3.Start();
            //Console.WriteLine($"after start {t3.Name} state is: {t3.ThreadState}");

            //Console.WriteLine("---------------");
            //Thread t4 = new Thread(new ThreadStart(wallet1.RandomTransactions));
            //t4.Name = "t4";
            //t4.Start();
            //Console.WriteLine($"after start {t4.Name} state is: {t4.ThreadState}");


            Console.ReadKey();
        
        }

    }
    public class Wallet
    {
        public Wallet(string name, int bitCoins)
        {
            Name = name;
            BitCoins = bitCoins;
        }

        public string Name { get; private set; }
        public int BitCoins { get; private set; }

        public void Debit(int amount)
        {
            BitCoins -= amount;
            Thread.Sleep(1000);
            Console.WriteLine("Thread Id: " + Thread.CurrentThread.ManagedThreadId + "-" + Thread.CurrentThread.Name +
                    " , Processor Id: " + Thread.GetCurrentProcessorId() + "] +"
                    + amount);
        }
        public void Credit(int amount)
        {
            BitCoins += amount;
            Thread.Sleep(1000);
            Console.WriteLine("Thread Id: " + Thread.CurrentThread.ManagedThreadId + "-" + Thread.CurrentThread.Name+
                    " , Processor Id: " + Thread.GetCurrentProcessorId() +  "] +" 
                    + amount);

        }

        public void RandomTransactions()
        {
            int[] amounts = { 10, 20, -10, 30, 10, 20, -10, 30, 10, 20, -10, 15 };
            foreach (var amount in amounts)
            {
                var absVal = Math.Abs(amount);
                if (amount < 0)
                {
                    Debit(absVal);
                }
                else
                {
                    Credit(amount);
                }
                //Console.WriteLine("Thread Id: " + Thread.CurrentThread.ManagedThreadId +
                //    " | Processor Id: " + Thread.GetCurrentProcessorId() + "] " + amount);

            }
        }
        public override string ToString()
        {
            return $"[{Name} -> {BitCoins} Bitcoins]";
        }
    }
}
