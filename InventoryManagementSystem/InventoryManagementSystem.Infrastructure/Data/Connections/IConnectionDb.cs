using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InventoryManagementSystem.Infrastructure.Data.Connections
{
    public interface IConnectionDb : IDisposable
    {
        IDbConnection OpenConnection();
        void CloseConnection();
    }
}
