using StockManagementProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockManagementProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RoleController roleController = new RoleController();
            UserController userController = new UserController();
            CategoryController categoryController = new CategoryController();
            ProductController productController = new ProductController();
            WarehouseController warehouseController = new WarehouseController();

            bool status = true;
            bool login = false;
            int userId = 0;
            string userRole = "Hata";
            char rolec = '.';
            while (status)
            {
                if(login==false)
                {
                    Console.WriteLine("Stock Yönetim Sistemine Hoşgeldiniz");
                    Console.WriteLine("Devam Etmek İçin Lütfen Giriş Yapınız");
                    userController.Login(out userId,out userRole);
                    if (userId != 0 && userRole!= "Hata")
                    {
                        login = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Hatalı Kullanıcı Girişi Yaptınız Lütfen Tekrar Deneyiniz");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                }
                else if (login==true)
                {
                    Console.Clear();
                    Console.WriteLine("Hoşgeldiniz");
                    Console.WriteLine();
                    Console.WriteLine("Lütfen Yapmak İstediğiniz İşlemi Seçiniz");
                    Console.WriteLine("----------------------------------------");
                    if (userRole == "Admin")
                    {
                        Console.WriteLine("1. Rol İşlemleri");
                        Console.WriteLine("2. Kullanıcı İşlemleri");
                        Console.WriteLine("3. Depo İşlemleri");
                        Console.WriteLine("4. Kategori İşlemleri");
                        Console.WriteLine("5. Ürün İşlemleri");
                        rolec = 'a';
                    }
                    else if(userRole == "Sevkiyatçı")
                    {
                        Console.WriteLine("1. Kullanıcı Bilgilerini Güncelle");
                        Console.WriteLine("2. Deponun Bilgilerini Görüntüle");
                        Console.WriteLine("3. Sevkiyat yap");
                        Console.WriteLine("4. Mevcut Depoya Yapılmış Sevkiyatları Görüntüle");
                        rolec = 's';
                    }
                    Console.WriteLine("0. Çıkış");

                    Console.Write("Seçim: ");
                    string select= Console.ReadLine().Substring(0,1);
                    string guide = select+rolec;
                    switch (guide)
                    {
                        case "1a": roleController.Menu(); break;
                        case "2a": userController.Menu(); break;
                        case "3a": warehouseController.Menu(); break;
                        case "4a": categoryController.Menu(); break;
                        case "5a": productController.Menu(); break;
                        case "1s": userController.Update(userId); break;
                        case "2s": warehouseController.Get(); break; //user id gönderip user id ye göre deponun gelmesi lazım!!
                        case "3s":  break; //shipment !!
                        case "4s": break; //shipment
                        default: Console.Clear(); Console.WriteLine("Hatalı Giriş Yaptınız Lütfen Tekrar Giriniz"); Thread.Sleep(1500); break;
                    }
                }


            }
        }
    }
}
