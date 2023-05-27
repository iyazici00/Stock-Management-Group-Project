using StockManagementProject.DataAccessLayer;
using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Controllers
{
    internal class WarehouseProductStockController : IController<WarehouseProductStock>
    {
        WarehouseProductStockRepository productStockRepository = new WarehouseProductStockRepository();

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            if (productStockRepository.Delete(id))
            {
                Console.Clear();
                Console.WriteLine("Silme İşlemi Başarılı");
                Console.WriteLine();
                GetAll();
                Console.WriteLine("Ana Menü İçin Bir Tuşa Basın");
            }
            else
            {
                Console.WriteLine("Silme İşlemi Başarısız");
            }
            Console.ReadKey();

        }

        public void Delete(Product product)
        {
            if (productStockRepository.Delete(product))
            {
                Console.Clear();
                Console.WriteLine("Silme İşlemi Başarılı");
                Console.WriteLine();
                GetAll();
                Console.WriteLine("Ana Menü İçin Bir Tuşa Basın");
            }
            else
            {
                Console.WriteLine("Silme İşlemi Başarısız");
            }
            Console.ReadKey();
           
        }

        public void Delete(Warehouse warehouse)
        {
            if (productStockRepository.Delete(warehouse))
            {
                Console.Clear();
                Console.WriteLine("Silme İşlemi Başarılı");
                Console.WriteLine();
                GetAll();
                Console.WriteLine("Ana Menü İçin Bir Tuşa Basın");
            }
            else
            {
                Console.WriteLine("Silme İşlemi Başarısız");
            }
            Console.ReadKey();
          
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public WarehouseProductStock Get() 
        {
            throw new NotImplementedException();
        }

        public void GetAll()
        {
            //Console.WriteLine("Depolardaki Ürün-Stok Yönetimi Listesi");
            //Console.WriteLine();
            //if (productStockRepository.GetAll().Count > 0)
            //{

            //    foreach (var islem in productStockRepository.GetAll())
            //    {

            //        WarehouseProductStock warehouseProductStock = productStockRepository.GetById(islem.Id);
            //        Warehouse depoİsmi = productStockRepository.GetAll().Where(x => x.Id == warehouseProductStock.WarehouseId).FirstOrDefault();

            //        Console.WriteLine("İşlem Id:     " + islem.Id);
            //        Console.WriteLine("Depo Id:      " + islem.WarehouseId);
            //        Console.WriteLine("Ürün Id:      " + islem.ProductId);
            //        Console.WriteLine("Ürün Miktarı: " + islem.ProductQuantity);
              
            //        Console.WriteLine();


            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Depo Listesi Boş");
            //}
        }

        public void Menu()
        {
            throw new NotImplementedException();
        }

        public WarehouseProductStock SetValue()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
