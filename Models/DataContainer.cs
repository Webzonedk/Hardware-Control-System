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
        private string department;
        private string description;
        private DateTime dateStart;
        private DateTime dateEnd;
        private string status;
        private string deviceName;
        private string deviceType;
        private string accessories;
        private int caseID;

        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string Department { get => department; set => department = value; }
        public string Description { get => description; set => description = value; }
        public DateTime DateStart { get => dateStart; set => dateStart = value; }
        public DateTime DateEnd { get => dateEnd; set => dateEnd = value; }
        public string Status { get => status; set => status = value; }
        public string DeviceName { get => deviceName; set => deviceName = value; }
        public string DeviceType { get => deviceType; set => deviceType = value; }
        public string Accessories { get => accessories; set => accessories = value; }
        public int CaseID { get => caseID; set => caseID = value; }
    }
}
