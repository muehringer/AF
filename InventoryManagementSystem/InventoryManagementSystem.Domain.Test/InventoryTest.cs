using InventoryManagementSystem.Infrastructure.Data.Interfaces;
using System;
using Xunit;

namespace InventoryManagementSystem.Domain.Test
{
    public class InventoryTest
    {
        private IInventoryRepository _inventoryRepository;

        [Fact]
        public void Update_Quantity_Should_Be_Result_Bigger_Than_Zero()
        {
            var result =_inventoryRepository.Update(new Infrastructure.Data.Maps.Inventory()
                        {
                            IdInventory = 1,
                            IdProduct = 1,
                            IdUser = 1,
                            Quantity = 10
                        });

            Assert.NotEqual(0, result);
        }
    }
}
