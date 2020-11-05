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
        private static int count = 0;
        private static SemaphoreSlim sem = new SemaphoreSlim(1);
        private static SemaphoreSlim sem_2 = new SemaphoreSlim(3);


     /*   static void Main(string[] args)
        {
            long counter = 0;
            int numberOfThreads = 5;


            List<Thread> threads = new List<Thread>();
            List<Task> tasks1 = new List<Task>();



            for (int i = 0; i < numberOfThreads; i++)
            {
                Thread t = new Thread(() => thread_increment(ref counter));

                t.Start();
                threads.Add(t);

            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }



            /*  for (int i = 0; i < numberOfThreads; i++)
              {
                  Task k = new Task(() => thread_increment(ref counter));

                  k.Start();
                  tasks1.Add(k);

              }

              foreach (Task task in tasks1)
              {
                  task.Wait();
              }
              */

            //run tasks in one line
            /* Task[] tasks2 = new Task[5];

             for (int i = 0; i < numberOfThreads; i++)
             {
                 tasks2[i] = Task.Run(() => thread_increment(ref counter));
             }

             Task.WaitAll(tasks2);
             */
          //  Console.WriteLine("Counter:{0}", counter);



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
        
        //lock method
        /*static void thread_increment(ref long counter)

        {

            lock (Tolock)
            {

                for (int i = 0; i < 100000; i++)
                {

                    counter++;

                }
            }

        }
        */
        //monitor method
        /*
        static void thread_increment(ref long counter)

        {


            
            for (int i = 0; i < 100000; i++)
            {
                Monitor.Enter(Tolock);
                counter++;
                Monitor.Exit(Tolock);

            }
            

        }
        */
        //Interlock Exchange method

        /*static void thread_increment(ref long counter)

        {



            for (int i = 0; i < 100000; i++)
            {
                if (0 == Interlocked.Exchange(ref count, 1))
                {
                    counter++;
                    Interlocked.Exchange(ref count, 0);

                }

                else
                {
                    i--;
                }
            }
        */

        //SemaphoreSlim alone method
        /*
        static void thread_increment(ref long counter)

        {
            
            for (int i = 0; i < 100000; i++)
            {
                sem.Wait();
                counter++;
                
                sem.Release();

            }
            
        }
        */
        //Combination of SemaphoreSlim with initial capacity of 3 and Interlocked.Increment() method
   /*     static void thread_increment(ref long counter)

        {
            
            for (int i = 0; i < 100000; i++)
            {
                sem.Wait();
                Interlocked.Increment(ref counter);
                sem.Release();

            }
            
        }
   */
    }



