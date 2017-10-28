using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;

namespace JournalLabs.API.BLL
{
    public class JournalService
    {
        private JournalRepository _journalRepository;
        public JournalService()
        {
            _journalRepository = new JournalRepository();
        }
        public void CreateJournal(Journal journalModel)
        {
            _journalRepository.CreateJournal(journalModel);
        }

        public void UpdateJournal(Journal journalModel)
        {
            _journalRepository.UpdateJournal(journalModel);
        }

        public Journal GetJournalById(string journalId)
        {
            return _journalRepository.GetJournalById(journalId);
        }
        public bool DeleteJournalById(string id)
        {
            return _journalRepository.DeleteJournalById(id);
        }
    }
}