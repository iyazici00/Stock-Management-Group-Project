using StockManagementProject.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Models
{
    internal class User:CommonProperty
    {
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<Shipment> Shipment { get; set; }

    }
}
