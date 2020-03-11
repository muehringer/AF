using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystem.Application.Interfaces;
using InventoryManagementSystem.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryApp _inventoryApp;

        public InventoryController(IInventoryApp inventoryApp)
            => _inventoryApp = inventoryApp;

        [HttpPut]
        [Route("UpdateQuantity")]
        public bool UpdateQuantity(InventoryVm vm)
            => _inventoryApp.UpdateQuantity(vm);
    }
}
