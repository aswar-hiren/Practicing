using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class Schedulingvm
    {
    public List<Shiftdetail> shifts { get; set; } 
        public List<Physician> phy { get; set; }
        public DateTime date {  get; set; }
        public List<DateTime> datesonly { get; set; } 
    }
}
