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
    public class JournalRepository
    {
        private string _connectionString;

        public JournalRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void CreateJournal(Journal journalModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[Journals]([Id],[LessonName],[IsExam],[GroupName]) VALUES (@Id,@LessonName,@IsExam,@GroupName)";
                try
                {
                    var result = db.Execute(insertQuery, journalModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateJournal(Journal journalModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE Journals Set LessonName = @LessonName,IsExam=@IsExam,GroupName=@GroupName Where Id = @Id";
                    var result = db.Execute(insertQuery, journalModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public JournalViewModel GetJournalById(string journalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Journals Where Id = @journalId";
                try
                {
                    var result = db.Query<JournalViewModel>(insertQuery, new { journalId = journalId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteJournalById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM Journals Where Id = @Id";
                    var res = db.Execute(insertQuery, new { Id = id });
                    return res > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public List<JournalViewModel> GetAllJournalsByTeacherId(string teacherId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string selectQuery = @"Select tj.TeacherId,lb.Id,lb.LessonName,lb.IsExam,lb.GroupName From (SELECT * From TeacherJournals Where TeacherId = @teacherId) as tj
                                        inner join Journals lb on lb.Id = tj.JournalId";
                try
                {
                    var result = db.Query<JournalViewModel>(selectQuery, new { teacherId = teacherId });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public List<JournalViewModel> GetAllJournalsByAssistantId(string teacherId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string selectQuery = @"Select distinct tj.TeacherId,j.Id,j.LessonName,j.IsExam,j.GroupName From (SELECT * From TeacherJournals Where TeacherId = @teacherId) as tj
                                        inner join Journals j on j.Id = tj.JournalId
                                        inner join LabBlocks lb on lb.JournalId = j.Id
                                        inner join KindOfWorks kw on lb.KindOfWorkId = kw.Id and kw.IsKindOfWorkVisible=1";
                try
                {
                    var result = db.Query<JournalViewModel>(selectQuery, new { teacherId = teacherId });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public List<StudentJournal> GetAllStudentJournalsByStudentName(string studentName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string selectQuery = @"  select distinct lb.JournalId, s.StudentId, j.LessonName,j.IsExam,j.GroupName  from 
                                        (SELECT Id as StudentId From Students Where StudentName = @studentName) s
                                         left join LabBlocks lb on lb.StudentId = s.StudentId
                                         inner join KindOfWorks kw on lb.KindOfWorkId = kw.Id and kw.IsVisibleToStudent=1
                                         inner join Journals j on j.Id= lb.JournalId
                                         inner join Remarks r on r.StudentId=s.StudentId and r.IsHideStudent=0";
                try
                {
                    var result = db.Query<StudentJournal>(selectQuery, new { studentName = studentName });
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