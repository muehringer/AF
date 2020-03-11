using InventoryManagementSystem.Application.Apps;
using InventoryManagementSystem.Application.Interfaces;
using InventoryManagementSystem.Infrastructure.Data.Connections;
using InventoryManagementSystem.Infrastructure.Data.Interfaces;
using InventoryManagementSystem.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.IoC
{
    public class InjectorContainer
    {
        public IServiceCollection GetScope(IServiceCollection interfaceService)
        {
            #region Apps

            interfaceService.AddScoped(typeof(IAuthenticationApp), typeof(AuthenticationApp));
            interfaceService.AddScoped(typeof(IInventoryApp), typeof(InventoryApp));

            #endregion

            #region Data / Repositories

            interfaceService.AddScoped(typeof(IConnectionDb), typeof(ConnectionDb));
            interfaceService.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            interfaceService.AddScoped(typeof(IInventoryRepository), typeof(InventoryRepository));

            #endregion

            return interfaceService;
        }
    }
}
