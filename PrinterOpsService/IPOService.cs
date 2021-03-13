using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PrinterOpsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPOService
    {
        /*
        [OperationContract]
        [FaultContract(typeof(POServiceFault))]
        bool SetDisplayName(string serialNumber, string displayMessage, string alertMessage, string contactInfo, string locationInfo);

        [OperationContract]
        [FaultContract(typeof(POServiceFault))]
        bool SetPrinterState(string serialNumber, string model, string ipAddress, int pageCount, string upTime);*/

        [OperationContract]
        [FaultContract(typeof(POServiceFault))]
        bool SetColor(SNMPPrinterColorStateModel snmpPrinterColorStateModel);

        /*[OperationContract]
        [FaultContract(typeof(POServiceFault))]
        bool SetBranchSerial(string branchNumber, string serialNumber);*/

        [OperationContract]
        [FaultContract(typeof(POServiceFault))]
        bool SetUSBPrinterState(USBPrinterStateModel usbPrinterStateModel);
        //bool SetUSBPrinterState(string serialNumber, string model, int pageCount, string displayMessage, string alertMessage, string computerName);

        [OperationContract]
        [FaultContract(typeof(POServiceFault))]
        bool SetState(SNMPPrinterStateModel snmpPrinterStateModel);
        
        /*[OperationContract]
        string GetConnectionString();
        */

       
    }

    [DataContract]
    public class POServiceFault
    {
        public POServiceFault(string msg)
        {
            FaultMessage = msg;
        }

        [DataMember]
        public string FaultMessage;
    }


    [DataContract]
    public class USBPrinterStateModel
    {
        [DataMember]
        public string SerialNumber {get;set;}
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public int PageCount { get; set; }
        [DataMember]
        public string DisplayMessage { get; set; }
        [DataMember]
        public string AlertMessage { get; set; }
        [DataMember]
        public string ComputerName { get; set; }
    }

    [DataContract]
    public class SNMPPrinterStateModel
    {
        [DataMember]
        public string SerialNumber { get; set; }
        [DataMember]
        public string DisplayMessage { get; set; }
        [DataMember]
        public string AlertMessage { get; set; }
        [DataMember]
        public string ContactInfo { get; set; }
        [DataMember]
        public string LocationInfo { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public int PageCount { get; set; }
        [DataMember]
        public string Uptime { get; set; }
    }


     [DataContract]
    public class SNMPPrinterColorStateModel
    {
         [DataMember]
        public string ColorNameBlack { get; set; }
         [DataMember]
        public int TonerRemainingBlack { get; set; }
         [DataMember]
        public int MaxLevelBlack { get; set; }
         [DataMember]
        public string ColorNameCyan { get; set; }
         [DataMember]
        public int TonerRemainingCyan { get; set; }
         [DataMember]
        public int MaxLevelCyan { get; set; }
         [DataMember]
        public string ColorNameMagenta { get; set; }
         [DataMember]
        public int TonerRemainingMagenta { get; set; }
         [DataMember]
        public int MaxLevelMagenta { get; set; }
         [DataMember]
        public string ColorNameYellow { get; set; }
         [DataMember]
        public int TonerRemainingYellow { get; set; }
         [DataMember]
        public int MaxLevelYellow { get; set; }
         [DataMember]
        public string SerialNumber { get; set; }
    }
}
