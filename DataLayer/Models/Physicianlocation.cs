using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Physicianlocation
{
    public int Locationid { get; set; }

    public int Physicianid { get; set; }

    public DateTime? Createddate { get; set; }

    public string? Physicianname { get; set; }

    public string? Address { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public virtual Physician Physician { get; set; } = null!;
}
