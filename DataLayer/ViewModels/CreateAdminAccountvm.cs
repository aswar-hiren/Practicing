using DataLayer.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class CreateAdminAccountvm
    {
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Please enter Minimum eight characters, at least one letter and one number.")]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }

        public List<Role> Rolelist { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        //[DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Compare("Email", ErrorMessage = "Email & Confirm Email Should be match")]
        public string confirmEmail { get; set; }

        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string Phonenumber { get; set; }

        public List<Adminregion> adminRegions { get; set; }

        public List<Region> regions { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string? Address2 { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string AltPhone { get; set; }




    }
}
