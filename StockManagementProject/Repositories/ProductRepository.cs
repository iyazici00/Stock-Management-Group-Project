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
    internal class ProductRepository : IRepository<Product>
    {
        DataContext db = new DataContext();
        public bool Add(Product entity)
        {
            bool result = false;
            if (entity != null)
            {
                db.Product.Add(entity);
                db.SaveChanges();
                result = true;

            }
            return result;
        }

        public bool Delete(int id)
        {
            Product product = db.Product.Find(id);
            bool result = false;
            if (product != null)
            {
                db.Product.Remove(product);
                db.SaveChanges();
                result = true;

            }
            return result;
        }

        public List<Product> GetAll()
        {
            return db.Product.ToList();
        }

        public Product GetById(int id)
        {
            return db.Product.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(Product entity)
        {
            var product = db.Product.FirstOrDefault(p => p.Id == entity.Id);
            bool result = false;
            if (product != null)
            {
                product.Name = String.IsNullOrWhiteSpace(entity.Name) ? product.Name : entity.Name;
                product.IsStatus = entity.IsStatus;
                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
