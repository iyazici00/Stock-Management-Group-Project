using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
        RoleController roleController = new RoleController();
        public void Login(out int userId,out string userRole)
        {
            User user = new User();

            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Şifre: ");
            string password = Console.ReadLine();

            user=repository.GetByLogin(email, password);

            if (user != null)
            {
                var sorgu = (from role in roleRepository.GetAll()
                             where user.RoleId == role.Id
                             select new
                             {
                                 rolAd = role.Name
                             }).FirstOrDefault();
                if (sorgu.rolAd != null)
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
            else
            {
                userId = 0;
                userRole = "Hata";
            }
 

        }

        
        public void Add()
        {


            Console.Clear();
            Console.WriteLine();
            if (repository.Add(SetValue()) == true)
            {
                Console.Clear();
                Console.WriteLine("Kayıt Başarılı");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Kayıt Başarısız");
            }

        }

        public void Delete()
        {
            GetAll();
            Console.Write("Silinecek Kullanıcı Id Giriniz: ");
            string select = Console.ReadLine().Substring(0, 1);
            if (!string.IsNullOrWhiteSpace(select) && int.TryParse(select, out int numberSelect) == true)
            {
                Console.Clear();
                User user = Get(numberSelect);
                Console.WriteLine("------------");
                Console.WriteLine("Silmek istediğinize emin misiniz? (e) (h)");
                string choose=Console.ReadLine().ToLower().Substring(0,1);
                switch (choose)
                {
                    case "e":
                        DeleteHelp(user);
                        break;
                    case "h":
                        Console.Clear();
                        CheckForContinue();
                        break;
                    default:
                        GetSwitchErrorMessage();

                        break;
                }

                
            }
            else
            {
                GetSwitchErrorMessage();
            }
        }
        public  void GetSwitchErrorMessage()
        {
            Console.Clear();
            Console.WriteLine("Hatalı Giriş Yaptınız Lütfen Tekrar Giriniz");
            CheckForContinue();
        }
        public void DeleteHelp(User user)
        {
            foreach (var warehouse in warehouseRepository.GetAll())
            {
                if (warehouse.ManagerId == user.Id)
                {
                    warehouse.ManagerId = 1;
                    warehouseRepository.Update(warehouse);

                }
            }
            Console.Clear();
            Console.WriteLine(repository.Delete(user.Id) ?
                                "Silme Başarılı" :
                                "Silme Başarısız");
            Thread.Sleep(2000);


        }
        
        public User Get(int id)
        {
            Console.Clear();
            User user = repository.GetById(id);
            var rol = (from role in roleRepository.GetAll()
                       where role.Id == user.RoleId
                       select new
                       {
                           rolAd = role.Name,
                       }).FirstOrDefault();
            if (user != null)
            {
                Console.WriteLine("Kullanıcı Detayları");
                Console.WriteLine("-------------");
                Console.WriteLine("Ad    : " + user.Name);
                Console.WriteLine("Soyad : " + user.Surname);
                Console.WriteLine("Email : " + user.Email);
                Console.WriteLine("Rol    : " + rol.rolAd);
            }
            else
            {
                Console.WriteLine("Kullanıcı Bulunamadı");
            }
            return user;
        }
        public User GetJustObject(int id)
        {
            User user = repository.GetById(id);          
            return user;
        }
        public void Getvoid(int id)
        {
            Console.Clear();

            User user = repository.GetById(id);
            var rol = (from role in roleRepository.GetAll()
                       where role.Id == user.RoleId
                       select new
                       {
                           rolAd = role.Name,
                       }).FirstOrDefault();
            if (user != null)
            {
                Console.WriteLine("Kullanıcı Detayları");
                Console.WriteLine("-------------");
                Console.WriteLine("Ad    : " + user.Name);
                Console.WriteLine("Soyad : " + user.Surname);
                Console.WriteLine("Email : " + user.Email);
                Console.WriteLine("Rol    : " + rol.rolAd);

                Console.WriteLine("Devam Etmek İçin Herhangi Bir Tuşa Basınız");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Kullanıcı Bulunamadı");
            }
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
                                     depoId = warehouse.Id,
                                     depoAd = warehouse.Name
                                 }).FirstOrDefault();
                    var rol = (from role in roleRepository.GetAll()
                                 where role.Id == user.RoleId
                                 select new
                                 {
                                     rolAd= role.Name,
                                 }).FirstOrDefault();
                    Console.WriteLine("-------------");
                    Console.WriteLine("Id     : " + user.Id);
                    Console.WriteLine("Ad     : " + user.Name);
                    Console.WriteLine("Soyad  : " + user.Surname);
                    Console.WriteLine("Email  : " + user.Email);
                    Console.WriteLine("Rol    : "+ rol.rolAd);
                    if (sorgu !=null )
                    {
                        Console.WriteLine("Bağlı Olduğu Depo Ad:" + sorgu.depoAd);
                        Console.WriteLine("Bağlı Olduğu Depo Id:" + sorgu.depoId);
                    }
                    else
                    {
                        Console.WriteLine("Kullanıcının Bağlı Olduğu Depo Bulunmamaktadır");
                    }
                    Console.WriteLine("------------------------");


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



                string select = Console.ReadLine().Substring(0, 1);
                if (!string.IsNullOrWhiteSpace(select) && int.TryParse(select, out int numberSelect) == true)
                {
                    switch (numberSelect)
                    {
                        case 1: Console.Clear(); Delete();  break;
                        case 2: Console.Clear(); GetAll(); CheckForContinue(); break;
                        case 3: Update(); break;
                        case 0: return;
                        default: Console.Clear(); Console.WriteLine("Hatalı Giriş Yaptınız Lütfen Tekrar Giriniz"); Thread.Sleep(1500); break;
                    }
                }
                else
                {
                    Console.Clear(); Console.WriteLine("Hatalı Giriş Yaptınız Lütfen Tekrar Giriniz"); Thread.Sleep(1500);
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

            return user;
        }

        public void Update(int id)
        {
            User user = Get(id);
            Console.WriteLine();
            Console.WriteLine("-----------------");
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
            GetAll();
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            roleController.GetAll();
            Console.WriteLine("------------------------");
            Console.WriteLine();
            Console.Write("Rolünü Değiştirmek İstediğiniz Kullanıcının Id Giriniz:");
            int select = Convert.ToInt32(Console.ReadLine().Substring(0, 1));
            User user = GetJustObject(select);
            Console.WriteLine("-----------------");
            Console.WriteLine();
            Console.Write("Kullanıcıya Atanacak Yeni Rolün Idsini Giriniz:");
            int newRoleId= Convert.ToInt32(Console.ReadLine().Substring(0, 1));
            user.RoleId=newRoleId;
            repository.Update(user);

        }
        //public User Get
        public User Get()
        {
            throw new NotImplementedException();
        }


    }
}
