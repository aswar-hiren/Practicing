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
    public class editProvidervm
    {  public string aspuserid { get; set; }
        public string username {  get; set; }
        public string password { get; set; }
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
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string phoneno {  get; set; }
        public string ml { get; set; }
        public string npinumber { get; set; }
        public string semail { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string adress1 { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string adress2 { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string city { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
        public string state { get; set; }
        [RegularExpression(@"^\d{6,}$", ErrorMessage = "Please enter, at least 6 digits.")]
        public string zip { get; set; }
        public string Photo { get; set; }
        public string sign { get; set; }
        public IFormFile signdevice { get; set; }
        public string bname { get; set; }

        public string website { get; set; }
        public string text { get; set; }
        public int phyid { get; set; }
        public int regionid { get; set; }
        public bool agreement { get; set; }
        public bool background { get; set; }
        public bool license { get; set; }

        public bool disclosure { get; set; }
        public bool compilance { get; set;}
        public IFormFile agreementdoc { get; set; }
        public IFormFile backgrounddoc { get; set; }
        public IFormFile disclosuredoc { get; set; }
        public IFormFile licensedoc { get; set; }
        public List<Physicianregion> phyregion { get; set; }
        public List<Region> region { get; set; }
        public List<int> AllCheck { get; set; }


    }
}
