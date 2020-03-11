using AutoMapper;
using InventoryManagementSystem.Application.Interfaces;
using InventoryManagementSystem.Application.ViewModels;
using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Application.Apps
{
    public class InventoryApp : IInventoryApp
    {
        private Inventory inventory;

        public bool UpdateQuantity(InventoryVm vm)
        {
            inventory = new Inventory(vm.IdInventory, vm.IdUser, vm.IdProduct, vm.Quantity);

            return inventory.UpdateQuantity();
        }
    }
}
