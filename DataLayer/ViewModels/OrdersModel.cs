using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class OrdersModel
    {
      public  List<Healthprofessionaltype> hptype {  get; set; }
      public  List<Healthprofessional> hp {  get; set; }

      public Healthprofessional Healthprofessional { get; set; }
      public int vendorid { get; set; }
        [Required]
      public string contact {  get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string faxnumber {  get; set; }
        [Required]
        public string details { get; set; }
        public int refill {  get; set; }
        public int reqid { get; set; }
    }
}
