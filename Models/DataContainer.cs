using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;


namespace RedCrossItCheckingSystem.Models
{
    public class DataContainer
    {
        private string serialNumber;

        private string status;
        private string deviceName;
        private string deviceType;
        private string accessories;
        private int caseID;
        private bool isValid;
        private List<DataLog> dataLogs;
        private int logCount;


        public string SerialNumber { get => serialNumber; set => serialNumber = value; }


        public string Status { get => status; set => status = value; }


        public string DeviceName { get => deviceName; set => deviceName = value; }


        public string DeviceType { get => deviceType; set => deviceType = value; }


        public string Accessories { get => accessories; set => accessories = value; }

        public int CaseID { get => caseID; set => caseID = value; }

        public bool IsValid { get => isValid; set => isValid = value; }
        public List<DataLog> DataLogs { get => dataLogs; set => dataLogs = value; }
        public int LogCount { get => logCount; set => logCount = value; }

        public DataContainer()
        {
            dataLogs = new List<DataLog>();
        }
    }
}
