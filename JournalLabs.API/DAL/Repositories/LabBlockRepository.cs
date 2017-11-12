using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using JournalLabs.API.Models;

namespace JournalLabs.API.DAL.Repositories
{
    public class LabBlockRepository
    {
        private string _connectionString;

        public LabBlockRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void CreateLabBlock(LabBlock labBlockModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[LabBlocks]([Id],[FirstDate],[FirstMark],[SecondDate],[SecondMark],[StudentId],[KindOfWorkId],[JournalId]) VALUES (@Id,@FirstDate,@FirstMark,@SecondDate,@SecondMark,@StudentId,@KindOfWorkId,@JournalId)";
                try
                {
                    var result = db.Execute(insertQuery, labBlockModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateLabBlock(LabBlock labBlockModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE LabBlocks Set FirstDate = @FirstDate,FirstMark = @FirstMark,SecondDate = @SecondDate,SecondMark = @SecondMark,StudentId = @StudentId,KindOfWorkId=@KindOfWorkId,JournalId = @JournalId Where Id = @Id";
                    var result = db.Execute(insertQuery, labBlockModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public LabBlock GetLabBlockById(string labBlockId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From LabBlocks Where Id = @labBlockId";
                try
                {
                    var result = db.Query<LabBlock>(insertQuery, new { labBlockId = labBlockId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteLabBlockById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM LabBlocks Where Id = @Id";
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