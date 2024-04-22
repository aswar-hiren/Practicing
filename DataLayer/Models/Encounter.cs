using System;
using System.Collections;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Encounter
{
    public int EncounterId { get; set; }

    public string? Firstname { get; set; }

    public string? Location { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? HistoryOfInjury { get; set; }

    public string? MedicalHistory { get; set; }

    public string? Medication { get; set; }

    public string? Allergies { get; set; }

    public int? Temp { get; set; }

    public int? Hr { get; set; }

    public int? Rr { get; set; }

    public int? BpS { get; set; }

    public int? BpD { get; set; }

    public int? O2 { get; set; }

    public string? Pain { get; set; }

    public string? Heent { get; set; }

    public int? Cv { get; set; }

    public int? Chest { get; set; }

    public int? Abd { get; set; }

    public string? Extr { get; set; }

    public string? Skin { get; set; }

    public string? Neuro { get; set; }

    public string? Other { get; set; }

    public string? Diagonosis { get; set; }

    public string? TreatmentPlan { get; set; }

    public string? MedicationDispense { get; set; }

    public string? Procedure { get; set; }

    public string? FollowUp { get; set; }

    public int? Requestid { get; set; }

    public BitArray? IsFinalized { get; set; }

    public BitArray? Isreport { get; set; }

    public string? Report { get; set; }

    public virtual Request? Request { get; set; }
}
