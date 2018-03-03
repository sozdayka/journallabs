using Dapper;
using JournalLabs.API.Models;
using JournalLabs.API.ViewModels;
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
                string insertQuery = @"INSERT INTO [dbo].[Users]([Id],[Login],[Password],[Role],[CathedraId]) VALUES (@Id,@Login,@Password, @Role,@CathedraId)";
                try
                {
                    var result = db.Execute(insertQuery, userModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public User SignInUser(User userModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Users Where Login = @Login AND Password=@Password";
                try
                {
                    var result = db.Query<User>(insertQuery, userModel);
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
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
        public List<User> GetAllAssistants()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Users Where Role = 'Assistant'";
                try
                {
                    var result = db.Query<User>(insertQuery);
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public List<TeacherViewModelForExternalApi> GetAllTeachers()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Users Where Role = 'Teacher'";
                try
                {
                    var result = db.Query<TeacherViewModelForExternalApi>(insertQuery);
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}