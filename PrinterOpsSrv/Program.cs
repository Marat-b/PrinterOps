using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
#if DEBUG
using System.Diagnostics;
#endif
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

using NCron;
using NCron.Fluent.Generics;
using NCron.Fluent.Crontab;
using NCron.Logging;
using NCron.Service;
using NCron.Integration.log4net;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;



namespace PrinterOpsSrv
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.Init(args, ServiceSetup);
        }

        static void ServiceSetup(ISchedulingService service)
        {
            
            service.LogFactory = new NCron.Integration.log4net.LogFactory();
            
            string cron = ConfigurationManager.AppSettings["cron"].ToString();

            service.At(cron).Run<MyJob>();
        }
    }


    class MyJob : NCron.CronJob
    {
        string IpAddress = "";
        VersionCode version = VersionCode.V2;
        OctetString community = new OctetString("public");
        int timeout = 6000;
        Ping ping = new Ping();
        SNMPLib snmpLib=new SNMPLib();

        public override void Execute()
        {
            /*StreamWriter sw = new StreamWriter(@"C:\SOFTZ\TEST\TestNcron\test.txt", true);
            sw.WriteLine(DateTime.Now.ToString());
            sw.Close();
           */

            string fileIpAddresses = ConfigurationManager.AppSettings["listAddresses"].ToString().Trim();
#if DEBUG
            Console.WriteLine(fileIpAddresses);

            Console.WriteLine(fileIpAddresses);
#endif
            if (File.Exists(fileIpAddresses))
            {
                StreamReader sr = new StreamReader(fileIpAddresses);
                while (!sr.EndOfStream)
                {
                    IpAddress = sr.ReadLine().Trim();
#if DEBUG
                    Console.WriteLine("IPAddress=" + IpAddress);
#endif
                    PingReply reply = ping.Send(IPAddress.Parse(IpAddress));
                    if (reply.Status == IPStatus.Success)
                    {

                        snmpLib.SetStates(version, IpAddress, community, timeout);
                    }

#if DEBUG
                    else
                    {
                        Console.WriteLine(IpAddress + " - not available!");
                    }
#endif



#if DEBUG
                    Console.WriteLine(IpAddress);
#endif
                }

                sr.Close();
            }
            else
            {
                Log.Error(() => fileIpAddresses+" is not exists");
            }

            ping.Dispose();

            
           

           
        }
                
    }
}
