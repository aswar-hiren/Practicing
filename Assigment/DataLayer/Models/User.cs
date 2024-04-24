using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }
}
