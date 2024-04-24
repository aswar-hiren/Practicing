using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? CityId { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Gender { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public DateTime? Birthdate { get; set; }

    public virtual City? CityNavigation { get; set; }
}
