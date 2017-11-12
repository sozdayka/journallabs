﻿using System;
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
                string insertQuery = @"INSERT INTO [dbo].[Journals]([Id],[TeacherId],[LessonName]) VALUES (@Id,@TeacherId,@LessonName)";
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
                    string insertQuery = @"UPDATE Journals Set TeacherId = @TeacherId Where Id = @Id";
                    var result = db.Execute(insertQuery, journalModel);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public Journal GetJournalById(string journalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertQuery = @"SELECT * From Journals Where Id = @journalId";
                try
                {
                    var result = db.Query<Journal>(insertQuery, new { journalId = journalId });
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
    }
}