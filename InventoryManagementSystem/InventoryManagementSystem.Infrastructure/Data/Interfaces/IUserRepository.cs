using System;
using System.Collections.Generic;
using System.Text;
using InventoryManagementSystem.Infrastructure.Data.Maps;

namespace InventoryManagementSystem.Infrastructure.Data.Interfaces
{
    public interface IUserRepository
    {
        User GetByEmailPassword(string email, string password);
        int Create(User user);
    }
}
