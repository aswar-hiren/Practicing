using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class myProfilevm
    {
        public readonly string AspnetUserId;
        public readonly object Firstname;

        public string username { get; set; }
        [Required]
        public string password { get; set; }
   
        public string aspuserId { get; set; }
        public int status { get; set; }
        public int role {  get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string cemail { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string adress1 { get; set; }
        [Required]
        public string adress2 { get; set;}
        [Required]
        public string city { get; set; }
        public string state { get; set; }
        [Required]
        public string zip {  get; set; }
        [Required]
        public string phoneno {  get; set; }
        [Required]
        public Admin admin { get; set; }
        public int region {  get; set; }
        public List<Region> regions { get; set; }
       
      public List<Adminregion> adminregion { get; set; }
        [Required]
        public List<int> AllCheck {  get; set; }

    }
}
