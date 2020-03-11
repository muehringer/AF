using InventoryManagementSystem.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Inventory
    {
        private IInventoryRepository _inventoryRepository;

        public Inventory(int idInventory, int idUser, int idProduct, int quantity)
        {
            IdInventory = idInventory;
            IdUser = idUser;
            IdProduct = idProduct;
            Quantity = quantity;
        }

        public int IdInventory { get; set; }
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }

        public bool UpdateQuantity()
            => _inventoryRepository.Update(new Infrastructure.Data.Maps.Inventory()
                {
                    IdInventory = IdInventory,
                    IdProduct = IdProduct,
                    IdUser = IdUser,
                    Quantity = Quantity
                }) > 0;
    }
}
