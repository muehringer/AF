using System;
using System.Collections.Generic;
using System.Text;
using InventoryManagementSystem.Infrastructure.Data.Interfaces;

namespace InventoryManagementSystem.Domain.Entities
{
    public class User
    {
        private IUserRepository _userRepository;

        public User(IUserRepository userRepository)
            => _userRepository = userRepository;

        public User(string email, string password)
        {
            EMail = email;
            Password = password;            
        }

        public int IdUser { get; private set; }
        public string EMail { get; private set; }
        public string Password { get; private set; }
        
        public bool Authenticate()
        {
            var result = _userRepository.GetByEmailPassword(EMail, Password);

            return result != null ? (result.IdUser != 0 ? true : false) : false;
        }
    }
}
