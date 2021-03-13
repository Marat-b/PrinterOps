using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PrinterOpsDBO;
using PrinterOpsDBO.Model;
using PrinterOpsService.Model;
using AutoMapper;


namespace PrinterOpsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class POService : IPOService
    {
        private PODBO poService;

        public POService()
        {
            poService = new PODBO();
        }
        /*
        public bool SetDisplayName(string serialNumber, string displayMessage, string alertMessage, string contactInfo, string locationInfo)
        {
            bool ret=false;
            try
            {
                ret=poService.SetDisplayName(serialNumber, displayMessage, alertMessage, contactInfo, locationInfo);
            }
            catch (Exception e)
            {
                var msg=e.Message;
                var reason="SetDisplayName Exception";
                throw new FaultException<POServiceFault>(new POServiceFault(msg),reason);
            }
            return ret;
        }

        public bool SetPrinterState(string serialNumber, string model, string ipAddress, int pageCount, string upTime)
        {
            bool ret = false;
            try
            {
                ret=poService.SetPrinterState(serialNumber, model, ipAddress, pageCount, upTime);
            }
            catch (Exception e)
            {
                var msg=e.Message;
                var reason="SetPrinterState Exception";
                throw new FaultException<POServiceFault>(new POServiceFault(msg),reason);
            }
            return ret;
        }*/

        public bool SetColor(SNMPPrinterColorStateModel snmpPrinterColorState)
        {
            bool ret = false;
            try
            {
                PrinterOpsDBO.Model.SNMPPrinterColorStateModel snmpPrinterColorStateDBO = new PrinterOpsDBO.Model.SNMPPrinterColorStateModel();
                Mapper.CreateMap<SNMPPrinterColorStateModel, PrinterOpsDBO.Model.SNMPPrinterColorStateModel>();
                Mapper.Map(snmpPrinterColorState, snmpPrinterColorStateDBO);
                ret = poService.SetColor(snmpPrinterColorStateDBO);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                var reason = "SetColor Exception";
                throw new FaultException<POServiceFault>(new POServiceFault(msg), reason);
            }
            return ret;
        }
        /*
        public bool SetBranchSerial(string branchNumber, string serialNumber)
        {
            bool ret = false;
            try
            {
                ret= poService.SetBranchSerial(branchNumber, serialNumber);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                var reason = "SetBranchSerial Exception";
                throw new FaultException<POServiceFault>(new POServiceFault(msg), reason);
            }
            return ret;
        }*/

        //public bool  SetUSBPrinterState(string serialNumber, string model, int pageCount, string displayMessage, string alertMessage, string computerName)
        public bool SetUSBPrinterState(USBPrinterStateModel usbPrinterStateModel)
        {
            bool ret = false;
            try
            {
                PrinterOpsDBO.Model.USBPrinterStateModel usbPrinterStateDBO = new PrinterOpsDBO.Model.USBPrinterStateModel();
                Mapper.CreateMap<USBPrinterStateModel, PrinterOpsDBO.Model.USBPrinterStateModel>();
                Mapper.Map(usbPrinterStateModel, usbPrinterStateDBO);
                ret = poService.SetUSBPrinterState(usbPrinterStateDBO);
                //ret = poService.SetUSBPrinterState(serialNumber, model, pageCount, displayMessage, alertMessage,computerName);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                var reason = "SetUSBPrinterState Exception :(";
                throw new FaultException<POServiceFault>(new POServiceFault(msg), reason);
            }
            return ret;
        }

        public bool SetState(SNMPPrinterStateModel snmpPrinterStateModel)
        {
            bool ret = false;
            try
            {
                PrinterOpsDBO.Model.SNMPPrinterStateModel snmpPrinterStateModelDBO = new PrinterOpsDBO.Model.SNMPPrinterStateModel();
                snmpPrinterStateModelDBO.AlertMessage = snmpPrinterStateModel.AlertMessage;
                snmpPrinterStateModelDBO.ContactInfo = snmpPrinterStateModel.ContactInfo;
                snmpPrinterStateModelDBO.DisplayMessage = snmpPrinterStateModel.DisplayMessage;
                snmpPrinterStateModelDBO.IPAddress = snmpPrinterStateModel.IPAddress;
                snmpPrinterStateModelDBO.LocationInfo = snmpPrinterStateModel.LocationInfo;
                snmpPrinterStateModelDBO.Model = snmpPrinterStateModel.Model;
                snmpPrinterStateModelDBO.PageCount = snmpPrinterStateModel.PageCount;
                snmpPrinterStateModelDBO.SerialNumber = snmpPrinterStateModel.SerialNumber;
                snmpPrinterStateModelDBO.Uptime = snmpPrinterStateModelDBO.Uptime;
                ret = poService.SetState(snmpPrinterStateModelDBO);
            }
            catch(Exception e)
            {
                var msg = e.Message;
                var reason = "SetState Exception :(";
                throw new FaultException<POServiceFault>(new POServiceFault(msg), reason);
            }
            return ret;
        }

        /*
        public string GetConnectionString()
        {
            return poService.GetConnectionString();
        }*/
    }
}
