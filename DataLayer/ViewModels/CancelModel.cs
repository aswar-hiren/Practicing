using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class CancelModel
    {
        public int reqid {  get; set; }
        public int phyid { get; set; }
        public string patientname { get; set; }
        public string TransCancelNotes { get; set; }
        [Required]
        public string reason { get; set; }
    }
}
