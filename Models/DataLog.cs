using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedCrossItCheckingSystem.Models
{
    public class DataLog
    {
        private string department;
        private string emplyeeName;
        private string description;
        private DateTime logDate;

        public string Department { get => department; set => department = value; }
        public string EmplyeeName { get => emplyeeName; set => emplyeeName = value; }
        public string Description { get => description; set => description = value; }
        public DateTime LogDate { get => logDate; set => logDate = value; }
    }
}
