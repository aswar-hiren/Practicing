using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class AssignModel
    {
        public List<Physician> Physicians { get; set; }
        public List<Region> Regions { get; set; }
        
       public int reqid { get; set; }
        [Required]
        public string TransNotes { get; set; }

        public int phy_id { get; set; } 
        public string email { get; set; }
        public string text { get; set; }
       
    }
}
