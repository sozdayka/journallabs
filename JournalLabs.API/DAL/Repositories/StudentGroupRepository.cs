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
    public class StudentGroupRepository
    {
        private string _connectionString;

        public StudentGroupRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void CreateStudentGroup(StudentGroup studentGroupModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[StudentGroups]([Id],[StudentId],[GroupId]) VALUES (@Id,@StudentId,@GroupId)";
                try
                {
                    var result = db.Execute(insertQuery, studentGroupModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateStudentGroup(StudentGroup studentGroupModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE StudentGroups Set StudentId = @StudentId,GroupId=@GroupId Where Id = @Id";
                    var result = db.Execute(insertQuery, studentGroupModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public StudentGroup GetStudentGroupById(string studentGroupId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From StudentGroups Where Id = @studentGroupId";
                try
                {
                    var result = db.Query<StudentGroup>(insertQuery, new { studentGroupId = studentGroupId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteStudentGroupById(string id)
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