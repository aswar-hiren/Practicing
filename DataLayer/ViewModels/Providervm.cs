using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
   public class Providervm
    {
        public List<Region> regions {  get; set; }

        public List<Physiciannotification> physicians { get; set; }
      
    }
}
