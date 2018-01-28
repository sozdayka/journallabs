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
    public class TeacherJournalRepository
    {
        private string _connectionString;

        public TeacherJournalRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void AddTeacherToJournal(TeacherJournal teacherJournalModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[TeacherJournals]([Id],[TeacherId],[JournalId]) VALUES (@Id,@TeacherId,@JournalId)";
                try
                {
                    var result = db.Execute(insertQuery, teacherJournalModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateTeacherJournal(TeacherJournal teacherJournalModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE TeacherJournals Set TeacherId = @TeacherId,JournalId=@JournalId Where Id = @Id";
                    var result = db.Execute(insertQuery, teacherJournalModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public TeacherJournal GetTeacherJournalById(string teacherJournalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From TeacherJournals Where Id = @teacherJournalId";
                try
                {
                    var result = db.Query<TeacherJournal>(insertQuery, new { teacherJournalId = teacherJournalId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteTeacherFromJournal(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM TeacherJournals Where Id = @Id";
                    var res = db.Execute(insertQuery, new { Id = id });
                    return res > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public List<TeacherJournal> GetAllTeacherJournalsByJournalId(string journalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string selectQuery = @"SELECT * From TeacherJournals Where JournalId = @journalId";
                try
                {
                    var result = db.Query<TeacherJournal>(selectQuery, new { journalId = journalId });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        //public List<TeacherJournal> GetAllStudentTeacherJournalsByStudentName(string studentName)
        //{
        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        string selectQuery = @"  select distinct lb.TeacherJournalId, s.StudentId, j.LessonName  from 
        //                                (SELECT Id as StudentId From Students Where StudentName = @studentName) s
        //                                 left join LabBlocks lb on lb.StudentId = s.StudentId
        //                                 inner join TeacherJournals j on j.Id= lb.TeacherJournalId";
        //        try
        //        {
        //            var result = db.Query<TeacherJournal>(selectQuery, new { studentName = studentName });
        //            return result.ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}