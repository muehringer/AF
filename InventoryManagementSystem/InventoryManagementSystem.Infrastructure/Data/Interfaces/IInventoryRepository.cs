using InventoryManagementSystem.Infrastructure.Data.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Infrastructure.Data.Interfaces
{
    public interface IInventoryRepository
    {
        Inventory GetById(int idInventory);
        int Update(Inventory inventory);
    }
}
