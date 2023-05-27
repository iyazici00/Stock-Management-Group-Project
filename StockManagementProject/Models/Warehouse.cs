using StockManagementProject.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Models
{
    internal class Warehouse:CommonProperty
    {
        public string District { get; set; }
        public int ManagerId { get; set; }
        public User User { get; set; }
        public List<WarehouseProductStock> WarehouseProductStock { get; set; }
        public List<Shipment> ReceiverShipment { get; set; }
        public List<Shipment> ShipperShipment { get; set; }
    }
}
