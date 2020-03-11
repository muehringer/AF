using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Application.Interfaces
{
    public interface IAuthenticationApp
    {
        bool Authenticate(string email, string password);
    }
}
