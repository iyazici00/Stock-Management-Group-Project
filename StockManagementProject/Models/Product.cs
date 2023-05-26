using StockManagementProject.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Models
{
    internal class Product:CommonProperty
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<WarehouseProductStock> WarehouseProductStock { get; set; }
        public List<Shipment> Shipment { get; set; }


    }
}
