using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace MTQ4
{
    class Program
    {
        public static int rollerCoasterCapacity = 4;
        public static int passengersInLine = 4;
        public static int passengerOnBoard = 0;
        public static int remainingSeat = 0;

        //initialize semaphoreSlims with 0, max is number of rollarCoasterCapacity
        public static SemaphoreSlim Entry = new SemaphoreSlim(0, rollerCoasterCapacity); //Passengers perform a Wait()on Entry in order to board the rollercoaster. When the rollercoaster starts it can Release() Entry to allow passengers to board
        public static SemaphoreSlim Full = new SemaphoreSlim(0, rollerCoasterCapacity);//The rollercoaster will perform Wait() operations on this semaphore before departing to ensure it is full. Each passenger who boards the rollercoaster will Release() this semaphore
        public static SemaphoreSlim Exit = new SemaphoreSlim(0, rollerCoasterCapacity); //Passengers will perform a Wait() on Exit before getting off. When the rollercoaster returns after a ride, it will Release() this semaphore to let off the passengers
        public static SemaphoreSlim Empty = new SemaphoreSlim(0, rollerCoasterCapacity); //Passengers will perform a Release() on this semaphore when they have actually got off. The rollercoaster will perform a Wait() on this semaphore to ensure that the passengers have left before letting new passengers on

        //mutexes to protect counters
        public static SemaphoreSlim s1 = new SemaphoreSlim(1, passengersInLine); 
        public static SemaphoreSlim s2 = new SemaphoreSlim(1, passengersInLine);

        static void Main(string[] args)
        {
            // TO-DO ROLLER-COASTER METHOD IN A THREAD

            // TO-DO PASSENGER/THREAD
           // Console.WriteLine("start");
            Task.Run(() =>
            {
                RollerCoaster(); //run the roller coaster thread
            });

            for (int i = 0; i < passengersInLine; i++)
            {
                int count = i + 1;
                Task.Run(() => {
                    Passenger(count); //create number of passenger of thread of passengers, and run the passenger threads
                });

            }

            Console.ReadKey();
            


        }


        private static void Passenger(int count)
        {
            //passengers get on the roller coaster
            Console.WriteLine("passenger{0} is ready to get on", count);
            Entry.Wait(); //passengers ready to get on the rollar coaster
            Console.WriteLine("passenger{0} has boarded", count);

            s1.Wait();
            passengerOnBoard++;
            
            if (passengerOnBoard == rollerCoasterCapacity || passengersInLine - rollerCoasterCapacity <= 0) //if roaller coaster is full or there's no one waiting in line, the roller coaster starts
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

            if (remainingSeat == rollerCoasterCapacity || passengersInLine - rollerCoasterCapacity <= 0) 
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

            Entry.Release(rollerCoasterCapacity); //release entry to let passenger threads in.
            Thread.Sleep(1500);

            Console.WriteLine("All Boarded");
            Full.Wait();
            Thread.Sleep(1500);


            Exit.Release(rollerCoasterCapacity);
            Thread.Sleep(1500);


            Empty.Wait(); 
            Console.WriteLine("All Exited");
            Thread.Sleep(1500);

            passengersInLine = passengersInLine - rollerCoasterCapacity; //one ride is finished, the next group of passengers are ready to get on

            if (passengersInLine <= 0)
            {
                Environment.Exit(0); //when there is no passenger waiting in line, stop the program
            }



            else if (passengersInLine < rollerCoasterCapacity) //when the remaining passengers can not fill up the seats, run the last group of passenger, when finish, exit the program.
            {
                Entry.Release(rollerCoasterCapacity);
                Console.WriteLine("All Entered");
                Thread.Sleep(1500);

                Full.Wait();

                Thread.Sleep(1500);

                Exit.Release(rollerCoasterCapacity);

                Thread.Sleep(1500);

                Empty.Wait();
                Console.WriteLine("All Exited");
                Thread.Sleep(1500);

                Environment.Exit(0); 

            }

            else
            {
                Task.Run(() => {
                    RollerCoaster(); //when the number of remaining passengers are greater than the seats, run the next group of passengers.
                });
            }
        }


    }


}



