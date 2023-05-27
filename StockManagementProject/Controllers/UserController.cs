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
    internal class UserController : IController<User>
    {
        RoleRepository roleRepository = new RoleRepository();
        UserRepository repository = new UserRepository();
        WarehouseRepository warehouseRepository = new WarehouseRepository();
        public void Login(out int userId,out string userRole)
        {
            User user = new User();

            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Şifre: ");
            string password = Console.ReadLine();

            user=repository.GetByLogin(email, password);
            var sorgu = (from role in roleRepository.GetAll()
                         where user.RoleId == role.Id
                         select new
                         {
                             rolAd=role.Name
                         }).FirstOrDefault();
            if (user != null && sorgu.rolAd !=null)
            {
                userId = user.Id;
                userRole = sorgu.rolAd;
            }
            else
            {
                userId = 0;
                userRole = "Hata";
            }
 

        }

        
        public void Add()
        {
            Console.WriteLine(repository.Add(SetValue()) ?
                    "Ekleme Başarılı" :
                    "Ekleme Başarısız");
        }

        public void Delete()
        {
            //Console.Write("Silinecek Kullanıcı Id Giriniz: ");
            //int id = Convert.ToInt32(Console.ReadLine());
            //var sorgu = (from user in userRepository.GetAll()
            //             where user.RoleId == id
            //             select new
            //             {
            //                 userIds = user.Id
            //             }).ToList();
            //if (id > 1 && sorgu.Count == 0)
            //{
            //    Console.WriteLine(repository.Delete(id) ?
            //        "Silme İşlemi Başarılı" :
            //        "Silme İşlemi Başarısız");
            //}
            //else
            //{
            //    Console.WriteLine("Silme İşlemi Başarısız");
            //    Console.WriteLine("Silmeye Çalıştığınız Role Sahip Kullanıcılar Bulunuyor Olabilir");
            //}
        }

        
        public User Get(int id)
        {
            User user = repository.GetById(id);
            if (user != null)
            {
                Console.WriteLine("Kullanıcı Detayları");
                Console.WriteLine("-------------");
                Console.WriteLine("Id       : " + user.Id);
                Console.WriteLine("Ad       : " + user.Name);
                Console.WriteLine("Soyad       : " + user.Surname);
                Console.WriteLine("Soyad       : " + user.Email);
                //role
            }
            else
            {
                Console.WriteLine("Kullanıcı Bulunamadı");
            }
            return user;
        }

        public void GetAll()
        {
            
            Console.WriteLine("Kullanıcı Listesi");
            if (repository.GetAll().Count > 0)
            {
                foreach (User user in repository.GetAll())
                {
                    var sorgu = (from warehouse in warehouseRepository.GetAll()
                                 where warehouse.ManagerId == user.Id
                                 select new
                                 {
                                     DepoId = warehouse.Id,
                                     DepoAd = warehouse.Name
                                 }).FirstOrDefault();
                    Console.WriteLine("-------------");
                    Console.WriteLine("Id                  : " + user.Id);
                    Console.WriteLine("Ad                  : " + user.Name);
                    Console.WriteLine("Soyad               : " + user.Surname);
                    Console.WriteLine("Soyad               : " + user.Email);
                    if(sorgu !=null )
                    {
                        Console.WriteLine("Bağlı Olduğu Depo Ad:" + sorgu.DepoAd);
                        Console.WriteLine("Bağlı Olduğu Depo Id:" + sorgu.DepoId);
                    }
                    else
                    {
                        Console.WriteLine("Kullanıcının Bağlı Olduğu Depo Bulunmamaktadır");
                    }


                }
            }
            else
            {
                Console.WriteLine("Kullanıcı Listesi Boş");
            }
        }

        public void Menu()
        {
            //Admin Menu
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------------------");
                Console.WriteLine("--Kullanıcı İşlemleri--");
                Console.WriteLine("-----------------------");
                Console.WriteLine("1. Kullanıcı Sil");
                Console.WriteLine("2. Kullanıcıları Listele ");
                Console.WriteLine("3. Kullanıcı Rol Ata");
                Console.WriteLine("0. Üst Menü");
                Console.Write("Seçiminiz: ");
                int select = Convert.ToInt32(Console.ReadLine().Substring(0, 1));

                switch (select)
                {
                    case 1: Add(); break;
                    case 2: GetAll(); break;
                    case 3: Update(); break;
                    case 0: return;
                    default: Console.Clear(); Console.WriteLine("Hatalı Giriş Yaptınız Lütfen Tekrar Giriniz"); Thread.Sleep(1500); break;
                }



            }
        }

        public User SetValue()
        {
            User user = new User();

            Console.Write("Ad: ");
            user.Name = Console.ReadLine();
            Console.Write("Soyad: ");
            user.Surname = Console.ReadLine();
            Console.Write("Email : ");
            user.Email = Console.ReadLine();
            Console.Write("Şifre : ");
            user.Password = Console.ReadLine();
            user.RoleId=1;
            user.IsStatus = true;

            return user;
        }

        public void Update(int id)
        {
            User user = Get(id);
            if (user != null)
            {
                User setUser = SetValue();
                if (setUser != null)
                {
                    setUser.Id = user.Id;
                    user = setUser;
                    repository.Update(user);
                }
            }
        }

        public void Update()
        {
            Console.Clear();
            Console.Write("Rolünü Değiştirmek İstediğiniz Kullanıcının Id Giriniz:");
            int select = Convert.ToInt32(Console.ReadLine().Substring(0, 1));
            User user = Get(select);
            Console.WriteLine("-----------------");
            Console.WriteLine();
            Console.Write("Kullanıcıya Atanacak Yeni Rolün Idsini Giriniz:");
            int newRoleId= Convert.ToInt32(Console.ReadLine().Substring(0, 1));
            user.RoleId=newRoleId;
            repository.Update(user);

        }

        public User Get()
        {
            throw new NotImplementedException();
        }
    }
}
