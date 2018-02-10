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
                string insertQuery = @"INSERT INTO [dbo].[LabBlocks]([Id],[Date],[Mark],[MarkTeacherId],[KindOfMark],[IsKindOfWorkVisible],[IsCalculateMark],[IsVisibleToStudent],[IsBoolField],[StudentId],[KindOfWorkId],[JournalId],[Color])
                                                             VALUES (@Id,@Date,@Mark,@MarkTeacherId,@KindOfMark,@IsKindOfWorkVisible,@IsCalculateMark,@IsVisibleToStudent,@IsBoolField,@StudentId,@KindOfWorkId,@JournalId,@Color)";
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
                    string insertQuery = @"UPDATE LabBlocks Set Date = @Date,Mark = @Mark,MarkTeacherId = @MarkTeacherId,KindOfMark = @KindOfMark,IsKindOfWorkVisible = @IsKindOfWorkVisible,IsCalculateMark=@IsCalculateMark,IsVisibleToStudent = @IsVisibleToStudent,IsBoolField = @IsBoolField,StudentId = @StudentId,KindOfWorkId=@KindOfWorkId,JournalId = @JournalId,Color=@Color Where Id = @Id";
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
                string insertQuery = @" select  s.* from (SELECT distinct s.* From LabBlocks lb left join Students s on s.Id=lb.StudentId
                                            where lb.JournalId=@journalId)as s order by 
											s.StudentName"; //len(s.StudentName),
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
            ///------------review order by
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"select  lb.* from (
                                            SELECT lb.*,kw.NameKindOfWork From LabBlocks lb 
                                            left join KindOfWorks kw on lb.KindOfWorkId=kw.Id 
                                            Where StudentId = @studentId And JournalId=@journalId)as lb order by 
										    lb.NameKindOfWork"; //len(lb.NameKindOfWork),
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
        public List<StudentLabBlocksViewModel> GetStudentJournalViewModels( string journalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"select st.*,lb.* from (SELECT distinct s.* From LabBlocks lb
	                                    left join Students s on s.Id=lb.StudentId
                                            where lb.JournalId=@journalId) as st
	                                    right join LabBlocks lb on st.Id=lb.StudentId";
                try
                {
                    var result = db.Query<StudentLabBlocksViewModel, Student, List<LabBlockViewModel>, StudentLabBlocksViewModel>(insertQuery, (sv, st, lb) =>
                    new StudentLabBlocksViewModel(){
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