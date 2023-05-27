using StockManagementProject.DataAccessLayer;
using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Repositories
{
    internal class RoleRepository : IRepository<Role>
    {
        DataContext db = new DataContext();
        public bool Add(Role entity)
        {
            var exists=db.Role.Any(x=> x.Name == entity.Name);
            if (exists==false)
            {
                db.Role.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            var role = db.Role.Find(id);
            if (role != null)
            {
                try
                {
                    db.Role.Remove(role);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    
                }
            }
            return false;
        }

        public List<Role> GetAll()
        {
            return db.Role.ToList();
        }

        public Role GetById(int id)
        {
            var role = db.Role.Find(id);
            return role;
        }

        public bool Update(Role entity)
        {
            var role =db.Role.Find(entity.Id);
            if(role!=null && !String.IsNullOrWhiteSpace(entity.Name))
            {
                role.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
