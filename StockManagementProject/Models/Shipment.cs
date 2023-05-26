using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Models
{
    internal class Shipment
    {
        [Key]
        public int Id { get; set; }
        public int ReceiverWarehouseId { get; set; }
        public Warehouse ReceiverWarehouse { get; set; }
        public int ShipperWarehouseId { get; set; }
        public Warehouse ShipperWarehouse { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime ShipmentDate { get; set; } = DateTime.Now;
        public int ShipperManagerId { get; set; }
        public User User { get; set; }
    }
}
