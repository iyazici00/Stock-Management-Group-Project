using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockManagementProject.Controllers
{
    internal class RoleController : IController<Role>
    {
        RoleRepository repository=new RoleRepository();
        UserRepository userRepository=new UserRepository();
        public void Add()
        {
            Console.Clear();
            Console.WriteLine(repository.Add(SetValue()) ?
                                "Ekleme Başarılı" :
                                "Ekleme Başarısız");
            Thread.Sleep(2000);

        }

        public void Delete()
        {
            GetAll();
            Console.WriteLine();
            Console.WriteLine("------------");
            Console.Write("Silinecek Rol Id Giriniz: ");

            int id = Convert.ToInt32(Console.ReadLine());
            var sorgu = (from user in userRepository.GetAll()
                         where user.RoleId == id
                         select new
                         {
                             userIds = user.Id
                         }).ToList();
            if (id > 1 && sorgu.Count==0)
            {
                Console.Clear();
                Console.WriteLine(repository.Delete(id) ?
                    "Silme İşlemi Başarılı" :
                    "Silme İşlemi Başarısız");
                Thread.Sleep(2500);
            }
            else
            {
                Console.Clear();

                Console.WriteLine("Silme İşlemi Başarısız");
                Console.WriteLine("Silmeye Çalıştığınız Role Sahip Kullanıcılar Bulunuyor Olabilir");
                Thread.Sleep(3000);

            }
        }

        public Role Get()
        {
            Console.WriteLine();
            Console.Write("Detaylar İçin Rol Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Role rol = repository.GetById(id);
            Console.Clear();
            if (rol != null)
            {
                Console.WriteLine("Rol Detayları");
                Console.WriteLine("-------------");
                Console.WriteLine("Id       : " + rol.Id);
                Console.WriteLine("Ad       : " + rol.Name);
                Console.WriteLine("Durum    : " + (rol.IsStatus ? "Aktif" : "Pasif"));
            }
            else
            {
                Console.WriteLine("Product Bulunamadı");
            }
            return rol;
        }

        public void GetAll()
        {
            Console.WriteLine("Rol Listesi");
            if(repository.GetAll().Count>0)
            {
                foreach(Role rol in repository.GetAll())
                {
                    Console.WriteLine("-------------");
                    Console.WriteLine("Id       : " + rol.Id);
                    Console.WriteLine("Ad       : " + rol.Name);
                    Console.WriteLine("Durum    : " + (rol.IsStatus ? "Aktif" : "Pasif"));
                }
            }
            else
            {
                Console.WriteLine("Rol Listesi Boş");
            }

        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------------------");
                Console.WriteLine("-----Rol İşlemleri-----");
                Console.WriteLine("-----------------------");
                Console.WriteLine("1. Rol Oluştur ");
                Console.WriteLine("2. Rolleri Listele");
                Console.WriteLine("3. Rol Güncelle");
                Console.WriteLine("4. Rol Sil");
                Console.WriteLine("0. Üst Menü");
                Console.Write("Seçiminiz: ");
                //short select=Convert.ToInt16(Console.ReadLine().Substring(0,1));
                string select= Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(select) && int.TryParse(select, out int numberSelect)==true)
                {
                    switch (numberSelect)
                    {
                        case 1: Add(); break;
                        case 2: Console.Clear(); GetAll(); CheckForContinue(); break;
                        case 3: Console.Clear(); Update(); break;
                        case 4: Console.Clear(); Delete(); break;
                        case 0: return;
                        default: Console.WriteLine("Yanlış Giriş Gerçekleştirdiniz Lütfen Tekrar Deneyiniz"); break;
                    }
                }
                else
                {
                    Console.WriteLine("Yanlış Giriş Gerçekleştirdiniz Lütfen Tekrar Deneyiniz");
                    Console.WriteLine();
                }
            }
        }

        public Role SetValue()
        {
            Role role = new Role();

            Console.Write("Rol Adı: ");
            role.Name = Console.ReadLine();
            Console.Write("Rol Durumu Aktif (A) Pasif (P):");
            role.IsStatus = Console.ReadLine().Substring(0, 1).ToLower() == "a" ? true : false;

            return role;
        }
        public void CheckForContinue()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine();
            Console.WriteLine("Devam Etmek İçin Herhangi Bir Tuşa Basınız");
            Console.ReadKey();
        }
        public void Update()
        {
            GetAll();
            Console.WriteLine( "--------------");
            Console.WriteLine();
            Role rol = Get();
            Console.WriteLine();
            Console.WriteLine("---------------");
            if (rol != null)
            {
                Role setRol = SetValue();
                if (setRol != null)
                {
                    setRol.Id = rol.Id;
                    rol = setRol;
                    repository.Update(rol);
                }
            }
        }
    }
}
