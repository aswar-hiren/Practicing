using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class SendLinkViewModel
    {
        [Required]
        public string firstname {  get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string mobile { get; set; }
        [Required]
        public string email { get; set; }

    }
}
