using StockManagementProject.Controllers;
using StockManagementProject.DataAccessLayer;
using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace StockManagementProject.Repositories
{
    internal class WarehouseRepository : IRepository<Warehouse>
    {
        DataContext db = new DataContext();
        WarehouseProductStockRepository whstrepo = new WarehouseProductStockRepository();
        public bool Add(Warehouse entity) // her depo eklendiği zaman warehouseproductstocka göndereceğiz
        {
            bool result = false;
            if (entity != null)
            {
                db.Warehouse.Add(entity); //depo ekliyor
                whstrepo.Add(entity); // Eklenen depo için WarehouseProductStocks tablosuna tüm ürünlerden 0 adet ekliyor  
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool Delete(int id)
        {
            Warehouse warehouse = db.Warehouse.Find(id);
            bool result = false;
            if (warehouse != null)
            {
                db.Warehouse.Remove(warehouse);
                whstrepo.Delete(warehouse);
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        public List<Warehouse> GetAll()
        {
            List<Warehouse> warehouses = db.Warehouse.ToList();
            return warehouses;
        }

        public Warehouse GetById(int id)
        {
            Warehouse warehouse = db.Warehouse.FirstOrDefault(x => x.Id == id );
            return warehouse;
        }

        public bool Update(Warehouse entity)
        {
            Warehouse warehouse = db.Warehouse.FirstOrDefault(x=>x.Id == entity.Id); ;
            bool result = false;

            if (warehouse != null)
            {
                warehouse.Name = String.IsNullOrWhiteSpace(entity.Name)?warehouse.Name : entity.Name;
                warehouse.District = String.IsNullOrWhiteSpace(entity.District) ? warehouse.District : entity.District; ;
                warehouse.ManagerId = (entity.ManagerId>0) ? entity.ManagerId  : warehouse.ManagerId;
                warehouse.IsStatus = entity.IsStatus;

                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
