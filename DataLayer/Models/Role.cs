﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Role
{
    public int Roleid { get; set; }

    public string Name { get; set; } = null!;

    public short Accounttype { get; set; }

    public string Createdby { get; set; } = null!;

    public DateTime Createddate { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public BitArray Isdeleted { get; set; } = null!;

    public string? Ip { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual ICollection<Physician> Physicians { get; } = new List<Physician>();

    public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();
}