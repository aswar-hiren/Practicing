using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class CreatePatientModel
    {
        [Required]
        public string? passwordhash { get; set; }
        [Required]
        public string? confirmpass { get; set; }
        [Required]
        public string? Email { get; set; }

    }
}
