using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace DataLayer.ViewModels;

public partial class PatientInfo
{
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]

    public string FirstName { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string Details { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Please enter Minimum eight characters, at least one letter and one number.")]

    public string PasswordHash { get; set; }
    [Required]
    public string cPasswordHash { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string LastName { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string? Street { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string? City { get; set; }
    [Required]
    [RegularExpression(@"^\d{3,}$", ErrorMessage = "Please enter, at least 3 digits.")]
    public int  room { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string? State { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string? notes { get; set; }
    [Required]
    [RegularExpression(@"^\d{6,}$", ErrorMessage = "Please enter, at least 6 digits.")]

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
