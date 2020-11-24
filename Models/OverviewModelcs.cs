using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedCrossItCheckingSystem.Models
{
    public class OverviewModelcs
    {
        private List<DataContainer> containers;
        private Sort.SortOptions caseID;
        public List<DataContainer> Containers { get => containers; set => containers = value; }
        public Sort.SortOptions CaseID { get => caseID; set => caseID = value; }

        public OverviewModelcs()
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
