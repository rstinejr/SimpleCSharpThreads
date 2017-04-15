using System;
using System.Reflection;
using System.Threading;

namespace Waltonstine.Example.CSharp.Threads
{
    class Worker
    {
        private Mutex mutx;

        public Worker(Mutex m)
        {
            mutx = m;
        }

        public bool Quit { get; set; }

        public void DoSomeWork()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started.");

            mutx.WaitOne();

            try
            {
                if (Quit)
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} done. Throw an exception.");
                    throw new Exception("boo");
                }

                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has mutex. Do work, release mutex, exit.");

                // work work work
            }
            catch (Exception)
            {
                Console.WriteLine($"Exception caught in thread {Thread.CurrentThread.ManagedThreadId}");
            }
            finally
            {
                mutx.ReleaseMutex();
            }

            return;
        }
    }

    public class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Main thread {Thread.CurrentThread.ManagedThreadId}, create a worker thread.");
            Mutex mutx = new Mutex();

            Worker worker = new Worker(mutx);
            Thread t = new Thread(new ThreadStart(worker.DoSomeWork));

            Console.WriteLine("Start thread");
            mutx.WaitOne();
            t.Start();

            Thread.Sleep(10);  // Thread.Yield() is not part of .NET Core.

            Console.Write("Press 'x' to throw exception in thread, any other key to continue: ");
            ConsoleKeyInfo ki = Console.ReadKey();
            Console.WriteLine();
            if (ki.KeyChar == 'x')
            {
                worker.Quit = true;
            }
            mutx.ReleaseMutex();
            
            t.Join();

            Console.WriteLine("Thread joined. Done.");
        }
    }
}
