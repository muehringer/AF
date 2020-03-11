using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using InventoryManagementSystem.Infrastructure.Data.Connections;
using InventoryManagementSystem.Infrastructure.Data.Interfaces;
using InventoryManagementSystem.Infrastructure.Data.Maps;

namespace InventoryManagementSystem.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IConnectionDb Connection;
        private IDbConnection IDbConn;

        public UserRepository()
            => Connection = new ConnectionDb();

        public User GetByEmailPassword(string email, string password)
        {
            IDbConn = Connection.OpenConnection();

            string query = @"SELECT DISTINCT
                                    IdUser, 
                                    Name,
                                    EMail, 
                                    Password, 
                                    RegisterDate, 
                                    UpdateDate, 
                                    Active 
                            FROM User 
                            WHERE EMail = @EMail 
                            AND Password = @Password 
                            AND Active = 1;";

            User user = new User();

            try
            {
                user = IDbConn.Query<User>(query, new { EMail = email, Passsword = password }).FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                Connection.CloseConnection();
            }

            return user;
        }

        public int Create(User user)
        {
            int id = 0;

            IDbConn = Connection.OpenConnection();

            string query = @"INSERT INTO User (
                             Name,
                             EMail,
                             Password,
                             RegisterDate,
                             UpdateDate,
                             Active
                            ) VALUES (
                             @Name,
                             @EMail,
                             @Password,
                             SYSDATE(),
                             SYSDATE(),
                             1
                            );";

            try
            {
                id = IDbConn.Execute(query, new
                {
                    Name = user.Name,
                    EMail = user.EMail,
                    Password = user.Password,
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
