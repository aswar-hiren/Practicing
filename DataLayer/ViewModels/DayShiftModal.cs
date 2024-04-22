﻿using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class DayShiftModal
    {
        public DateTime currentDate { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int daysInMonth { get; set; }
        public DateTime firstDayOfMonth { get; set; }
        public int startDayIndex { get; set; }
        public string[] dayNames { get; set; }
        public List<ShiftDetailsmodal> shiftDetailsmodals { get; set; }
        public List<Physician> Physicians { get; set; }
    }
}
