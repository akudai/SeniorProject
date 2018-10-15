using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp_V2
{
    class Program
    {
        private static int C;
        private static int R;
        private static int E;
        private static int EW;
        private static int RW;
        private static int EM;
        private static int RM;
        private static int T; //Total Rows

        static void Main(string[] args)
        {
            C = 21; //Days
            R = 8; //Daily Audits

            E = 5; //Level1 Employees
            EW = 7;//Level2 Employees
            EM = 5;//Level3 Employees

            //122
            var watch = System.Diagnostics.Stopwatch.StartNew();

            T = (R + RW + RM);//Total of the three Rows
            int iterationCount = 0;//To count Number of Iterations

            //string[] empL1 = System.IO.File.ReadAllLines(@"C:\Users\akudai\Documents\Visual Studio 2015\Projects\ConsoleApplication1\ConsoleApplication1\textfiles\dailyAudits.txt");
            //string[] empL2 = System.IO.File.ReadAllLines(@"");
            //string[] empL3 = System.IO.File.ReadAllLines(@"");
            //string[] auditD = System.IO.File.ReadAllLines(@"");
            //string[] auditM = System.IO.File.ReadAllLines(@"");
            //string[] auditW = System.IO.File.ReadAllLines(@"");

            //Console.WriteLine(empL1[1]);
            String[,] slots = new String[R, C];//for Daily slots

            int[] faults = new int[C];//To store Faults
            int faultCount = 0;
            int indexM;

            Random rn = new Random();

            //This is the code that helps us know which index does C belongs
            if (C % 2 == 0){//if even
                indexM = (C - 2);
            }
            else{//if odd
                indexM = (C - 1);
            }

            //Populate Daily Employeess
            for (int j = 0; j < C; j++)
            {
                for (int i = 0; i < R; i++)
                {
                    if (j == indexM)
                    {
                        slots[i, j] = "C" + rn.Next(EM);
                    }
                    else if ((j + 1) % 5 == 0 && j != 0)
                    {
                        slots[i, j] = "B" + rn.Next(EW);
                    }else
                    {
                        slots[i, j] = "A" + rn.Next(E);
                    }


                }   
            }
            Console.WriteLine(indexM);
            //print pattern (Daily)
            Console.WriteLine("Initial random assignment (Daily)");
            printSchedule(slots, R);

            faults = getFaults(slots, R);
            Console.WriteLine();

            ////print the array containing faults
            printFaults1(faults);
            Console.WriteLine();

            ////Count Number of Faults
            faultCount = getFaultCount(faults);

            Console.WriteLine("[The Fault Count is " + faultCount + "]");
            Console.WriteLine();

            do
            {
                iterationCount++;
                //Console.WriteLine("The Iteration count is " + iterationCount);
                for (int j = 0; j < C; j++)
                {
                    //If the column contains fault	  
                    if (faults[j] > 0)
                    {
                        //Repair the dailys
                        for (int i = 0; i < R; i++)
                        {
                            if (j == indexM)
                            {
                                slots[i, j] = "C" + rn.Next(EM);
                            }
                            else if ((j + 1) % 5 == 0 && j != 0)
                            {
                                slots[i, j] = "B" + rn.Next(EW);
                            }
                            else
                            {
                                slots[i, j] = "A" + rn.Next(E);
                            }
                        }
                        
                    }

                }

                //printSchedule(slots, R);

                //first initialize faults
                faults = new int[C];

                //Get the faults for repaired slots
                faults = getFaults(slots, R);
                //Console.WriteLine();

                //print the array containing faults
                //printFaults(faults);
                //Console.WriteLine();

                //Count Number of Faults
                faultCount = getFaultCount(faults);

                Console.WriteLine("[The Fault Count is " + faultCount + "]");
                //Console.WriteLine();

            } while (faultCount != 0);

            Console.WriteLine("The Iteration count is " + iterationCount);

            printSchedule(slots, R);
            printFaults(faults);
            Console.WriteLine("[The Fault Count is " + faultCount + "]");




            //// the code that you want to measure comes here
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;


            Console.WriteLine("Total Time elapsed :" + elapsedMs + "miliseconds which is approximately " + (elapsedMs/1000) +" secs");

        }//main class

        public static void printSchedule(String[,] ThisArr, int Row)
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    if (ThisArr[i, j] == null)
                    {
                        Console.Write(0 + "  ");
                    }
                    else
                    {
                        Console.Write(ThisArr[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        public static String[,] CombineArray(String[,] slots, String[,] slots2, String[,] slots3)
        {
            String[,] combinedArr = new String[T, C];

            Array.Copy(slots, 0, combinedArr, 0, (C * R));
            Array.Copy(slots2, 0, combinedArr, (C * R), (C * RW));
            Array.Copy(slots3, 0, combinedArr, (C * (R + RW)), (C * RM));

            return combinedArr;
        }

        public static int[] getFaults(String[,] ThisArr, int Row)
        {
            //Declare and initialize array of size C to store Faults    
            int[] thisFaults = new int[C];
            String temp1 = "";

            for (int j = 0; j < C; j++)
            {
                for (int i = 0; i < Row; i++)
                {
                    if (ThisArr[i, j] != null)
                    {
                        temp1 = ThisArr[i, j];
                        int flagCount1 = 0;
                        for (int k = 0; k < Row; k++)
                        {
                            if (ThisArr[k, j] != null)
                            {
                                if (ThisArr[k, j].Equals(temp1))
                                {
                                    flagCount1++;
                                }
                            }
                        }
                        if (flagCount1 > 2)
                        {
                            thisFaults[j]++;
                        }
                    }
                }
            }
            return thisFaults;
        }

        public static void printFaults(int[] ThisArr)
        {
            for (int j = 0; j < C; j++)
            {   
                Console.Write(ThisArr[j] + "  ");
            }
        }

        public static void printFaults1(int[] ThisArr)
        {
            for (int j = 0; j < C; j++)
            {
                if (ThisArr[j] > 0)
                {
                    Console.Write("1  ");
                }
                else
                {
                    Console.Write("0  ");

                }
            }
        }
        public static int getFaultCount(int[] ThisArr)
        {
            int faultCount = 0;
            for (int j = 0; j < C; j++)
            {
                if (ThisArr[j] != 0)
                {
                    faultCount++;
                }
            }
            return faultCount;
        }


        public static void Weekbreaking(String[,] slots){
            for (int j = 0; j < C; j++)
            {
                for (int i = 0; i < R; i++)
                {

                    //needs implementation
                }
            }

        }

        //A function that shuffles up an array send to it. 
        public static string[] reshuffle(string[] texts)
        {
            Random rn = new Random();
            // Knuth shuffle algorithm :: courtesy of Wikipedia :)
            for (int t = 0; t < texts.Length; t++)
            {
                string tmp = texts[t];
                int r = rn.Next(t, texts.Length);
                texts[t] = texts[r];
                texts[r] = tmp;
            }

            return texts;
        }
    }
}


