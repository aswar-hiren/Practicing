using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace DataLayer.ViewModels;

public partial class BusinessViewModel
{
    [Required]
    public string? b_FirstName { get; set; }
    [Required]
    public string? b_LastName { get; set; } 
    [Required]
    public string? b_Email { get; set; }
    [Required(ErrorMessage = "Phone Number Required!")]
    [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
    public string? b_PhoneNumber { get; set; }
    [Required]
    public string? b_caseno { get; set; }
    [Required]

    public string? b_name { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? details { get; set; }
    [Required]
    public DateOnly? date { get; set; }
    [Required]
    public string? Email { get; set; }
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


}

