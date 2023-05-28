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
    internal class ShipmentRepository : IRepository<Shipment>
    {
        DataContext db = new DataContext();

        public bool Add(Shipment entity)
        {

                db.Shipment.Add(entity);
                db.SaveChanges();
                return true;
  
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Shipment> GetAll()
        {
            return db.Shipment.ToList();
        }

        public Shipment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Shipment entity)
        {
            throw new NotImplementedException();
        }
    }
}
