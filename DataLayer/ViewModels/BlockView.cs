using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class BlockView
    {
        public int reqid {  get; set; }
        public int reqtypeid { get; set; }
        [Required] 
        public string TransNotes { get; set; }

        public string Patientname { get; set; }

        public string email { get; set; }

        public string phonenumber { get; set; } 
        public int status { get; set; }
    }
}
