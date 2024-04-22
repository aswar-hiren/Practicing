using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Aspnetuserrole
{
    public int? Userid { get; set; }

    public int? Roleid { get; set; }

    public int Id { get; set; }

    public virtual User? User { get; set; }
}
