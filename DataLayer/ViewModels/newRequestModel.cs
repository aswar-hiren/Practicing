using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class newRequestModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string firstName {  get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string phoneNumber { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        public string birthDate { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string street { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string city { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string state { get; set; }
        [Required]
        [RegularExpression(@"^\d{6,}$", ErrorMessage = "Please enter, at least 6 digits.")]
        public string zipCode { get; set; }
   
        public int room {  get; set; }
        public string notes { get; set; }
    }
}
