using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class CreateShift
    {
        public List<Region>? Region { get; set; }
        public int PhysicianRegion { get; set; }
        public int PhysicianId { get; set; }
        public string ShiftDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsRepeat { get; set; }
        public List<int>? RepeatDays { get; set; }
        public int RepeatUpto { get; set; }
    }
}
