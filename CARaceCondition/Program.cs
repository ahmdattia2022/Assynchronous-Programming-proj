
using System;
using System.Threading;

namespace CARaceCondition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var wallet = new Wallet("ahmed", 50);
            Thread t1 = new Thread(() => wallet.Debit(40)); //for giving parameter to thread function
            Thread t2 = new Thread(() => wallet.Debit(30));
            {
                t1.Start();
                t2.Start();
                t1.Join();
                t2.Join();
                Console.WriteLine(wallet);
            }

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
}
