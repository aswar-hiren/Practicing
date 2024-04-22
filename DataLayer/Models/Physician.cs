using System;
using System.Collections;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Physician
{
    public int Physicianid { get; set; }

    public string? Aspnetuserid { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? Medicallicense { get; set; }

    public string? Photo { get; set; }

    public string? Adminnotes { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public int? Regionid { get; set; }

    public string? Zip { get; set; }

    public string? Altphone { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createddate { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public short? Status { get; set; }

    public string? Businessname { get; set; }

    public string? Businesswebsite { get; set; }

    public BitArray? Isdeleted { get; set; }

    public int? Roleid { get; set; }

    public string? Npinumber { get; set; }

    public BitArray? Iscredentialdoc { get; set; }

    public BitArray? Istokengenerate { get; set; }

    public string? Syncemailaddress { get; set; }

    public bool? Isagreementdoc { get; set; }

    public bool? Isbackgrounddoc { get; set; }

    public bool? Istrainingdoc { get; set; }

    public bool? Islicensedoc { get; set; }

    public bool? Isnondisclosuredoc { get; set; }

    public string? Signature { get; set; }

    public virtual Aspnetuser? Aspnetuser { get; set; }

    public virtual Aspnetuser? CreatedbyNavigation { get; set; }

    public virtual Aspnetuser? ModifiedbyNavigation { get; set; }

    public virtual Physicianlocation? Physicianlocation { get; set; }

    public virtual ICollection<Physiciannotification> Physiciannotifications { get; } = new List<Physiciannotification>();

    public virtual ICollection<Physicianregion> Physicianregions { get; } = new List<Physicianregion>();

    public virtual ICollection<Request> Requests { get; } = new List<Request>();

    public virtual ICollection<Requeststatuslog> RequeststatuslogPhysicians { get; } = new List<Requeststatuslog>();

    public virtual ICollection<Requeststatuslog> RequeststatuslogTranstophysicians { get; } = new List<Requeststatuslog>();

    public virtual ICollection<Requestwisefile> Requestwisefiles { get; } = new List<Requestwisefile>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Shift> Shifts { get; } = new List<Shift>();
}
