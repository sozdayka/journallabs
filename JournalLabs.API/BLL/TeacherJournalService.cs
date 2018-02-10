using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;
using JournalLabs.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.BLL
{
    public class TeacherJournalService
    {
        private TeacherJournalRepository _teacherJournalRepository;
        private UserRepository _userRepository;
        public TeacherJournalService()
        {
            _teacherJournalRepository = new TeacherJournalRepository();
            _userRepository = new UserRepository();
        }
        public void AddTeacherToJournal(TeacherJournal userModel)
        {
            userModel.Id = Guid.NewGuid();
            _teacherJournalRepository.AddTeacherToJournal(userModel);
        }

        public bool DeleteTeacherFromJournal(string id)
        {
            return _teacherJournalRepository.DeleteTeacherFromJournal(id);
        }
        public List<AssistantsJournalViewModel> GetAllJournalAssistants(string journalId)
        {
            var allAssistants = _userRepository.GetAllAssistants();
            var assistanstsByCurrentJournal = _teacherJournalRepository.GetAllTeacherJournalsByJournalId(journalId);
            var returnJournalAssistants = new List<AssistantsJournalViewModel>();
            foreach (var assistant in allAssistants)
            {
                var assistantJournal = new AssistantsJournalViewModel { Id = assistant.Id, Name = assistant.Login, IsAllowAccess = false};
                var recordTeacherJournal = assistanstsByCurrentJournal.Where(x => x.TeacherId == assistant.Id).FirstOrDefault();
                if (recordTeacherJournal!=null)
                {
                    assistantJournal.TeacherJournalId = recordTeacherJournal.Id;
                    assistantJournal.IsAllowAccess = true;
                }
                returnJournalAssistants.Add(assistantJournal);
            }
            return returnJournalAssistants;
        }
    }
}