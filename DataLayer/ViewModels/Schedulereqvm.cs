using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class Schedulereqvm
    {
        public int total { get; set; }
        public int CurrentPage { get; set; }

        public double TotalPages { get; set; }
        public int PageSize { get; set; }

        public List<Shiftdetail> shiftdetail { get; set; }
        public IEnumerable<Shiftdetail> paginatedRequest { get; set; }

        public List<Region> region { get; set; }
        public List<int> allshift {  get; set; }
    }
}
