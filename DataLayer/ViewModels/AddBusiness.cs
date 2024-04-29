using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class AddBusiness
    {
        public List<Healthprofessionaltype> hptype { get; set; }
        [Required]


        public string bname { get; set; }
        [Required]

        public int? proffesion {  get; set; }
        [Required]
        [RegularExpression(@"^\d{6,}$", ErrorMessage = "Please enter, at least 6 digits.")]
        public string faxno { get; set; }
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string contact { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]

        public string bcontact { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]

        public string city { get; set; }
        [Required]
        [RegularExpression(@"^\d{6,}$", ErrorMessage = "Please enter, at least 6 digits.")]

        public string pincode { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]

        public string state { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]


        public string street { get; set; }
        public int vendorid {  get; set; }
    }
}
