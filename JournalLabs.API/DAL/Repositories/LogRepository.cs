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
    public class LogRepository
    {
        private string _connectionString;

        public LogRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public void CreateLog(Log logModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[Logs]([Id],[Text],[Type]) VALUES (@Id,@Text,@Type)";
                try
                {
                    var result = db.Execute(insertQuery, logModel);
                }
                catch (Exception ex)
                {

                }

            }
        }
        public List<Log> GetLogsByType(string type)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Logs Where Type = @type";
                try
                {
                    var result = db.Query<Log>(insertQuery, new { type = type });
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