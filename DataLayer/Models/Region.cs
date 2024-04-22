using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Region
{
    public int Regionid { get; set; }

    public string Name { get; set; } = null!;

    public string? Abbreviation { get; set; }

    public virtual ICollection<Adminregion> Adminregions { get; } = new List<Adminregion>();

    public virtual ICollection<Business> Businesses { get; } = new List<Business>();

    public virtual ICollection<Concierge> Concierges { get; } = new List<Concierge>();

    public virtual ICollection<Physicianregion> Physicianregions { get; } = new List<Physicianregion>();

    public virtual ICollection<Requestclient> Requestclients { get; } = new List<Requestclient>();

    public virtual ICollection<Shiftdetailregion> Shiftdetailregions { get; } = new List<Shiftdetailregion>();
}
