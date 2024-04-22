using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataLayer.ViewModels
{
    public class EditViewShift
    {
        public int ShiftDetailId { get; set; }
        public int PhysicianRegionVS { get; set; }
        public string? PhysicianRegionName { get; set; }
        public int PhysicianIdVS { get; set; }
        public string? PhysicianName { get; set; }
        public string ShiftDateVS { get; set; }
        public TimeOnly StartTimeVS { get; set; }
        public TimeOnly EndTimeVS { get; set; }
    }
}
