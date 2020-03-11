using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Infrastructure.Data.Maps
{
    public class Inventory
    {
        public int IdInventory { get; set; }
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }
    }
}
