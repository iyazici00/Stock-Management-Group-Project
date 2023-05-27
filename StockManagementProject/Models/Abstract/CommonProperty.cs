using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Models.Abstract
{
    abstract class CommonProperty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStatus { get; set; }=true;
    }
}
