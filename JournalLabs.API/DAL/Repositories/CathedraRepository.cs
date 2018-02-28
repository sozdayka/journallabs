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
    public class CathedraRepository
    {
        private string _connectionString;

        public CathedraRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void CreateCathedra(Cathedra cathedraModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[Cathedras]([Id],[ShortName],[FullName],[Description]) VALUES (@Id,@ShortName,@FullName,@Description)";
                try
                {
                    var result = db.Execute(insertQuery, cathedraModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateCathedra(Cathedra cathedraModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE Cathedras Set ShortName = @ShortName,FullName=@FullName,Description=@Description Where Id = @Id";
                    var result = db.Execute(insertQuery, cathedraModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public Cathedra GetCathedraById(string cathedraId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Cathedras Where Id = @cathedraId";
                try
                {
                    var result = db.Query<Cathedra>(insertQuery, new { cathedraId = cathedraId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteCathedraById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM Cathedras Where Id = @Id";
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