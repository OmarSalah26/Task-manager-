using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;


namespace os
{
    class Program
    {
    public   static  double  cpu_usage_For_idel = 0;
        private static DateTime Curtime;
        static void ListOfProcesses()
        {
            #region CpuUsage
            // to calc usage for Idel
          

        // label to return up by go to 


            // to get all running processes

            Console.WriteLine("Count : {0}", Process.GetProcesses().Length);
            foreach (var item in Process.GetProcesses().OrderBy(e => e.ProcessName))
            {
                Console.Write(" Process Name   --> " + item.ProcessName + " -  process id : " + item.Id);

                // to get process id 
                int procID = item.Id;


                // to get specific process by id 
                if (procID != 0)
                {
                    try
                    {
                        Process p = Process.GetProcessById(procID);
                        // to give the current time
                        Curtime = DateTime.Now;

                        // calc cpu usage

                        double cupUsge = (p.TotalProcessorTime.TotalMilliseconds) /
                                  Curtime.Subtract(p.StartTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                        // i will use it to calc the total processor time for all processes
                        cpu_usage_For_idel += cupUsge;
                        Console.WriteLine("  --------->  Cpu : {0:0.0000000} %",  cupUsge * 100);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("the process not found");
                    }




                }
               




            }

            CalcprocessIdel(0, cpu_usage_For_idel);

            #endregion

        }

        static void CalcprocessIdel(int procID ,double cpu_usage_For_idel) {
            

                // to calc the cpu usage for adle process

                Process pp = Process.GetProcessById(procID);

                //Thread.Sleep(100);
                Console.WriteLine(" {0}  processId : 0 Cpu Usage : {1:0.0} %", pp.ProcessName, (100 - cpu_usage_For_idel * 100));


            


        }
            static void Main(string[] args)
        {


        #region taskManager

        ShowTheListAgain:

            ListOfProcesses();



            //#region Kill And Change pirority

            Console.WriteLine("\n \n OPtions :");
            Console.WriteLine("if you need to kill process                            press =>> 1");
            Console.WriteLine("if you need to change prioirty of process              press =>> 2");
            Console.WriteLine("if you need to test individual process                 press =>> 3");
            Console.WriteLine("if you need to Show task manager more than one time    press =>> 4");

            Console.WriteLine("if you need to Show task manager Again                 press =>> 5");
            Console.WriteLine("if you need to Close or Exit From application          press =>> 6");

            //to choose any process to use
            again6:
            try { 
            int Choice_Kill_Prioirty = Convert.ToInt32(Console.ReadLine());

            // to kill and change pirority of process

            #region to kill and change pirority of process

            switch (Choice_Kill_Prioirty)
            {

                //to kill process
                case 1:
                    // to list all process before kill

                    again3:
                    Console.WriteLine("enter process id to kill");

                    // to get that id for process to kill

                   
                    int processid = 0;
                          
                    try
                    {
                        processid = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OOOH... Please enter number");
                        goto again3;
                    }

                    try
                    {
                        Process.GetProcessById(processid).Kill();
                    }
                    catch (Exception ex) {
                        Console.WriteLine("OOOH...  the process is not found");
                            Console.WriteLine("Please -press 1 to enter process id again ");
                            Console.WriteLine("       -press 2 to return to the task manager");
                            int Choice = Convert.ToInt32(Console.ReadLine());
                            switch (Choice)
                            {
                                case 1:
                                    goto again3;
                                default:
                                    goto ShowTheListAgain;


                            }
                        }

                    // to list all process after kill

                    Console.WriteLine("The Process Is Kill");
                    Console.WriteLine("Please Wait To Return....");

                    Thread.Sleep(2000);

                    goto ShowTheListAgain;
                    
                    // to change pirority
                case 2:
                     again:
                    Console.WriteLine("enter process id to change priority");
                    try
                    {
                        processid = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OOOH... Please enter number ");
                        goto again;
                    }

                        // this code for change priority for gthe current process by the ProcessPriorityClass  

                        try
                        {
                            Console.WriteLine("THE CURRENT PRIORITY IS  :{0} \n", Process.GetProcessById(processid).PriorityClass);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("OOOH...  the process is not found");
                            Console.WriteLine("Please -press 1 to enter process id again ");
                            Console.WriteLine("       -press 2 to return to the task manager");
                            int Choice = Convert.ToInt32(Console.ReadLine());
                            switch (Choice)
                            {
                                case 1:
                                    goto again;
                                default:
                                    goto ShowTheListAgain;


                            }
                        }

                    Console.WriteLine("Choosse the New priority ");

                    Console.WriteLine(@"Please Select Appropriate Pirority :
1=>> Normal
2=>> High
3=>> BelowNormal
4=>> RealTime
5=>> AboveNormal
6=>> Idle
7=>> Leave as it");
                    int priorityChoice = 0;

                    again1:
                    try
                    {
                        priorityChoice = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OOOH... Please enter number");
                        goto again1;
                    }

                    // pirority choices
                    switch (priorityChoice)
                    {
                        case 1:
                            Process.GetProcessById(processid).PriorityClass = ProcessPriorityClass.Normal;
                            break;
                        case 2:
                            Process.GetProcessById(processid).PriorityClass = ProcessPriorityClass.High;
                            break;
                        case 3:
                            Process.GetProcessById(processid).PriorityClass = ProcessPriorityClass.BelowNormal;
                            break;
                        case 4:
                            Process.GetProcessById(processid).PriorityClass = ProcessPriorityClass.RealTime;
                            break;
                        case 5:
                            Process.GetProcessById(processid).PriorityClass = ProcessPriorityClass.AboveNormal;
                            break;
                        case 6:
                            Process.GetProcessById(processid).PriorityClass = ProcessPriorityClass.Idle;
                            break;

                    }


                    Console.WriteLine("The Priority Is  :{0} ", Process.GetProcessById(processid).PriorityClass);
                    Console.WriteLine("Please Wait To Return....");


                    Thread.Sleep(2000);
                    // to show the list more than time
                    goto ShowTheListAgain;







                // for test individual process
                case 3:
                    again8:
                        again0:
                        Console.Write(" Please Enter the process Id ");
                        
                    int procID = 0;
                        try { procID = Convert.ToInt32(Console.ReadLine());


                        } catch (Exception ex) {

                            Console.WriteLine("OOOH... Please enter  number");
                           goto again0;
                        }
                    if (procID != 0)
                    {
                        try
                        {
                            while (!Console.KeyAvailable) { 
                            Process p = Process.GetProcessById(procID);
                            Curtime = DateTime.Now;

                            // calc cpu usage

                            double cupUsge = (p.TotalProcessorTime.TotalMilliseconds) /
                                      Curtime.Subtract(p.StartTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                        
                            Console.WriteLine("  ---------> {0} Cpu : {1:0.0000000} %", p.ProcessName, cupUsge * 100);
                                Console.WriteLine("please press any key to return ");
                                while (Console.KeyAvailable)
                                    goto ShowTheListAgain;
                }
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(" OOOH... the process not found");
                               goto again8;
                            }




                        }

                    goto ShowTheListAgain;
                    // to show the list more than time
                    case 4:
                        while (!Console.KeyAvailable)

                       {
                            ListOfProcesses();

                            Console.WriteLine("Please press any key to return to task manager");
                            Thread.Sleep(2000);
                        }
                        goto ShowTheListAgain;
                case 5:

                    goto ShowTheListAgain;
                // To close the console

                case 6:
                    Environment.Exit(0);
                    break;

                // to show the list more than time

                default:
                    goto ShowTheListAgain;



            }
                #endregion

            }
            catch (Exception ex) { Console.WriteLine("OOOH... Please enter  number");
                Console.WriteLine("\n \n OPtions :");
                Console.WriteLine("if you need to kill process                    press =>> 1");
                Console.WriteLine("if you need to change prioirty of process      press =>> 2");
                Console.WriteLine("if you need to test individual process         press =>> 3");

                Console.WriteLine("if you need to Show task manager Again         press =>> 4");
                Console.WriteLine("if you need to Close or Exit From application  press =>> 5");
                goto again6;

            }
            #endregion


        }
       
    }
}

