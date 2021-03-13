using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceModel;
#if DEBUG
using System.Diagnostics;
#endif
using PrinterOpsSrv.POServiceRef;
using AutoMapper;




namespace PrinterOpsSrv
{
    public class SNMPLib
    {


        /// <summary>
        /// Установка состояния принтера
        /// </summary>
        /// <param name="ipAddress"></param>
        public void SetStates(VersionCode version, string ipAddress, OctetString community, int timeout)
        {
            IPEndPoint IpEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), 161);
            SNMPPrinterStateModel snmpPrinterState = SNMPGet(version, IpEndPoint, community, timeout);
            if (snmpPrinterState != null)
            {
                snmpPrinterState.IPAddress = ipAddress;
                snmpPrinterState.AlertMessage = SNMPAlertMessage(version, IpEndPoint, community, timeout);
                // Record date to DB
                saveToDB(snmpPrinterState);
            }

            SNMPPrinterColorStateModel snmpPrinterColorState = SNMPGetColor(version, IpEndPoint, community, timeout);
            if (snmpPrinterColorState != null)
            {
                snmpPrinterColorState.SerialNumber = snmpPrinterState.SerialNumber ?? "";
                saveToDBforColor(snmpPrinterColorState);
            }


        }

        /// <summary>
        /// Return ASCII string from UTF-8
        /// </summary>
        /// <param name="var"></param>
        /// <returns></returns>
        string UTFtoASCII(ISnmpData var)
        {
            string str = "";
#if DEBUG
            //Console.WriteLine("var.TypeCode=" + var.TypeCode.GetType().ToString());
            //Console.WriteLine("var.TypeCode=" + var.TypeCode.ToString());
#endif
            if (var.TypeCode.ToString() != "NoSuchInstance")
            {
                OctetString raw = (OctetString)var;
                if (raw != null)
                {
                    byte[] bytes = raw.GetRaw();  //var.ToBytes();
                    char[] chars = Encoding.UTF8.GetChars(bytes);
                    str = new string(chars);
                }
                else
                {
                    str = "";
                }
            }
            return str;
        }

        /// <summary>
        /// Save these below parameters to DB
        /// </summary>
        /// <param name="snmpPrinterStateModel"></param>
        void saveToDB(SNMPPrinterStateModel snmpPrinterStateModel)
        {

            if (snmpPrinterStateModel != null)
            {
#if DEBUG
                Console.WriteLine("serialNumber=" + snmpPrinterStateModel.SerialNumber);
                Console.WriteLine("alertMessage=" + snmpPrinterStateModel.AlertMessage);
                Console.WriteLine("contactInfo=" + snmpPrinterStateModel.ContactInfo);
                Console.WriteLine("locationInfo=" + snmpPrinterStateModel.LocationInfo);
                Console.WriteLine("ipAddress=" + snmpPrinterStateModel.IPAddress);
                Console.WriteLine("PageCount=" + snmpPrinterStateModel.PageCount.ToString());
                Console.WriteLine("Uptime=" + snmpPrinterStateModel.Uptime);
                Console.WriteLine("DisplayMessage=" + snmpPrinterStateModel.DisplayMessage);
                Console.WriteLine("Model=" + snmpPrinterStateModel.Model);

#endif
                POServiceClient proxy = new POServiceClient("NetTCPBinding_IPOService");
#if DEBUG
                Console.WriteLine("After");
#endif
                POServiceRef.SNMPPrinterStateModel snmpPrinterState = new POServiceRef.SNMPPrinterStateModel();
                Mapper.CreateMap<SNMPPrinterStateModel, POServiceRef.SNMPPrinterStateModel>();
                Mapper.Map(snmpPrinterStateModel, snmpPrinterState);

                proxy.Open();


#if DEBUG
                if (proxy.SetState(snmpPrinterState))
                {
                    Console.WriteLine("***** Set state is Successed! *************");
                    Console.WriteLine("Success!");
                }
                else
                {
                    Console.WriteLine(" ******  Set state is Failed! ***************");
                    Console.WriteLine("Failed");
                }
#else
            proxy.SetState(snmpPrinterState);
#endif
                proxy.Close();
            }
        }

        /// <summary>
        /// Установка CMY цветов
        /// </summary>
        /// <param name="snmpPrinterColorStateModel"></param>
        void saveToDBforColor(SNMPPrinterColorStateModel snmpPrinterColorStateModel)
        {

            if (snmpPrinterColorStateModel != null)
            {
#if DEBUG
                Console.WriteLine("MaxLevelCyan=" + snmpPrinterColorStateModel.MaxLevelCyan);
                Console.WriteLine("TonerRemainingCyan=" + snmpPrinterColorStateModel.TonerRemainingCyan);
                Console.WriteLine("TonerRemainingCyan=" + snmpPrinterColorStateModel.TonerRemainingCyan);
#endif
                POServiceClient proxy = new POServiceClient("NetTCPBinding_IPOService");
                POServiceRef.SNMPPrinterColorStateModel snmpPrinterColorState = new POServiceRef.SNMPPrinterColorStateModel();
                Mapper.CreateMap<SNMPPrinterColorStateModel, POServiceRef.SNMPPrinterColorStateModel>();
                Mapper.Map(snmpPrinterColorStateModel, snmpPrinterColorState);
                proxy.Open();
#if DEBUG
                if (proxy.SetColor(snmpPrinterColorState))
                {
                    Console.WriteLine("Success Color");
                }
                else
                {
                    Console.WriteLine("Failed Color");
                }
#else
                proxy.SetColor(snmpPrinterColorState);

#endif
                proxy.Close();
            }
        }





        /// <summary>
        /// Check whether data is integer
        /// </summary>
        /// <param name="snmpData"></param>
        /// <returns></returns>
        int CheckVariableDataForInt(ISnmpData snmpData)
        {
            string snmpStirng = snmpData.ToString();
            int res = 0;
            if (snmpStirng != "NoSuchInstance")
            {
                res = Convert.ToInt32(snmpStirng);
            }
            return res;
        }


        /// <summary>
        /// Get SNMP Printer Satate
        /// </summary>
        /// <param name="ipAddress">IPAddress of host</param>
        /// <returns>SNMPPrinterStateModel</returns>
        SNMPPrinterStateModel SNMPGet(VersionCode version, IPEndPoint ipEndPoint, OctetString community, int timeout)
        {
            /*string SerialNumber = "";
            string DisplayMessage = "";
            string AlertMessage = "";
            string ContactInfo = "";
            string LocationInfo = "";
            string Model = "";
            
            int PageCount = 0;
            string Uptime = "";*/

            SNMPPrinterStateModel snmpPrinterStateModel = new SNMPPrinterStateModel();

            List<Variable> variable = new List<Variable>();
            variable.Add(new Variable(new ObjectIdentifier("1.3.6.1.4.1.641.2.1.2.1.6.1"))); // SerialNumber for Lexmark
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.5.1.1.17.1"))); // SerialNumber for Xerox
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.16.5.1.2.1.1"))); //DisplayMessage
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.16.5.1.2.1.2"))); //DisplayMessage2
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.10.2.1.4.1.1"))); //PageCount
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.1.3.0"))); //Uptime
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.1.4.0"))); //ContactInfo
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.1.6.0"))); //LocationInfo
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.4.1.641.2.1.2.1.2.1"))); // Model for Lexmark
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.25.3.2.1.3.1")));  //Model for Xerox

            try
            {
                var result = Messenger.Get(version,
                        ipEndPoint,
                        community,
                        variable,
                        timeout);
                if (result != null)
                {
                    if (result[0].Data.ToString() != "NoSuchObject")
                    {
#if DEBUG
                        Console.WriteLine("SerialNumber=" + result[0].Data.ToString());
#endif
                        snmpPrinterStateModel.SerialNumber = result[0].Data.ToString();
                    }
                    else
                    {
#if DEBUG
                        Console.WriteLine("SerialNumber=" + UTFtoASCII(result[1].Data));
#endif
                        snmpPrinterStateModel.SerialNumber = result[1].Data.ToString();
                    }
                    if (result[8].Data.ToString() != "NoSuchObject")
                    {
#if DEBUG
                        Console.WriteLine("Model=" + result[8].Data.ToString());
#endif
                        snmpPrinterStateModel.Model = result[8].Data.ToString();
                    }
                    else
                    {
#if DEBUG
                        Console.WriteLine("Model=" + UTFtoASCII(result[9].Data));
#endif
                        snmpPrinterStateModel.Model = result[9].Data.ToString();
                    }
                    snmpPrinterStateModel.ContactInfo = UTFtoASCII(result[6].Data);
                    snmpPrinterStateModel.LocationInfo = UTFtoASCII(result[7].Data);
#if DEBUG
                    Console.WriteLine("PageCount=" + result[4].Data.ToString());
#endif
                    snmpPrinterStateModel.PageCount = Convert.ToInt32(result[4].Data.ToString());
#if DEBUG
                    Console.WriteLine("Uptime=" + result[5].Data.ToString());
#endif
                    snmpPrinterStateModel.Uptime = result[5].Data.ToString();
#if DEBUG
                    Console.WriteLine("DisplayMessage=" + UTFtoASCII(result[2].Data));
#endif
                    snmpPrinterStateModel.DisplayMessage = UTFtoASCII(result[2].Data);
#if DEBUG
                    Console.WriteLine("Begin DisplayMessage2");
                    Console.WriteLine("DisplayMessage2=" + UTFtoASCII(result[3].Data));
#endif
                    snmpPrinterStateModel.DisplayMessage += " " + UTFtoASCII(result[3].Data);
                }
            }
            catch
            {
#if DEBUG
                Console.WriteLine("EEEEEEERRRRRRRROOOOOOOOORRRRRRRRR!!!");
#endif
            }
            return snmpPrinterStateModel;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        string SNMPAlertMessage(VersionCode version, IPEndPoint ipEndPoint, OctetString community, int timeout)
        {
            string AlertMessage = "";
            List<Variable> resWalk = new List<Variable>();
            ObjectIdentifier oi = new ObjectIdentifier(".1.3.6.1.2.1.43.18.1.1.8"); //AlertMessage

            try
            {
                var resultWalk = Messenger.Walk(version,
                    ipEndPoint,
                    community,
                    oi,
                    resWalk,
                    timeout,
                    WalkMode.WithinSubtree);



                if (resultWalk != null)
                {
                    foreach (Variable rWalk in resWalk)
                    {

                        //Console.WriteLine(string.Join("", Encoding.UTF8.GetChars(rWalk.Data.ToBytes())));
                        //Console.WriteLine(UTFtoASCII(rWalk.Data));
                        AlertMessage += UTFtoASCII(rWalk.Data) + " ";
                    }
#if DEBUG
                    Console.WriteLine("AlertMessage=" + AlertMessage);
#endif
                }
            }
            catch
            {
#if DEBUG
                Console.WriteLine("Error resultWalk");
#endif
            }
            return AlertMessage;
        }


        SNMPPrinterColorStateModel SNMPGetColor(VersionCode version, IPEndPoint ipEndPoint, OctetString community, int timeout)
        {

            /*
             *     [string]$ColorName=Invoke-SNMPget $IPAddress ".1.3.6.1.2.1.43.12.1.1.4.1.1"
                    [int]$TonerRemaining=Invoke-SNMPget $IPAddress ".1.3.6.1.2.1.43.11.1.1.9.1.1"
                    [int]$TonerMaxLevel=Invoke-SNMPget $IPAddress ".1.3.6.1.2.1.43.11.1.1.8.1.1"

             */

            SNMPPrinterColorStateModel snmpPrinterColorState = new SNMPPrinterColorStateModel();

            List<Variable> variable = new List<Variable>();
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.12.1.1.4.1.1"))); // Color Name - Black
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.11.1.1.9.1.1"))); //is the maximum of black toner remaining
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.11.1.1.8.1.1"))); //is the maximum black toner level
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.12.1.1.4.1.2"))); //Color Name - Cyan
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.11.1.1.9.1.2"))); //is the maximum of cyan toner remaining
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.11.1.1.8.1.2"))); //is the maximum cyan toner level
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.12.1.1.4.1.3"))); //Color Name - Magenta
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.11.1.1.9.1.3"))); //is the maximum of magenta toner remaining
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.11.1.1.8.1.3"))); // is the maximum magenta toner level
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.12.1.1.4.1.4")));  //Color Name - Yellow
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.11.1.1.9.1.4"))); //is the maximum of yellow toner remaining
            variable.Add(new Variable(new ObjectIdentifier(".1.3.6.1.2.1.43.11.1.1.8.1.4"))); // is the maximum yellow toner level

            try
            {
                var result = Messenger.Get(version,
                        ipEndPoint,
                        community,
                        variable,
                        timeout);

                if (result != null)
                {
#if DEBUG
                    foreach (Variable res in result)
                    {

                        Console.WriteLine(res);

                    }
#endif

                    snmpPrinterColorState.ColorNameBlack = result[0].Data.ToString();
                    snmpPrinterColorState.TonerRemainingBlack = CheckVariableDataForInt(result[1].Data);
                    snmpPrinterColorState.MaxLevelBlack = CheckVariableDataForInt(result[2].Data);

                    snmpPrinterColorState.ColorNameCyan = result[3].Data.ToString();
                    snmpPrinterColorState.TonerRemainingCyan = CheckVariableDataForInt(result[4].Data);
                    snmpPrinterColorState.MaxLevelCyan = CheckVariableDataForInt(result[5].Data);

                    snmpPrinterColorState.ColorNameMagenta = result[6].Data.ToString();
                    snmpPrinterColorState.TonerRemainingMagenta = CheckVariableDataForInt(result[7].Data);
                    snmpPrinterColorState.MaxLevelMagenta = CheckVariableDataForInt(result[8].Data);

                    snmpPrinterColorState.ColorNameYellow = result[9].Data.ToString();
                    snmpPrinterColorState.TonerRemainingYellow = CheckVariableDataForInt(result[10].Data);
                    snmpPrinterColorState.MaxLevelYellow = CheckVariableDataForInt(result[11].Data);
                }
            }
            catch
            {
#if DEBUG
                Console.WriteLine("Error result Color");
#endif
            }

            return snmpPrinterColorState;
        }


    }
}
