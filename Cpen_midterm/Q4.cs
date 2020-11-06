using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace MTQ4
{
    class Program
    {
        public static int rollerCoasterCapacity = 3;
        public static int passengersInLine = 8;
        public static int passengerOnBoard = 0;
        public static int remainingSeat = 0;
        public static SemaphoreSlim Entry = new SemaphoreSlim(0, rollerCoasterCapacity);
        public static SemaphoreSlim Full = new SemaphoreSlim(0, rollerCoasterCapacity);
        public static SemaphoreSlim Exit = new SemaphoreSlim(0, rollerCoasterCapacity);
        public static SemaphoreSlim Empty = new SemaphoreSlim(0, rollerCoasterCapacity);

        public static SemaphoreSlim s1 = new SemaphoreSlim(1, passengersInLine);
        public static SemaphoreSlim s2 = new SemaphoreSlim(1, passengersInLine);

        static void Main(string[] args)
        {
            // TO-DO ROLLER-COASTER METHOD IN A THREAD

            // TO-DO PASSENGER/THREAD

            Task.Run(() =>
            {
                RollerCoaster();
            });

            for (int i = 0; i < passengersInLine; i++)
            {
                int count = i + 1;
                Task.Run(() => {
                    Passenger(count);
                });

            }

            Console.ReadKey();
            //SYNCHRONIZE THREADS


        }


        private static void Passenger(int count)
        {
            //passengers get on the roller coaster
            Console.WriteLine("passenger{0} is ready to get on", count);
            Entry.Wait(); //passengers ready to get on the rollar coaster
            Console.WriteLine("passenger{0} has boarded", count);

            s1.Wait();
            passengerOnBoard++;

            if (passengerOnBoard == rollerCoasterCapacity)
            {
                Full.Release(); //all passengers are boarded
                passengerOnBoard = 0;
            }
            s1.Release();

            //passengers get off the roller coaster
            Exit.Wait(); //passengers ready to get off
            Console.WriteLine("passenger{0} is ready to get off", count);

            s2.Wait();
            remainingSeat++;
            if (remainingSeat <= rollerCoasterCapacity)
            {
                Empty.Release(); //all passenger got off
                remainingSeat = 0;
            }
            
            s2.Release();
            Console.WriteLine("passenger{0} got off ", count);
        }



        static void RollerCoaster()
        {
            //TO-DO Implement each condition //
            //condition 1 semaphore boarding

            Entry.Release(rollerCoasterCapacity);
            Console.WriteLine("All Entered");
            Thread.Sleep(1500);

            Full.Wait();
            Console.WriteLine("All Boarded");
            Thread.Sleep(1500);

            Exit.Release(rollerCoasterCapacity);
            Console.WriteLine("Ride begins");
            Thread.Sleep(1500);

            Empty.Wait();
            Console.WriteLine("All Exited");
            Thread.Sleep(1500);
            passengersInLine = passengersInLine - rollerCoasterCapacity; //one ride is finished, the next 4 passengers are ready to get on
            Console.WriteLine(passengersInLine);
            if (passengersInLine == 0)
            {
                Environment.Exit(0);
            }



            else if (passengersInLine < rollerCoasterCapacity)
            {
                Entry.Release(rollerCoasterCapacity);
                Console.WriteLine("All Entered");
                Thread.Sleep(1500);

                Full.Wait();
                Console.WriteLine("All Boarded");
                Thread.Sleep(1500);

                Exit.Release(rollerCoasterCapacity);
                Console.WriteLine("Ride begins");
                Thread.Sleep(1500);

                Empty.Wait();
                Console.WriteLine("All Exited");
                Thread.Sleep(1500);

            }

            else
            {
                Task.Run(() => {
                    RollerCoaster();
                });
            }
        }


    }

}


