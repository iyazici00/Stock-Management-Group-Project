using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Controllers
{
    internal class RoleController : IController<Role>
    {
        RoleRepository repository=new RoleRepository();
        UserRepository userRepository=new UserRepository();
        public void Add()
        {
            Console.WriteLine(repository.Add(SetValue()) ?
                                "Ekleme Başarılı" :
                                "Ekleme Başarısız");
        }

        public void Delete()
        {
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
                Console.WriteLine(repository.Delete(id) ?
                    "Silme İşlemi Başarılı" :
                    "Silme İşlemi Başarısız");
            }
            else
            {
                Console.WriteLine("Silme İşlemi Başarısız");
                Console.WriteLine("Silmeye Çalıştığınız Role Sahip Kullanıcılar Bulunuyor Olabilir");
            }
        }

        public Role Get()
        {
            Console.Clear();
            Console.Write("Detaylar İçin Rol Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Role rol = repository.GetById(id);
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
            Console.ReadKey();
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
                short select=Convert.ToInt16(Console.ReadLine().Substring(0,1));


                switch (select)
                {
                    case 1: Add(); break;
                    case 2: GetAll(); break;
                    case 3: Update(); break;
                    case 4: Delete(); break;
                    case 0: return;
                    default:
                        break;
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

        public void Update()
        {
            Console.Clear();
            GetAll();
            Console.WriteLine( "--------------");
            Console.WriteLine();
            Role rol = Get();
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
