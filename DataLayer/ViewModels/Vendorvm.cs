using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class Vendorvm
    {
      public  List<Healthprofessional> healthprofessionalList { get; set; }
      public List<Healthprofessionaltype> hptype {  get; set; }
    }
}
