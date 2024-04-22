using System;
using System.Collections;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Aspnetuser
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public string? Securitystamp { get; set; }

    public string? Email { get; set; }

    public BitArray? Emailconfirmed { get; set; }

    public BitArray? Phonenumberconfirmed { get; set; }

    public BitArray? Twofactorenabled { get; set; }

    public DateTime? Lockoutenddateutc { get; set; }

    public BitArray? Lockoutenabled { get; set; }

    public int? Accessfailedcount { get; set; }

    public string? Ip { get; set; }

    public string? Corepasswordhash { get; set; }

    public int? Hashversion { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Roleid { get; set; }

    public string? Phonenumber { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual ICollection<Business> BusinessCreatedbyNavigations { get; } = new List<Business>();

    public virtual ICollection<Business> BusinessModifiedbyNavigations { get; } = new List<Business>();

    public virtual ICollection<Physician> PhysicianAspnetusers { get; } = new List<Physician>();

    public virtual ICollection<Physician> PhysicianCreatedbyNavigations { get; } = new List<Physician>();

    public virtual ICollection<Physician> PhysicianModifiedbyNavigations { get; } = new List<Physician>();

    public virtual ICollection<Shiftdetail> Shiftdetails { get; } = new List<Shiftdetail>();

    public virtual ICollection<Shift> Shifts { get; } = new List<Shift>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
