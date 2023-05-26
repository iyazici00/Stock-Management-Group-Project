using StockManagementProject.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Models
{
    internal class Category:CommonProperty
    {

        
        public List<Product> Product { get; set; }
        
    }
}
