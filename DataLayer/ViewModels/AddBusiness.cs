using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class AddBusiness
    {
        public List<Healthprofessionaltype> hptype { get; set; }
        [Required]
        public string bname { get; set; }
        [Required]
        public int? proffesion {  get; set; }
        [Required]
        public string faxno { get; set; }
        [Required]
        public string contact { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string bcontact { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string pincode { get; set; }
        [Required]
        public string state { get; set; }
        [Required]

        public string street { get; set; }
        public int vendorid {  get; set; }
    }
}
