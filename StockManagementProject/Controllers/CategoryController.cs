using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManagementProject.Controllers
{
    internal class CategoryController : IController<Category>
    {

        CategoryRepository repository = new CategoryRepository();
        ProductRepository productRepository = new ProductRepository();

        public void Add()
        {
            Console.Clear();
            Console.WriteLine(repository.Add(SetValue()) ? "Ekleme Başarılı" : "Ekleme Başarısız");
            CheckForContinue();

        }

        public void Delete()
        {
            GetAll();

            Console.Write("Silinecek Kategori Id Giriniz: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var sorgu = (from product in productRepository.GetAll()
                         where product.CategoryId == id
                         select new
                         {
                             productIds = product.Id
                         }).ToList();

            if (id > 0 && sorgu.Count() == 0)
            {
                Console.WriteLine(repository.Delete(id) ? "Silme İşlemi Başarılı" : "Silme İşlemi Başarısız");
            }
            CheckForContinue() ;
        }

        public Category Get()
        {
            GetAll();
            Console.WriteLine("--------------");
            Console.WriteLine();
            Console.Write("Detayını Görmek İstediğiniz Kategori Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Category category = repository.GetById(id);
            Console.Clear();
            if (category != null)
            {
                Console.WriteLine("Kategori Detay");
                Console.WriteLine("--------------");
                Console.WriteLine("Id         :" + category.Id);
                Console.WriteLine("İsim       :" + category.Name);



                var sorgu = (from product in productRepository.GetAll()
                             where product.CategoryId == id
                             select new
                             {
                                 productNames = product.Name
                             });
                int i = 0;

                if (sorgu.Count() > 0)
                {
                    Console.WriteLine("\nKategori Ürünleri");
                    Console.WriteLine("-------------------");
                    foreach (var product in sorgu)
                    {
                        i++;
                        Console.WriteLine(i + ". " + product.productNames);
                    }
                }
                else
                {
                    Console.WriteLine("Bu Kategoriye Ait Ürün Yok.");
                }



            }
            else
            {
                Console.WriteLine("Kategori Bulunamadı!");
            }
            return category;
        }

        public bool GetAll()
        {
            Console.WriteLine("Kategori Listesi");
            if (repository.GetAll().Count() > 0)
            {
                foreach (var category in repository.GetAll())
                {
                    Console.WriteLine("--------------");
                    Console.WriteLine("Id         :" + category.Id);
                    Console.WriteLine("İsim       :" + category.Name);
                }
                return true;
            }
            else
            {
                Console.WriteLine("Kategori Listesi Boş");
                return false;

            }

        }

        public void Menu()
        {
            bool status = true;
            while (status)
            {
                Console.Clear();

                Console.WriteLine("    İşlem Seçiniz    ");
                Console.WriteLine("---------------------");
                Console.WriteLine("Kategori Ekle     (1)");
                Console.WriteLine("Kategori Detayı   (2)");
                Console.WriteLine("Kategori Sil      (3)");
                Console.WriteLine("Kategori Güncelle (4)");
                Console.WriteLine("Kategori Listesi  (5)");
                Console.WriteLine("Üst Menü          (0)");
                Console.Write("Seçiminiz: ");
                int select = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (select)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Get();
                        CheckForContinue();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        Update();
                        break;
                    case 5:
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

        public Category SetValue()
        {
            Category category = new Category();

            Console.Write("Kategori İsmi: ");
            category.Name = Console.ReadLine();

            return category;
        }

        public void Update()
        {

            Category category = Get();

            if (category != null)
            {
                Category setCategory = SetValue();
                if (setCategory != null)
                {
                    setCategory.Id = category.Id;
                    category = setCategory;
                    repository.Update(category);
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

        void IController<Category>.GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
