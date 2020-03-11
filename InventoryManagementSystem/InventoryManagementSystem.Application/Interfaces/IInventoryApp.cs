using InventoryManagementSystem.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Application.Interfaces
{
    public interface IInventoryApp
    {
        bool UpdateQuantity(InventoryVm vm);
    }
}
