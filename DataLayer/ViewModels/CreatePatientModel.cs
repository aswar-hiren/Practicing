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
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Please enter Minimum eight characters, at least one letter and one number.")]
        public string? passwordhash { get; set; }
        [Required]
        public string? confirmpass { get; set; }
        [Required]
        public string? Email { get; set; }

    }
}
