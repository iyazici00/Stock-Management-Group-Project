using StockManagementProject.DataAccessLayer;
using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManagementProject.Controllers
{
    internal class WarehouseController : IController<Warehouse>
    {
        WarehouseRepository repository = new WarehouseRepository();
        WarehouseProductStockRepository productStockRepository = new WarehouseProductStockRepository();
        ProductRepository productRepository = new ProductRepository();
        UserRepository userRepository = new UserRepository();


        public void Add()
        {
            Console.Clear();
            Console.WriteLine(repository.Add(SetValue()) ? "Ekleme Başarılı" : "Ekleme Başarısız");
            Console.WriteLine("Devam etmek için herhangi bir tuşa basınız");
            Console.ReadKey();
        }

        public void Delete()
        {
            GetAll();
            Console.Write("Silinecek Depo Id Giriniz: ");
            int id = Convert.ToInt32(Console.ReadLine());


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
            Console.WriteLine("-------------------");
            Console.WriteLine();
            Console.WriteLine("Devam Etmek İçin Herhangi Bir Tuşa Basınız");
            Console.ReadKey();
        }

        public Warehouse Get()
        {
            bool status = true;
            Warehouse warehouse = null;

            while (status)
            {
                Console.Clear();
                GetAll();

                Console.Write("Depo Id Giriniz: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                warehouse = repository.GetById(id);

                if (warehouse != null)
                {
                    User depoYonetici = userRepository.GetById(warehouse.ManagerId);
                    Console.WriteLine("Depo Detayları");
                    Console.WriteLine("-------------");
                    Console.WriteLine("Id              : " + warehouse.Id);
                    Console.WriteLine("Depo İsmi       : " + warehouse.Name);
                    Console.WriteLine("Deponun Semti   : " + warehouse.District);
                    if (depoYonetici.Id != 1) 
                    {
                        Console.WriteLine("Depo Yönetici Id: " + warehouse.ManagerId);
                        Console.WriteLine("Depo Yöneticisi : " + depoYonetici.Name); 
                    }
                    else
                    {
                        Console.WriteLine("Depo Yöneticisi : Yok");
                    }

                    Console.WriteLine();
                    Console.WriteLine();

                    WarehouseProducts(warehouse.Id);
                    status = false;
                }
                else
                {
                    Console.WriteLine("Depo Bulunamadı Devam Etmek İçin Bir Tuşa Basınız");
                    Console.ReadKey();
                }
            }

            return warehouse;
        }
        public Warehouse GetShp(int currentWarehouseId)
        {
            bool status = true;
            Warehouse warehouse = null;

            while (status)
            {
                Console.Clear();
                GetAll();

                Console.WriteLine("Çıkış için (0)");
                Console.Write("Sevkiyat Yapacağınız Depo Id Giriniz: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                warehouse = repository.GetById(id);
                if (id == 0)
                {
                    status = false;
                }
                else if (warehouse != null && warehouse.Id!=currentWarehouseId)
                {
                    return warehouse;
                }
                else
                {
                    Console.WriteLine("Depo Bulunamadı Tekrar Seçmek İçin Bir Tuşa Basınız");
                    Console.ReadKey();
                }
            }

            return warehouse;
        }
        public void Get(int id)
        {


            Console.WriteLine();

            Console.Clear();
            User depoYonetici = userRepository.GetById(id);
            Warehouse warehouse = repository.GetByManagerId(depoYonetici.Id);


            if (warehouse != null)
            {
                Console.WriteLine("Depo Detayları");
                Console.WriteLine("-------------");
                Console.WriteLine("Id              : " + warehouse.Id);
                Console.WriteLine("Depo İsmi       : " + warehouse.Name);
                Console.WriteLine("Deponun Semti   : " + warehouse.District);
                Console.WriteLine("Depo Yönetici Id: " + warehouse.ManagerId);
                Console.WriteLine("Depo Yöneticisi : " + depoYonetici.Name);
                Console.WriteLine();
                Console.WriteLine();

                WarehouseProducts(warehouse.Id);


            }
            else
            {
                Console.WriteLine("Depo Bulunamadı");
            }

        }

        public void GetAll()
        {
            Console.WriteLine("Depoların Listesi");
            Console.WriteLine();
            if (repository.GetAll().Count > 0)
            {

                foreach (var depo in repository.GetAll())
                {

                    Warehouse warehouse = repository.GetById(depo.Id);
                    User depoYonetici = userRepository.GetById(warehouse.ManagerId);

                    Console.WriteLine("Depo Id:           " + depo.Id);
                    Console.WriteLine("Depo İsmi:         " + depo.Name);
                    Console.WriteLine("Depo Semti:        " + depo.District);
                    Console.WriteLine("Depo Yönetici Id:  " + depo.ManagerId);
                    Console.WriteLine("Depo Yönetici Adı: " + depoYonetici.Name);
                    Console.WriteLine();


                }
            }
            else
            {
                Console.WriteLine("Depo Listesi Boş");
            }

        }

        public void Menu()
        {
            bool status = true;
            while (status)
            {
                Console.Clear();

                Console.WriteLine("--Depo İşlemi Seçiniz--");
                Console.WriteLine("-----------------------");
                Console.WriteLine("Depo Ekle           (1)");
                Console.WriteLine("Depo Detayları      (2)");
                Console.WriteLine("Depo Sil            (3)");
                Console.WriteLine("Depo Güncelle       (4)");
                Console.WriteLine("Depo Stok Güncelle  (5)");
                Console.WriteLine("Depo Listesi        (6)");
                Console.WriteLine("Üst Menü            (0)");
                Console.WriteLine();
                Console.Write("İşlem Seçiniz: ");
                int select = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (select)
                {
                    case 1: Add(); break;
                    case 2: Get(); Console.ReadKey(); break;
                    case 3: Delete(); break;
                    case 4: Update(); break;
                    case 5: ChangeQuantity(); break;
                    case 6: GetAll(); Console.WriteLine("Devam etmek için bir tuşa basınız"); Console.ReadKey(); break;
                    case 0: status = !status; break;
                    default: Console.WriteLine("Tanımsız İşlem Tekrar Deneyiniz"); break;
                }
            }

        }

        public Warehouse SetValue()
        {
            Warehouse warehouse = new Warehouse();

            Console.Write("Depo İsmi:  ");
            warehouse.Name = Console.ReadLine();

            Console.Write("Semt İsmi:  ");
            warehouse.District = Console.ReadLine();

            UserController userList = new UserController();
            userList.GetAll();
            Console.WriteLine("--------------------");

            bool status = true;
            while (status)
            {


                Console.Write("Depo Yönetici Id: ");
                string managerid = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(managerid) && int.TryParse(managerid, out int managerIdInt))
                {

                    Warehouse depoVarMı = repository.GetByManagerId(managerIdInt);

                    User managerisIsNull = userRepository.GetById(managerIdInt);

                    if (depoVarMı != null)
                    {

                        Console.WriteLine("Bu Kullanıcının Zaten Bir Depo İle ilişkisi Var Tekrar Deneyin");
                    }
                    else if (managerisIsNull != null)
                    {
                        warehouse.ManagerId = managerIdInt;
                        status = false;
                    }
                    else
                    {
                        Console.WriteLine("Geçerli bir Yönetici Id Giriniz");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Geçerli bir Yönetici Id Giriniz");
                    Console.WriteLine();
                }
            }





            return warehouse;
        }

        public void Update()
        {
            DataContext db = new DataContext();
            GetAll();
            Console.Write("Düzenlemek istediğiniz deponun Id sini girin: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Warehouse warehouse = db.Warehouse.FirstOrDefault(x => x.Id == id);

            if (warehouse != null)
            {
                Warehouse setWarehouse = SetValue();
                if (setWarehouse != null)
                {
                    setWarehouse.Id = warehouse.Id;
                    warehouse = setWarehouse;
                    repository.Update(warehouse);
                    Console.WriteLine("Güncelleme başarılı");
                }
                else
                {
                    Console.WriteLine("Güncelleme başarısız.");
                }

            }
        }

        public void WarehouseProducts(int warehouseId)
        {
            Console.WriteLine("Depodaki ürünler: ");
            Console.WriteLine();
            Console.WriteLine("Ürün Id \t Ürün İsmi \t Ürün Miktarı");

            var sorgu = from w in repository.GetAll()
                        join wp in productStockRepository.GetAll() on w.Id equals wp.WarehouseId
                        join p in productRepository.GetAll() on wp.ProductId equals p.Id
                        where w.Id == warehouseId
                        select new { p.Name, wp.ProductId, wp.ProductQuantity };


            foreach (var item in sorgu)
            {

                Console.WriteLine($"{item.ProductId} \t\t {item.Name} \t\t {item.ProductQuantity}");
            }
        }

        public void ChangeQuantity()
        {
            Warehouse warehouse = Get();

            if (warehouse != null)
            {
                Console.Write("Stoğunu değiştirmek istediğiniz ürünün Id'sini girin: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Product product = productRepository.GetById(id);

                WarehouseProductStock warehouseProductStock = productStockRepository.GetAll().FirstOrDefault(x => x.WarehouseId == warehouse.Id && x.ProductId == product.Id);

                if (product != null)
                {
                    Console.Write("Stoğu artırmak için (+) azaltmak için (-) ");
                    string islem = Console.ReadLine().Substring(0, 1);

                    Console.Write("Miktarı giriniz: ");
                    int miktar = Convert.ToInt32(Console.ReadLine());


                    switch (islem)
                    {
                        case "+":
                            warehouseProductStock.ProductQuantity += miktar;
                            productStockRepository.Update(warehouseProductStock);
                            Console.WriteLine("Stok arttırma başarılı");
                            break;
                        case "-":
                            int temp = warehouseProductStock.ProductQuantity - miktar;

                            if (temp < 0)
                            {
                                Console.WriteLine("Stok 0 ın altına düşemez");
                            }
                            else
                            {
                                warehouseProductStock.ProductQuantity = temp;
                                productStockRepository.Update(warehouseProductStock);
                            }

                            break;
                        default:
                            Console.WriteLine("Yanlış giriş yaptınız");
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Böyle bir ürün yok");

                }
                Console.WriteLine("Devam etmek için bir tuşa basınız.");
                Console.ReadKey();

            }
            else
            {
                Console.ReadKey();
            }

        }
    }
}
