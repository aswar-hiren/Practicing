using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace DataLayer.ViewModels;

public partial class ConciegeViewModel
{
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string? c_FirstName { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string? c_LastName { get; set; }
    [Required]
    public string? c_Email { get; set; }
    [Required(ErrorMessage = "Phone Number Required!")]
    [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
    public string? c_PhoneNumber { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string? c_pname { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string? FirstName { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]

    public string? LastName { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]
    public string? details { get; set; }
    [Required]

    public DateOnly? date { get; set; }
    [Required]
    public string?       email { get; set; }
    [Required(ErrorMessage = "Phone Number Required!")]
    [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
    public string? PhoneNumber { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]

    public string? Street { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]

    public string? City { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Please enter, at least three letter and only Charcter.")]

    public string? State { get; set; }
    [Required]
    [RegularExpression(@"^\d{6,}$", ErrorMessage = "Please enter, at least 6 digits.")]

    public string? Zipcode { get; set; }


}


