using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockManagementProject.Controllers
{
    internal class ProductController : IController<Product>
    {
        ProductRepository repository = new ProductRepository();

        public void Add()
        {
            Console.Clear();
            Console.WriteLine(repository.Add(SetValue()) ? "Ekleme Başarılı" : "Ekleme Başarısız");
            Thread.Sleep(2000);

        }

        public void Delete()
        {
            GetAll();

            Console.Write("Silinecek Ürün Id Giriniz: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(repository.Delete(id) ? "Silme İşlemi Başarılı" : " Silme İşlemi Başarısız");


        }
        CategoryRepository repositoryCategory = new CategoryRepository();
        public Product Get()
        {
            GetAll();

            Console.Write("Detayını Görmek İstediğiniz Ürün Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Product product = repository.GetById(id);

            Category category = repositoryCategory.GetById(product.CategoryId);

            if (product != null)
            {
                Console.WriteLine("Ürün Detayı");
                Console.WriteLine("--------------");
                Console.WriteLine("Id         :" + product.Id);
                Console.WriteLine("İsim       :" + product.Name);
                Console.WriteLine("Kategori   :" + category.Name);
                Console.WriteLine("Durum      :" + (product.IsStatus ? "Aktif" : "Pasif"));
            }
            else
            {
                Console.WriteLine("Ürün Bulunamadı!");
            }
            return product;
        }

        public void GetAll()
        {
            Console.WriteLine("Ürün Listesi");
            if (repository.GetAll().Count() > 0)
            {
                foreach (var product in repository.GetAll())
                {
                    Console.WriteLine("--------------");
                    Console.WriteLine("Id          :" + product.Id);
                    Console.WriteLine("İsim        :" + product.Name);
                    Console.WriteLine("Durum      :" + (product.IsStatus ? "Aktif" : "Pasif"));
                }
            }
            else
            {
                Console.WriteLine("Ürün Listesi Boş");
            }
        }

        public void Menu()
        {
            bool status = true;
            while (status)
            {
                Console.Clear();

                Console.WriteLine("  İşlem Seçiniz   ");
                Console.WriteLine("------------------");
                Console.WriteLine("Ürün Ekle     (1)");
                Console.WriteLine("Ürün Sil      (2)");
                Console.WriteLine("Ürün Güncelle (3)");
                Console.WriteLine("Ürün Listesi  (4)");
                Console.WriteLine("Üst Menü      (0)");
                Console.Write("Seçiminiz: ");
                int select = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (select)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Delete();
                        break;
                    case 3:
                        Update();
                        break;
                    case 4:
                        GetAll();
                        CheckForContinue();

                        break;
                    case 0:

                        status = !status;
                        break;
                    default:
                        Console.WriteLine("Tanımsız işlem Tekrar Deneyiniz");
                        break;
                }

            }
        }
        public Product SetValue()
        {
            Product product = new Product();

            Console.Write("Ürün İsmi: ");
            product.Name = Console.ReadLine();
            Console.WriteLine("Ürün Durumu Aktif(A) Pasif(P):");
            product.IsStatus = Console.ReadLine().Substring(0, 1).ToLower() == "a" ? true : false;
            return product;
        }

        public void Update()
        {
            Console.Clear ();
            Product product = Get();
            Console.WriteLine();
            Console.WriteLine("-------------");
            if (product != null)
            {
                Product setProduct = SetValue();
                if (setProduct != null)
                {
                    setProduct.Id = product.Id;
                    product = setProduct;
                    repository.Update(product);
                }
            }
        }

        public void CheckForContinue()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine();
            Console.WriteLine("Devam Etmek İçin Herhangi Bir Tuşa Basınız");
            Console.ReadKey();
        }
    }
}
