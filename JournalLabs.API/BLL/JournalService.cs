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
        private LabBlockRepository _labBlockRepository;
        private StudentRepository _studentRepository;
        private KindOfWorkRepository _kindOfWorkRepository;
        public JournalService()
        {
            _journalRepository = new JournalRepository();
            _labBlockRepository=new LabBlockRepository();
            _studentRepository = new StudentRepository();
            _kindOfWorkRepository = new KindOfWorkRepository();
        }
        public void CreateJournal(string lessonName, int studentsCount, int labBlocksCount,Guid teacherId)
        {
            var journalId = Guid.NewGuid();
            Journal journal = new Journal();
            journal.Id = journalId;
            journal.TeacherId = teacherId;
            journal.LessonName = lessonName;
            _journalRepository.CreateJournal(journal);
            var kindOfWorkGuidList = new List<Guid>();

            for (int i = 0; i < studentsCount; i++)
            {
                var studentId = Guid.NewGuid();
                _studentRepository.CreateStudent(
                    new Student()
                    {
                        Id = studentId,
                        StudentName = $"UserName {i}"
                    });               
                
                for (int j = 0; j < labBlocksCount; j++)
                {
                    if (kindOfWorkGuidList.Count != labBlocksCount)
                    {
                        var kindOfWork = Guid.NewGuid();
                        kindOfWorkGuidList.Add(kindOfWork);
                        _kindOfWorkRepository.CreateKindOfWork(new KindOfWork(){Id = kindOfWork,NameKindOfWork = $"Lab block{j}"});
                    }
                    _labBlockRepository.CreateLabBlock(
                        new LabBlock()
                        {
                            Id = Guid.NewGuid(),
                            JournalId = journalId,
                            StudentId = studentId,
                            KindOfWorkId = kindOfWorkGuidList[j]
                        });
                }
            }
            
            
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