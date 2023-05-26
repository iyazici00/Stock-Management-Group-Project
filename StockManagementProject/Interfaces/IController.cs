using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Interfaces
{
    public interface IController<T>
    {
        void Add();
        T SetValue();
        void Delete();
        void Update();
        T Get();
        void GetAll();
        void Menu();
    }
}
