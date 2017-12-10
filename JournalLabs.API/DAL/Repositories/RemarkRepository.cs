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
    public class RemarkRepository
    {
        private string _connectionString;

        public RemarkRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public Remark CreateRemark(Remark remarkModel)
        {
            remarkModel.Id = Guid.NewGuid();
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[Remarks]([Id],[RemarkText],[StudentId],[JournalId]) VALUES (@Id,@RemarkText,@StudentId, @JournalId)";
                try
                {
                    var result = db.Execute(insertQuery, remarkModel);
                    return remarkModel;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public Remark GetRemarkTextByJournalIdAndStudentId(string journalId,string studentId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Remarks Where JournalId = @journalId AND StudentId = @studentId";
                try
                {
                    var result = db.Query<Remark>(insertQuery, new { journalId = journalId, studentId= studentId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public void UpdateRemark(Remark remark)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE Remarks Set JournalId = @JournalId,StudentId=@StudentId,RemarkText=@RemarkText Where Id = @Id";
                    var result = db.Execute(insertQuery, remark);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public bool DeleteRemarkById(string journalId, string studentId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM Remarks Where JournalId = @journalId AND StudentId = @studentId";
                    var res = db.Execute(insertQuery, new { journalId = journalId, studentId = studentId });
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