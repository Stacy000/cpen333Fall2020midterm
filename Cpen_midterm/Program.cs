using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace MTQ1
{
    public class Program
    {
        private static Mutex mut = new Mutex();
        private static object Tolock = new object();


        static void Main(string[] args)
        {
            long counter = 0;
            int numberOfThreads = 5;


            List<Thread> threads = new List<Thread>();
            Task[] tasks = new Task[5];

            /*  for (int i = 0; i < numberOfThreads; i++)
              {
                  Thread t = new Thread(() => thread_increment(ref counter));

                  t.Start();
                  threads.Add(t);

              }
            */
            for (int i = 0; i < numberOfThreads; i++)
            {
                tasks[i] = Task.Run(() => thread_increment(ref counter));
            }

            Task.WaitAll(tasks);

            /* foreach (Thread thread in threads)
             {
                 thread.Join();
             }
            */
            Console.WriteLine("Counter:{0}", counter);

        }
        //Mutex Method
        /* static void thread_increment(ref long counter)

         {


             for (int i = 0; i < 100000; i++)
             {
                 mut.WaitOne();
                 counter++;
                 mut.ReleaseMutex();
             }
             // }


         }
        */
        static void thread_increment(ref long counter)

        {

            lock (Tolock)
            {

                for (int i = 0; i < 100000; i++)
                {

                    counter++;

                }
            }





        }
    }
}

