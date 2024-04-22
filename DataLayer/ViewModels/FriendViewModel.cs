using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace DataLayer.ViewModels;

public partial class FriendViewModel
{
    [Required]
    public string? f_FirstName { get; set; }
    [Required]
    public string? f_LastName { get; set; }
    [Required]
    public string? f_Email { get; set; }
    [Required(ErrorMessage = "Phone Number Required!")]
    [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
    public string? f_PhoneNumber { get; set; }
    [Required]
    public string? relation { get; set; }

    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? details { get; set; }
    [Required]
    public DateOnly? birthdate { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Phone Number Required!")]
    [RegularExpression(@"^\+[0-9 \-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
    public string? PhoneNumber { get; set; }
    [Required]

    public string? Street { get; set; }
    [Required]

    public string? City { get; set; }
    [Required]

    public string? State { get; set; }
    [Required]

    public string? Zipcode { get; set; }


}


