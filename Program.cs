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

        private static DateTime Curtime;
        static void Main(string[] args)
        {


            #region taskManager

            #region CpuUsage
            // to calc usage for Idel
            double cpu_usage_For_idel = 0;

                    // label to return up by go to 
                    ShowTheListAgain:


                    // to get all running processes
                    Process[] process = Process.GetProcesses();

                Console.WriteLine("Count : {0}", process.Length);
                    foreach (var item in process.OrderBy(e => e.ProcessName))
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
                                Curtime = DateTime.Now;

                                // calc cpu usage

                                double cupUsge = (p.TotalProcessorTime.TotalMilliseconds) /
                                          Curtime.Subtract(p.StartTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                                cpu_usage_For_idel += cupUsge;
                                Console.WriteLine("  ---------> {0} Cpu : {1:0.0000000} %", p.ProcessName, cupUsge * 100);

                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine("the process not found");
                            }




                        }
                        else
                        {

                            // to calc the cpu usage for adle process

                            Process pp = Process.GetProcessById(procID);

                            //Thread.Sleep(100);
                            Console.WriteLine(" {0} Cpu : {1:0.0} %", pp.ProcessName, (100 - cpu_usage_For_idel * 100));


                        }




                    }
            #endregion



            //#region Kill And Change pirority
            Console.WriteLine("OPtions :");
            Console.WriteLine("if you need to kill process                    press =>> 1");
            Console.WriteLine("if you need to change prioirty of process      press =>> 2");
            Console.WriteLine("if you need to Close or Exit From application  press =>> 3");
            Console.WriteLine("if you need to Show task manager Again         press =>> 4");
            //to choose any process to use

            int Choice_Kill_Prioirty = Convert.ToInt32(Console.ReadLine());

            // to kill and change pirority of process

            #region to kill and change pirority of process

            switch (Choice_Kill_Prioirty)
            {

                case 1:
                    // to list all process before kill

                   
                    Console.WriteLine("enter process id to kill");

                    // to get that id for process to kill

                    int processid = Convert.ToInt32(Console.ReadLine());
                    Process.GetProcessById(processid).Kill();

                    // to list all process after kill

                    Console.WriteLine("The Process Is Kill");
                    Console.WriteLine("Please Wait To Return....");

                    Thread.Sleep(3000);

                    goto ShowTheListAgain;
                    

                case 2:
                    foreach (var item4 in process.OrderBy(s => s.ProcessName))
                    {
                        Console.WriteLine("Process Name   --> " + item4.ProcessName + " -  process id : " + item4.Id);
                    }
                    Console.WriteLine("enter process id to change priority");
                    processid = Convert.ToInt32(Console.ReadLine());

                    // this code for change priority for gthe current process by the ProcessPriorityClass  
                    Console.WriteLine("THE CURRENT PRIORITY IS  :{0} \n", Process.GetProcessById(processid).PriorityClass);

                    Console.WriteLine("Choosse the New priority ");

                    Console.WriteLine(@"Please Select Appropriate Pirority :
1=>> Normal
2=>> High
3=>> BelowNormal
4=>> RealTime
5=>> AboveNormal
6=>> Idle
7=>> Leave as it");
                    int priorityChoice = Convert.ToInt32(Console.ReadLine());


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







                case 3:
                    Environment.Exit(0);
                    break;

                case 4:
                    goto ShowTheListAgain;
                default:
                    goto ShowTheListAgain;



            }
            #endregion

            Thread.Sleep(3000);
            // to show the list more than time
            goto ShowTheListAgain;
            #endregion


        }
       
    }
}

