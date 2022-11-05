using System;
using System.Diagnostics;
using System.Threading;

namespace CA_sequential
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //top down approach
            var wallet = new Wallet("ahmed", 50);
            wallet.RandomTransactions();
            Console.WriteLine("-----------");
            Console.WriteLine(wallet);

            var wallet2 = new Wallet("hussein", 17);
            wallet2.RandomTransactions();
            Console.WriteLine("-----------");
            Console.WriteLine(wallet2);



            Console.ReadKey();
        }
    }

    class Wallet
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
        }
        public void Credit(int amount)
        {
            BitCoins += amount;
        }

        public void RandomTransactions()
        {
            int[] amounts = { 10, 20, -10, 30, 10, 20, -10, 30, 10, 20, -10, 15 };
            foreach (var item in amounts)
            {
                var absVal = Math.Abs(item);
                if (item < 0)
                {
                    Debit(absVal);
                }
                else
                {
                    Credit(item);
                }
                Console.Write("Thread Id: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(" | Processor Id: " + Thread.GetCurrentProcessorId());
            }
        }
        public override string ToString()
        {
            return $"[{Name} -> {BitCoins} Bitcoins]";
        }
    }
}
