using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Application.ViewModels
{
    public class InventoryVm
    {
        public int IdInventory { get; set; }
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
    }
}
