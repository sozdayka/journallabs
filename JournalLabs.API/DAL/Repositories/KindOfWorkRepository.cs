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
                string insertQuery = @"INSERT INTO [dbo].[KindOfWorks]([Id],[IsKindOfWorkVisible],[IsVisibleToStudent],[NameKindOfWork],[IsSecondBlock]) VALUES (@Id,@IsKindOfWorkVisible,@IsVisibleToStudent,@NameKindOfWork,@IsSecondBlock)";
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
                    string insertQuery = @"UPDATE KindOfWorks Set NameKindOfWork = @NameKindOfWork,IsVisibleToStudent=@IsVisibleToStudent,IsKindOfWorkVisible=@IsKindOfWorkVisible,IsSecondBlock=@IsSecondBlock Where Id = @Id";
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
                string insertQuery = @"	select  kw.* from (select distinct kw.* from Journals j 
	                                    left join LabBlocks lb on j.Id=lb.JournalId
	                                    left join KindOfWorks kw on lb.KindOfWorkId=kw.Id 
                                        where j.Id=@journalId)as kw order by 
									    kw.NameKindOfWork"; //len(kw.NameKindOfWork),
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
        public bool UpdateVisibleKindOfWork(string idKindOfWork, bool isKindOfWorkVisible)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE KindOfWorks Set IsKindOfWorkVisible = @isKindOfWorkVisible Where Id = @idKindOfWork";
                    var res = db.Execute(insertQuery, new { idKindOfWork = idKindOfWork, isKindOfWorkVisible = isKindOfWorkVisible });
                    return res > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool UpdateVisibleKindOfWorkForStudent(string idKindOfWork, bool isKindOfWorkVisibleForStudent)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE KindOfWorks Set IsVisibleToStudent = @isKindOfWorkVisibleForStudent Where Id = @idKindOfWork";
                    var res = db.Execute(insertQuery, new { idKindOfWork = idKindOfWork, isKindOfWorkVisibleForStudent = isKindOfWorkVisibleForStudent });
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