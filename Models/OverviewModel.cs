using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedCrossItCheckingSystem.Models
{
    public class OverviewModel
    {
        private List<DataContainer> containers;
        //private Sort.SortOptions caseID;
        private int caseIDCount;
        private string filter;
        public List<DataContainer> Containers { get => containers; set => containers = value; }
        public int CaseIDCount { get => caseIDCount; set => caseIDCount = value; }
        public string Filter { get => filter; set => filter = value; }

        //public Sort.SortOptions CaseID { get => caseID; set => caseID = value; }

        public OverviewModel()
        {
            containers = new List<DataContainer>();
        }
    }

    public class Sort
    {
        public enum SortOptions { none,ascending,descending}
        public SortOptions Sortoption;
    }
}
