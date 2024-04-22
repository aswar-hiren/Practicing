using DataLayer.Models;

using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;


namespace DataLayer.ViewModels
{
    public class CreateProvidervm
    {
        public List<Region>? Regions { get; set; }
        public List<int> region { get; set; }
        public List<Role>? RoleList { get; set; }

        public List<Adminregion> adminRegions { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string MedicalLicense { get; set; }

        [Required]
        public string NpiNumber { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        public string BusinessName { get; set; }

        [Required]
        public string BusinessWebsite { get; set; }


        public IFormFile Photo { get; set; }

        [Required]
        public string AdminNotes { get; set; }

        public IFormFile AgreementDoc { get; set; }

        public IFormFile BackgroundDoc { get; set; }
        public IFormFile LicienceDc { get; set; }

        public IFormFile NonDisclosureDoc { get; set; }
        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

    }
}