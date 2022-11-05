using System;
using System.Threading;

namespace CADeadlock
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Wallet hazem = new Wallet("hazem", 100);
            Wallet Ahmed = new Wallet("Ahmed", 100);

            Console.WriteLine("Before transaction");
            Console.WriteLine("------------------");
            Console.WriteLine(hazem);
            Console.WriteLine(Ahmed);
            Console.WriteLine();
            
            TransferManager transfer1 = new TransferManager(hazem, Ahmed, 30);
            TransferManager transfer2 = new TransferManager(hazem, Ahmed, 20);

            Thread t1 = new Thread(transfer1.Transfer);
            Thread t2 = new Thread(transfer2.Transfer);
            t1.Name = "t1";
            t2.Name = "t2";

            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();


            Console.WriteLine("After transaction");
            Console.WriteLine("------------------");
            Console.WriteLine(hazem);
            Console.WriteLine(Ahmed);
            Console.WriteLine();


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
        private readonly object bitcoinsLock = new object();
        public void Debit(int amount)
        {
            lock (bitcoinsLock) //use lock keyword to ignore race condition
            {
                if (BitCoins > amount)
                {
                    Thread.Sleep(1000);
                    BitCoins -= amount;
                }
            }
        }
        public void Credit(int amount)
        {
            Thread.Sleep(1000);
            BitCoins += amount;

        }

        public override string ToString()
        {
            return $"[{Name} -> {BitCoins} Bitcoins]";
        }
    }
    class TransferManager //manager class of Wallet class -> manage wallets
    {
        private Wallet From;
        private Wallet To;
        private int TransferAmount;
        public TransferManager(Wallet from, Wallet to, int transferAmount)
        {
            From = from;
            To = to;
            TransferAmount = transferAmount;
        }
        public void Transfer()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} trying to lock ... {From}");
            lock (From)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} lock acquired ... {From}");
                Thread.Sleep(1000);
                Console.WriteLine($"{Thread.CurrentThread.Name} lock acquired ... {To}");
                //lock (To)
                //{
                //    From.Debit(TransferAmount);
                //    To.Credit(TransferAmount);
                //}


            }

        }

    }
}
