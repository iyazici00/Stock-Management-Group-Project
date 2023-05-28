using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManagementProject.Controllers
{
    internal class ProductController : IController<Product>
    {
        ProductRepository repository = new ProductRepository();
        CategoryRepository repositoryCategory = new CategoryRepository();
        public void Add()
        {
            Console.Clear();
            Console.WriteLine(repository.Add(SetValue()) ? "Ekleme Başarılı" : "Ekleme Başarısız");
            CheckForContinue();

        }

        public void Delete()
        {
            GetAll();

            Console.Write("Silinecek Ürün Id Giriniz: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            GetAll();

            if (repository.Delete(id))
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

            CheckForContinue();


        }

        public Product Get()
        {
            GetAll();

            Console.Write("\nİstediğiniz Ürün Id: ");
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
                    Category category = repositoryCategory.GetById(product.CategoryId);

                    Console.WriteLine("--------------");
                    Console.WriteLine("Id          :" + product.Id);
                    Console.WriteLine("İsim        :" + product.Name);
                    Console.WriteLine("Kategori   :" + category.Name);

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



            bool status = true;
            while (status)
            {
                CategoryController categoryList = new CategoryController();

                if (categoryList.GetAll() == false) { product = null; Console.WriteLine("Devam Etmek İçin Herhangi Bir Tuşa Basınız"); Console.ReadKey(); Console.Clear(); ; break; }
                Console.WriteLine("--------------------");
                Console.Write("Kategori Id: ");
                string categoryId = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(categoryId) && int.TryParse(categoryId, out int categoryIdInt))
                {
                    Category categoryIsNull = repositoryCategory.GetAll().FirstOrDefault(x => x.Id == categoryIdInt);

                    if (categoryIsNull != null)
                    {
                        product.CategoryId = categoryIdInt;
                        Console.Write("Ürün İsmi: ");
                        product.Name = Console.ReadLine();

                        status = false;
                    }
                    else
                    {
                        Console.WriteLine("Geçerli Bir Kategori Id Giriniz");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Geçerli Bir Kategori Id Giriniz");
                    Console.WriteLine();
                }
            }

            return product;
        }

        public void Update()
        {
            Console.Clear();
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
