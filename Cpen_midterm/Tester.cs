using System;
using System.Collections.Generic;
namespace MTQ2
{
    public class Tester
    {
         static int Main() {
              //===================================================================
              // TODO: generate data and call the Tester Functions in try/catch
              // format like lab 3
              //===================================================================

              try
              {
                Console.WriteLine("****BinarySearch Testing Begins****");

                int[] testArray = new int[] {10,10,20,30,56,78,80 };
                TestBinarySearch(testArray, 10, 0);
                TestBinarySearch(testArray, 20,2);
                TestBinarySearch(testArray, 80, 6);


                

                Console.WriteLine("****Successfully Tested BinarySearch****");
                Console.WriteLine("****GeneratePrime Testing Begins****");
                List<int> expectedList = new List<int> { 2, 3, 5, 7 };
                TestGeneratePrimes(8, expectedList);

                
                Console.WriteLine("****Successfully Tested GeneratePrime****");
                Console.WriteLine("****IsPrime Testing Begins****");
                TestIsPrime(7,true);
                TestIsPrime(8, false);
                TestIsPrime(2, true);
                TestIsPrime(60493, true);
                TestIsPrime(100000, false);

                Console.WriteLine("****Successfully Tested IsPrime****");
                Console.WriteLine("****Done Testing");



            }
              catch (UnitTestException ute) {
                  Console.WriteLine(ute);
              }

              return 0;
          }





          /**
          * Unit Tests for binary_search
          * @throws TestException if a test fails
          */
         static void TestBinarySearch(int[]array,int n,int expected) {


            int result = HelperFunctions.BinarySearch(array, n);
           // Console.WriteLine(result);
            if (result != expected)
            {
                throw new Exception("test fails");
            }




            //===================================================================
            // TODO: Do something with the results and expected value
            //===================================================================


        }

        /**
         * Unit tests for IsPrime
         * @throws TestException if a unit test fails
         */
         static void TestIsPrime(int n,bool expected)
        {
          

           bool result = HelperFunctions.IsPrime(n);
            if (result != expected)
            {
                throw new Exception("test fails");
            }


          //  Console.WriteLine("result="+result);
          //===================================================================
          // TODO: Do something with the results and expectation value
          //===================================================================


        }

        /**
         * Unit Tests for generate_primes
         * @throws TestException if a unit test fails
         */
        static void TestGeneratePrimes(int n, List<int> expectedList) {


          List<int> results = HelperFunctions.GeneratePrimes(n);

            /*foreach(int a in results)
            {
                Console.WriteLine(a);
            }
            */
            for (int i = 0;i < results.Count; i++)
            {

                if (results[i] != expectedList[i])
                {
                    throw new Exception("test fails");
                }

            }
            
           
            //===================================================================
            // TODO: Do something with the results and expected value
            //===================================================================


        }

     
    }
}
