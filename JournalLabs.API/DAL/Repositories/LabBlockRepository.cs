using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using JournalLabs.API.Models;
using JournalLabs.API.ViewModels;

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
        public List<Student> GetStudentsByJournalId(string journalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT distinct s.* From LabBlocks lb
	                                    left join Students s on s.Id=lb.StudentId
                                            where lb.JournalId=@journalId";
                try
                {
                    var result = db.Query<Student>(insertQuery, new { journalId = journalId });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public List<LabBlockViewModel> GetLabBlockByStudentAndJournalId(string studentId,string journalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"	SELECT * From LabBlocks Where StudentId = @studentId And JournalId=@journalId";
                try
                {
                    var result = db.Query<LabBlockViewModel>(insertQuery, new { studentId = studentId, journalId= journalId });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public List<StudentJournalViewModel> GetStudentJournalViewModels( string journalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"select st.*,lb.* from (SELECT distinct s.* From LabBlocks lb
	                                    left join Students s on s.Id=lb.StudentId
                                            where lb.JournalId=@journalId) as st
	                                    right join LabBlocks lb on st.Id=lb.StudentId";
                try
                {
                    var result = db.Query<StudentJournalViewModel, Student, List<LabBlockViewModel>, StudentJournalViewModel>(insertQuery, (sv, st, lb) =>
                    new StudentJournalViewModel(){
                        StudentInfo = st,
                        StudentLabBlocks = lb
                    }, new { journalId = journalId }, splitOn: "StudentId,JournalId,Id,journalId");
                    return result.ToList();
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