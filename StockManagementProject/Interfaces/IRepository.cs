using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Interfaces
{
    public interface IRepository<T>
    {
        bool Add(T entity);
        bool Delete(int id);
        bool Update(T entity);
        T GetById(int id);
        List<T> GetAll();
    }
}
