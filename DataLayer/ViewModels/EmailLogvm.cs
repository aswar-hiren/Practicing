using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class EmailLogvm
    {
        public int userid { get; set; }

        public int CurrentPage { get; set; }

        public double TotalPages { get; set; }
        public int PageSize { get; set; }
        public int total { get; set; }
        public List<Emaillog> clients { get; set; }
        public List<Smslog> smslog { get; set; }
    }
}
