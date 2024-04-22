using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class ConcludeCarevm
    {
        public IFormFile photo { get; set; }
        public int concludeId { get; set; }

        public int reqid { get; set; }
        public Encounter encounter { get; set; }
    }
}
