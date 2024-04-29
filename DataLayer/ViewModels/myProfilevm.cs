using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class myProfilevm
    {
        public readonly string AspnetUserId;
        public readonly object Firstname;

        public string username { get; set; }
        [Required]
        public string password { get; set; }
   
        public string aspuserId { get; set; }
        public int status { get; set; }
        public int role {  get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string firstname { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string lastname { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        public string cemail { get; set; }

        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string phone { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string adress1 { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string adress2 { get; set;}
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string city { get; set; }
        public string state { get; set; }
        [Required]
        [RegularExpression(@"^\d{6,}$", ErrorMessage = "Please enter, at least 6 digits.")]
        public string zip {  get; set; }
        [Required]
        public string phoneno {  get; set; }
        [Required]
        public Admin admin { get; set; }
        public int region {  get; set; }
        public List<Region> regions { get; set; }
       
      public List<Adminregion> adminregion { get; set; }
        [Required]
        public List<int> AllCheck {  get; set; }

    }
}
