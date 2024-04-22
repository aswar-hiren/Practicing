using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class ResetPassViewModel
    {

        [Required]
        public string? confirmpass { get; set; }
        [Required]
        public string? pass { get; set; }
    }
}
