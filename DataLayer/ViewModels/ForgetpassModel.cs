using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels;

public partial class forgetpassmodel
{
    [Required]
    public string
        email
    { get; set; }
}