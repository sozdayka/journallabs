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
    public class GroupRepository
    {
        private string _connectionString;

        public GroupRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public List<Group> Groups()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Groups ";
                try
                {
                    var result = db.Query<Group>(insertQuery);
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public void CreateGroup(Group groupModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"INSERT INTO [dbo].[Groups]([Id],[Name]) VALUES (@Id,@Name)";
                try
                {
                    var result = db.Execute(insertQuery, groupModel);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void UpdateGroup(Group groupModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"UPDATE Groups Set Name = @Name Where Id = @Id";
                    var result = db.Execute(insertQuery, groupModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public Group GetGroupById(string groupId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Groups Where Id = @groupId";
                try
                {
                    var result = db.Query<Group>(insertQuery, new { groupId = groupId });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool DeleteGroupById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    string insertQuery = @"Delete FROM Groups Where Id = @Id";
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