using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class City
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
