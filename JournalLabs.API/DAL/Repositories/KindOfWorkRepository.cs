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
    public class KindOfWorkRepository
    {
        private string _connectionString;

        public KindOfWorkRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void CreateKindOfWork(KindOfWork kindOfWorkModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[KindOfWorks]([Id],[NameKindOfWork]) VALUES (@Id,@NameKindOfWork)";
                try
                {
                    var result = db.Execute(insertQuery, kindOfWorkModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateKindOfWork(KindOfWork kindOfWorkModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE KindOfWorks Set NameKindOfWork = @NameKindOfWork Where Id = @Id";
                    var result = db.Execute(insertQuery, kindOfWorkModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public KindOfWork GetKindOfWorkById(string kindOfWorkId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From KindOfWorks Where Id = @kindOfWorkId"; 
                try
                {
                    var result = db.Query<KindOfWork>(insertQuery, new { kindOfWorkId = kindOfWorkId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public List<KindOfWork> GetKindsOfWorkByJournalId(string journalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"	select distinct kw.Id,kw.NameKindOfWork from Journals j 
	                                    left join LabBlocks lb on j.Id=lb.JournalId
	                                    left join KindOfWorks kw on lb.KindOfWorkId=kw.Id 
                                        where j.Id=@journalId";
                try
                {
                    var result = db.Query<KindOfWork>(insertQuery, new { journalId = journalId });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteKindOfWorkById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM KindOfWorks Where Id = @Id";
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