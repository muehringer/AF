using System;
using System.Collections.Generic;
using System.Text;
using InventoryManagementSystem.Application.Interfaces;
using InventoryManagementSystem.Domain.Entities;

namespace InventoryManagementSystem.Application.Apps
{
    public class AuthenticationApp : IAuthenticationApp
    {
        private User user;

        public bool Authenticate(string email, string password)
        {
            user = new User(email, password);

            return user.Authenticate();
        }
    }
}
