using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace DataLayer.ViewModels;

public partial class PatientLoginView
{

    [Required]
    public string? passwordhash { get; set; }
    [Required]
    public string? email { get; set; }

  
    public int role { get; set; }
    
    public string? userid { get; set; }
}


