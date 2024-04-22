using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace DataLayer.ViewModels;

public partial class PatientInfo
{
    [Required]
   
    public string FirstName { get; set; }
    [Required]
    public string Details { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    public string? Street { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public string? State { get; set; }
    [Required]
    public string? Zipcode { get; set; }
   
    [Required(ErrorMessage = "Phone Number Required!")]
    [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
    public string? PhoneNumber { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public DateOnly date { get; set; }
    public IFormFile
         Photo
    { get; set; }
}
