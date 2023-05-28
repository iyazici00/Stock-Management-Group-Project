using StockManagementProject.Controllers;
using StockManagementProject.Models;
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
        // boş role ıd=1   boş user ıd= 1;
        //default admin mail: şifre:
        //default sevkiyatçı mail: şifre:

        static void Main(string[] args)
        {
            GetFirstPage();   
        }

        public static void GetFirstPage()
        {
            bool status = true;
            while (status)
            {
                Console.Clear();
                Console.WriteLine("Stock Yönetim Sistemine Hoşgeldiniz");
                Console.WriteLine("1. Giriş Yap");
                Console.WriteLine("2. Kayıt Ol");
                Console.WriteLine("0. Programı Kapat");
                Console.Write("Seçim: ");
                string selectFirst = Console.ReadLine().Substring(0,1);
                if (!string.IsNullOrWhiteSpace(selectFirst) && int.TryParse(selectFirst, out int numberSelectFirst) == true)
                {
                    switch (numberSelectFirst)
                    {
                        case 1: GetLoginPage(); break;
                        case 2: GetRegisterPage(); break;
                        case 0: status = ProgramExit(); break;
                        default: GetSwitchErrorMessage(); break;
                    }
                }
                else
                {
                    GetSwitchErrorMessage();
                }
            }
        }
        public static void GetLoginPage()
        {
            Console.Clear();
            CategoryController categoryController = new CategoryController();
            ProductController productController = new ProductController();
            WarehouseController warehouseController = new WarehouseController();
            RoleController roleController = new RoleController();
            UserController userController = new UserController();

            bool status = true;
            bool login = false;
            int userId = 0;
            string userRole = "Hata";
            char rolec = '.';
            while (status)
            {
                if (login == false)
                {
                    Console.WriteLine("Stock Yönetim Sistemine Hoşgeldiniz");
                    Console.WriteLine("Devam Etmek İçin Lütfen Giriş Yapınız");
                    userController.Login(out userId, out userRole);
                    if (userId != 0 && userRole != "Hata")
                    {
                        login = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Hatalı Kullanıcı Girişi Yaptınız Giriş Ekranına Dönmek İçin Herhangi Bir Tuşa Basınız");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else if (login == true)
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
                    else if (userRole == "Sevkiyatçı")
                    {
                        Console.WriteLine("1. Kullanıcı Bilgilerini Görüntüle");
                        Console.WriteLine("2. Kullanıcı Bilgilerini Güncelle");
                        Console.WriteLine("3. Deponun Bilgilerini Görüntüle");
                        Console.WriteLine("4. Sevkiyat yap");
                        Console.WriteLine("5. Mevcut Depoya Yapılmış Sevkiyatları Görüntüle");
                        rolec = 's';
                    }
                    Console.WriteLine("0. Çıkış Yap");

                    Console.Write("Seçim: ");
                    string select = Console.ReadLine().Substring(0, 1);
                    string guide = select + rolec;
                    if (!string.IsNullOrWhiteSpace(select) && int.TryParse(select, out int numberSelect) == true)
                    {

                        switch (guide)
                        {
                            //Admin panel girdileri
                            case "1a": roleController.Menu(); break;
                            case "2a": userController.Menu(); break;
                            case "3a": warehouseController.Menu(); break;
                            case "4a": categoryController.Menu(); break;
                            case "5a": productController.Menu(); break;
                            //Sevkiyat panel girdileri
                            case "1s": userController.Getvoid(userId); break;
                            case "2s": userController.Update(userId); break;
                            case "3s": warehouseController.Get(); break; //user id gönderip user id ye göre deponun gelmesi lazım!!
                            case "4s": break; //shipment !!
                            case "5s": break; //shipment
                            case "0a":
                            case "0s":
                                status = false;
                                break;
                            default: GetSwitchErrorMessage(); break;
                        }
                    }
                    else
                    {
                        GetSwitchErrorMessage();
                    }
                }
            }
        }

        public static void GetRegisterPage()
        {
            UserController userController = new UserController();
            userController.Add();
            CheckForContinue();

        }


        public static void CheckForContinue()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine();
            Console.WriteLine("Devam Etmek İçin Herhangi Bir Tuşa Basınız");
            Console.ReadKey();
        }

        public static bool ProgramExit()
        {
            Console.Write("Program Kapatılıyor");
            int time = 4000;
            for (int i = 0; i < 6; i++)
            {
                Console.Write(".");
                Console.Beep();
                time /= 5;
                Thread.Sleep(time);
            }
            return false;
        }

        public static void GetSwitchErrorMessage()
        {
            Console.Clear(); 
            Console.WriteLine("Hatalı Giriş Yaptınız Lütfen Tekrar Giriniz"); 
            CheckForContinue();
        }



    }
}
