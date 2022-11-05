using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_sync_async
{
    public class Test
    {
        public static void testThreadPool()
        {
            Console.WriteLine($"Is main thread a thread pool thread {Thread.CurrentThread.IsThreadPoolThread}");
            var employee = new Employee(name: "orestis", companyName: "Microsoft");

            ThreadPool.QueueUserWorkItem(new WaitCallback(DisplayEmployeeInfo), employee);

            int workerThread = 0;
            int completionPortThreads = 0;
            ThreadPool.GetMinThreads(out workerThread, out completionPortThreads);
            ThreadPool.SetMaxThreads(workerThread * 2, completionPortThreads * 2);
            
            Console.WriteLine($"Is main thread a thread pool thread? {Thread.CurrentThread.IsThreadPoolThread}");

            Console.ReadLine();
        }
        static void DisplayEmployeeInfo(object employee)
        {
            Console.WriteLine($"Is thread running this piece of code a thread pool? {Thread.CurrentThread.IsThreadPoolThread}");
            Employee emp = employee as Employee;
            Console.WriteLine($"Person name is: {emp.Name} and company name is: {emp.CompanyName}");
        }
    }
    public class Employee
    {
        public Employee(string name, string companyName)
        {
            Name = name;
            CompanyName = companyName;
        }

        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
}
