﻿using DataLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class ShiftDetailsmodal
    {
        public int Shiftid { get; set; }
        public int Physicianid { get; set; }
        public string PhysicianName { get; set; }
        public DateOnly Startdate { get; set; }
        public BitArray Isrepeat { get; set; } = null!;
        public string? Weekdays { get; set; }
        public int? Repeatupto { get; set; }
        public int Shiftdetailid { get; set; }
        public DateTime Shiftdate { get; set; }
        public int? Regionid { get; set; }
        public string region { get; set; }
        public TimeOnly Starttime { get; set; }
        public TimeOnly Endtime { get; set; }
        public short Status { get; set; }
        public BitArray Isdeleted { get; set; }
        public string? Eventid { get; set; }

        public List<Region> regions { get; set; }
        public List<Physician> physics { get; set; }
        public string regionname { get; set; }
    }
}
