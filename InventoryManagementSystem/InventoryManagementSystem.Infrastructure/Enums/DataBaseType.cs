using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace InventoryManagementSystem.Infrastructure.Enums
{
    public enum DataBaseType
    {
        [Description(nameof(MySql))]
        MySql = 0,
    }
}
