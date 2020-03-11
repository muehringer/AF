using Dapper;
using InventoryManagementSystem.Infrastructure.Data.Connections;
using InventoryManagementSystem.Infrastructure.Data.Interfaces;
using InventoryManagementSystem.Infrastructure.Data.Maps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InventoryManagementSystem.Infrastructure.Data.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private IConnectionDb Connection;
        private IDbConnection IDbConn;

        public InventoryRepository()
            => Connection = new ConnectionDb();
        
        public Inventory GetById(int idInventory)
        {
            IDbConn = Connection.OpenConnection();

            string query = @"SELECT DISTINCT
                                    IdInventory, 
                                    IdUser,
                                    IdProduct, 
                                    Quantity, 
                                    RegisterDate, 
                                    UpdateDate, 
                                    Active 
                            FROM Inventory 
                            WHERE IdInventory = @IdInventory 
                            AND Active = 1;";

            Inventory inventory = new Inventory();

            try
            {
                inventory = IDbConn.Query<Inventory>(query, new { IdInventory = idInventory }).FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                Connection.CloseConnection();
            }

            return inventory;
        }

        public int Update(Inventory inventory)
        {
            int id = 0;

            IDbConn = Connection.OpenConnection();

            string query = @"UPDATE Inventory
                             SET Quantity = @Quantity,
                                 IdUser = @IdUser,
                                 UpdateDate = SYSDATE()
                             WHERE IdInventory = @IdInventory
                             AND Active = 1;";

            try
            {
                id = IDbConn.Execute(query, new
                {
                    Quantity = inventory.Quantity,
                    IdUser = inventory.IdUser,
                    IdInventory = inventory.IdInventory,
                });
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                Connection.CloseConnection();
            }

            return id;
        }
    }
}
