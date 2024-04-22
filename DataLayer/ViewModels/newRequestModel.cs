using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class newRequestModel
    {
        [Required]
        public string firstName {  get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string birthDate { get; set; }
        [Required]
        public string street { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string state { get; set; }
        [Required]
        public string zipCode { get; set; }
   
        public int room {  get; set; }
        public string notes { get; set; }
    }
}
