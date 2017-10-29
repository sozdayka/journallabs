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
    public class StudentRepository
    {
        private string _connectionString;

        public StudentRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void CreateStudent(Student studentModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[Students]([Id],[FirstDate],[FirstMark],[SecondDate],[SecondMark],[UserFIO],[LessonName],[JournalId]) VALUES (@Id,@FirstDate,@FirstMark,@SecondDate,@SecondMark,@UserFIO,@LessonName,@JournalId)";
                try
                {
                    var result = db.Execute(insertQuery, studentModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateStudent(Student studentModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE Students Set FirstDate = @FirstDate,FirstMark = @FirstMark,SecondDate = @SecondDate,SecondMark = @SecondMark,UserFIO = @UserFIO,LessonName=@LessonName,JournalId = @JournalId Where Id = @Id";
                    var result = db.Execute(insertQuery, studentModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public Student GetStudentById(string studentId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Students Where Id = @studentId";
                try
                {
                    var result = db.Query<Student>(insertQuery, new { studentId = studentId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteStudentById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM Students Where Id = @Id";
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