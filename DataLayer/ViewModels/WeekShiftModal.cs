using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class WeekShiftModal
    {
        public int startdate { get; set; }
        public int enddate { get; set; }
        public List<int> datelist { get; set; }
        public string[] dayNames { get; set; }
        public List<ShiftDetailsmodal> shiftDetailsmodals { get; set; }
        public List<Physician> Physicians { get; set; }
    }
}
