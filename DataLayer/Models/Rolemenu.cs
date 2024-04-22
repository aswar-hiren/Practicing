using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class RoleMenu
{
    public int Rolemenuid { get; set; }

    public int? Roleid { get; set; }

    public int? Menuid { get; set; }

    public virtual Menu? Menu { get; set; }

    public virtual Role? Role { get; set; }
}
