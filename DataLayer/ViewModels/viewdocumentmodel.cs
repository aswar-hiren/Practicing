using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
namespace DataLayer.ViewModels;

public partial class viewdocumentmodel
{
    public IFormFile
         Photo
    { get; set; }

    public List<Requestwisefile> requestwisefiles { get; set; }
    public int type { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int? reqid { get; set; }

    public int? reqclientid {  get; set; }
    public string Uploder { get; set; }
    [Required]
    public List<int> allfile { get; set; }

}
