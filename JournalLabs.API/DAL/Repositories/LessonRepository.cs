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
    public class LessonRepository
    {
        private string _connectionString;

        public LessonRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void CreateLesson(Lesson lessonModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[Lessons]([Id],[LessonName]) VALUES (@Id,@LessonName)";
                try
                {
                    var result = db.Execute(insertQuery, lessonModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateLesson(Lesson lessonModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE Lessons Set LessonName = @LessonName Where Id = @Id"; 
                    var result = db.Execute(insertQuery, lessonModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public Lesson GetLessonById(string lessonId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Lessons Where Id = @lessonId";
                try
                {
                    var result = db.Query<Lesson>(insertQuery, new { lessonId = lessonId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteLessonById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM Lessons Where Id = @Id";
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