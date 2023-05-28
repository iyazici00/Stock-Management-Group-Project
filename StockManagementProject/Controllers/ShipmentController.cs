using StockManagementProject.Interfaces;
using StockManagementProject.Models;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StockManagementProject.Controllers
{
    internal class ShipmentController
    {
        ShipmentRepository repository=new ShipmentRepository();
        WarehouseRepository warehouseRepository=new WarehouseRepository();
        UserRepository userRepository = new UserRepository();
        ProductRepository productRepository = new ProductRepository();
        WarehouseController warehouseController = new WarehouseController();
        WarehouseProductStockRepository warehouseProductStockRepository= new WarehouseProductStockRepository();
        
        public void MakeShipment(int managerId)
        {
            Console.Clear();
            Warehouse shipperWarehouse = warehouseRepository.GetByManagerId(managerId);
            warehouseController.WarehouseProducts(shipperWarehouse.Id);
            Console.WriteLine();
            Console.WriteLine("-----------------------");
            Console.Write("Sevkiyat Yapmak İstediğiniz Ürünün Id'si :");
            int id = Convert.ToInt32(Console.ReadLine());
            Product product = productRepository.GetById(id);
            Warehouse reciverWarehouse = warehouseController.GetShp(shipperWarehouse.Id);
            if (product != null&& reciverWarehouse != null)
            {
                Console.Write("Sevkiyat Yapmak İstediğiniz Ürün Miktarı :");
                int quantity = Convert.ToInt32(Console.ReadLine());
                WarehouseProductStock reciverStok = warehouseProductStockRepository.GetAll().FirstOrDefault(x => x.WarehouseId == reciverWarehouse.Id && x.ProductId == product.Id);
                WarehouseProductStock shipperStok = warehouseProductStockRepository.GetAll().FirstOrDefault(x => x.WarehouseId == shipperWarehouse.Id && x.ProductId == product.Id);
                if (shipperStok.ProductQuantity >= quantity)
                {
                    shipperStok.ProductQuantity -= quantity;
                    reciverStok.ProductQuantity += quantity;
                    warehouseProductStockRepository.Update(shipperStok);
                    warehouseProductStockRepository.Update(reciverStok);
                    Shipment shipment = new Shipment();
                    shipment.ShipperManagerId = managerId;
                    shipment.ProductId = product.Id;
                    shipment.ProductQuantity= quantity;
                    shipment.ShipperWarehouseId = shipperWarehouse.Id;
                    shipment.ReceiverWarehouseId = reciverWarehouse.Id;
                    repository.Add(shipment);
                    Console.Clear();
                    Console.WriteLine("Sevkiyat Gerçekleştirilmiştir");
                }
                else
                {
                    Console.Clear();

                    Console.WriteLine("Depodaki stok miktarınız yetersizdir!");
                }


            }
            else
            {
                Console.Clear();
                
                Console.WriteLine("Girmiş olduğunuz ürün veya Depo bulunamadı!");
            }

        }


        public void GetAll(int managerId)
        {
            Console.Clear();
            Console.WriteLine("Sevkiyat Listesi");
            Warehouse warehouse=warehouseRepository.GetByManagerId(managerId);
            if (repository.GetAll().Count > 0)
            {
                foreach (Shipment shipment in repository.GetAll().Where(x=>x.ShipperWarehouseId==warehouse.Id||x.ReceiverWarehouseId==warehouse.Id))
                {
                    User user = userRepository.GetById(shipment.ShipperManagerId);
                    Console.WriteLine("-------------");
                    Console.WriteLine("Id                    : " + shipment.Id);
                    Console.WriteLine("Gönderici Depo        : " + warehouseRepository.GetById(shipment.ShipperWarehouseId).Name);
                    Console.WriteLine("Alıcı Depo            : " + warehouseRepository.GetById(shipment.ReceiverWarehouseId).Name);
                    if (user != null)
                    {
                        Console.WriteLine("İşlemi Gerçekleştiren : " +user.Name+" "+user.Surname );
                    }
                    else
                    {
                        Console.WriteLine("İşlemi Gerçekleştiren : "+shipment.Id+ " Id'li kullanıcı sistemden silinmiştir");
                        Console.WriteLine("Detaylı bilgi için lütfen adminle görüşünüz!!");
                    }
                    Console.WriteLine("Sevk edilen Ürün      :"+ productRepository.GetById(shipment.ProductId).Name);
                    Console.WriteLine("Sevk edilen Miktar    :"+ shipment.ProductQuantity);
                    Console.WriteLine("Sevkiyat Tarihi       :"+ "{0:dd/MM/yyyy HH:mm:ss}", shipment.ShipmentDate);
                    Console.WriteLine("--------------------------");
                }
            }
            else
            {
                Console.WriteLine("Sevkiyat Listesi Boş");
            }
        }


    }
}
