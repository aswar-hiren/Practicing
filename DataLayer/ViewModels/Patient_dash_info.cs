
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace DataLayer.ViewModels;

public partial class Patient_dash_info
{
    [Required]

    public string FirstName { get; set; }
    [Required]
    public string relation { get; set; }
    [Required]
    public string details { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
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
    public DateOnly? BirthDate { get; set; }
   
    public IFormFile 
        Photo { get; set; }
 
    public DateTime date { get; set; }

    public string username { get; set; }
}