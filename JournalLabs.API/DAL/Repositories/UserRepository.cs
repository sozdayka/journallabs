using Dapper;
using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JournalLabs.API.DAL.Repositories
{
    public class UserRepository
    {
        private string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void CreateUser(User userModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[Users]([Id],[Login],[Password],[Role]) VALUES (@Id,@Login,@Password, @Role)";
                try
                {
                    var result = db.Execute(insertQuery, userModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateUser(User userModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE Users Set Login = @Login, Password = @Password, Role = @Role Where Id = @Id";
                    var result = db.Execute(insertQuery, userModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public User GetUserById(string userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Users Where Id = @userId"; 
                try
                {
                    var result = db.Query<User>(insertQuery, new { userId = userId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteUserById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM Users Where Id = @Id";
                    var res = db.Execute(insertQuery, new { Id = id });
                    return res > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

    }
}