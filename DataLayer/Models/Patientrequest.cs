using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Patientrequest
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public long? Zipcode { get; set; }

    public DateOnly[]? BirthDate { get; set; }

    public long[]? PhoneNumber { get; set; }
}
