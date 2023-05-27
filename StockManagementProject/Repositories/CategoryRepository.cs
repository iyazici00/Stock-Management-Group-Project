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
    internal class CategoryRepository : IRepository<Category>
    {
        DataContext db = new DataContext();
        public bool Add(Category entity)
        {
            bool result = false;
            if (entity != null)
            {
                db.Category.Add(entity);
                db.SaveChanges();
                result = true;

            }
            return result;
        }

        public bool Delete(int id)
        {
            Category category = db.Category.Find(id);
            bool result = false;
            if (category != null)
            {
                db.Category.Remove(category);
                db.SaveChanges();
                result = true;

            }
            return result;


        }

        public List<Category> GetAll()
        {
            return db.Category.ToList();
        }

        public Category GetById(int id)
        {
            return db.Category.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(Category entity)
        {
            var category = db.Category.FirstOrDefault(p => p.Id == entity.Id);
            bool result = false;
            if (category != null)
            {
                category.Name = String.IsNullOrWhiteSpace(entity.Name) ? category.Name : entity.Name; //boş yada boşluğa bastıysa kontrol
                category.IsStatus = entity.IsStatus;

                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
