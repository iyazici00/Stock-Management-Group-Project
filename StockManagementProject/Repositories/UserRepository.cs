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
    internal class UserRepository : IRepository<User>
    {
        DataContext db = new DataContext();

        public bool Add(User entity)
        {
            var exists = db.User.Any(x => x.Email == entity.Email);
            if (exists == false)
            {
                db.User.Add(entity);
                db.SaveChanges();
                return true;
            }
            return false;

        }

        public bool Delete(int id)
        {
            var user = db.User.Find(id);
            if (user != null)
            {
                try
                {
                    db.User.Remove(user);
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

        public List<User> GetAll()
        {
            return db.User.ToList();
        }

        public User GetById(int id)
        {
            var user = db.User.Find(id);
            return user;
        }

        public User GetByLogin(string mail, string password)
        {
            User user=db.User.Where(x=>x.Email==mail &&  x.Password==password).FirstOrDefault();
            return user;
        }
        public bool Update(User entity)
        {
            var user = db.User.Find(entity.Id);
            if (user != null && !String.IsNullOrWhiteSpace(entity.Name) && !String.IsNullOrWhiteSpace(entity.Surname)&& !String.IsNullOrWhiteSpace(entity.Email)&& !String.IsNullOrWhiteSpace(entity.Password)&& entity.RoleId>0)
            {
                user.Name = entity.Name;
                user.Surname = entity.Surname;
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.RoleId = entity.RoleId;

                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
