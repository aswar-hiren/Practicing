
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class AccessPagevm
    {
        public List<Role> roles { get; set; }

        public List<Menu> MenuList { get; set; }

        public string? RoleName { get; set; }

        public int? accountType { get; set; }

        public List<int?> menuItems { get; set; }

        public int? RoleId { get; set; }
    }
}