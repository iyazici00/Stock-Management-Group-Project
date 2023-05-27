using StockManagementProject.DataAccessLayer;
using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Repositories
{
    internal class WarehouseProductStockRepository : IRepository<WarehouseProductStock>
    {
        DataContext db = new DataContext();

        public bool Add(Product product)
        {
            bool result = false;

            if (product != null)
            {
                List<Warehouse> depolar = db.Warehouse.ToList(); 

                foreach (var depo in depolar)
                {
                    WarehouseProductStock warehouseProductStock = new WarehouseProductStock();
                    warehouseProductStock.WarehouseId = depo.Id;
                    warehouseProductStock.ProductId = product.Id;
                    warehouseProductStock.ProductQuantity = 0;

                    db.WarehouseProductStock.Add(warehouseProductStock);
                }

                db.SaveChanges();
                result = true;
            }

            return result;
        }
        public bool Add(Warehouse warehouse)
        {
            bool result = false;

            if (warehouse != null)
            {
                List<Product> ürünler = db.Product.ToList(); 

                foreach (var ürün in ürünler)
                {
                    WarehouseProductStock warehouseProductStock = new WarehouseProductStock();
                    warehouseProductStock.WarehouseId = warehouse.Id;
                    warehouseProductStock.ProductId = ürün.Id;
                    warehouseProductStock.ProductQuantity = 0;

                    db.WarehouseProductStock.Add(warehouseProductStock);
                }

                db.SaveChanges();
                result = true;
            }

            return result;
        }


        public bool Add(WarehouseProductStock entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            WarehouseProductStock warehouseProductStock = db.WarehouseProductStock.Find(id);
            bool result = false;
            if (warehouseProductStock != null)
            {
                db.WarehouseProductStock.Remove(warehouseProductStock);
                db.SaveChanges();
                result = true;
            }
            return result;
        }
        public bool Delete(Product product) //overload yapıldı
        {
            List<WarehouseProductStock> warehouseProductStock = db.WarehouseProductStock
                .Where(p => p.ProductId == product.Id)
                .ToList();

            if (warehouseProductStock.Count > 0)
            {
                db.WarehouseProductStock.RemoveRange(warehouseProductStock);
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(Warehouse warehouse) //overload yapıldı
        {
            List<WarehouseProductStock> warehouseProductStock = db.WarehouseProductStock
                .Where(p => p.WarehouseId == warehouse.Id)
                .ToList();

            if (warehouseProductStock.Count > 0)
            {
                db.WarehouseProductStock.RemoveRange(warehouseProductStock);
                db.SaveChanges();
                return true;
            }

            return false;
        }


        public List<WarehouseProductStock> GetAll()
        {
            List<WarehouseProductStock> warehouseProductStocks = db.WarehouseProductStock.ToList();
            return warehouseProductStocks;
        }

        public WarehouseProductStock GetById(int id)
        {
            WarehouseProductStock warehouseProductStock = db.WarehouseProductStock.FirstOrDefault(x => x.Id == id);
            return warehouseProductStock;
        }

        public bool Update(WarehouseProductStock entity)
        {
            WarehouseProductStock warehouseProductStock = db.WarehouseProductStock.FirstOrDefault(x => x.Id == entity.Id);
            bool result = false;

            if (warehouseProductStock != null)
            {
                warehouseProductStock.WarehouseId = entity.WarehouseId;
                warehouseProductStock.ProductId =entity.ProductId; ;
                warehouseProductStock.ProductQuantity = entity.ProductQuantity;
               

                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
