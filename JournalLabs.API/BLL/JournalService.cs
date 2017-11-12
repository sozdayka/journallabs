using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;
using JournalLabs.API.ViewModels;

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
            var kindOfWorkGuidList = new List<Guid>();
            var journalId = Guid.NewGuid();

            Journal journal = new Journal();
            journal.Id = journalId;
            journal.TeacherId = teacherId;
            journal.LessonName = lessonName;

            _journalRepository.CreateJournal(journal);
            

            for (int i = 0; i < studentsCount; i++)
            {
                var studentId = Guid.NewGuid();
                _studentRepository.CreateStudent(
                    new Student()
                    {
                        Id = studentId,
                        StudentName = $"UserName {i+1}"
                    });               
                
                for (int j = 0; j < labBlocksCount; j++)
                {
                    if (kindOfWorkGuidList.Count != labBlocksCount)
                    {
                        var kindOfWork = Guid.NewGuid();
                        kindOfWorkGuidList.Add(kindOfWork);
                        _kindOfWorkRepository.CreateKindOfWork(new KindOfWork(){Id = kindOfWork,NameKindOfWork = $"Lab block{j+1}"});
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

        public JournalGridViewModel GetJournalById(string journalId)
        {
            var journal = new JournalGridViewModel();
            journal.JournalModel = _journalRepository.GetJournalById(journalId);
            journal.KindsOfWorkForJournal = _kindOfWorkRepository.GetKindsOfWorkByJournalId(journalId);
            var students = _labBlockRepository.GetStudentsByJournalId(journalId);
            journal.StudentResultForJournal = new List<StudentJournalViewModel>();
            foreach (var student in students)
            {
                string studentId = student.Id.ToString();
                journal.StudentResultForJournal.Add(new StudentJournalViewModel()
                {
                    StudentInfo = student,
                    StudentLabBlocks = _labBlockRepository.GetLabBlockByStudentAndJournalId(studentId, journalId)
                });
            }
            //journal.StudentResultForJournal = _labBlockRepository.GetStudentJournalViewModels(journalId);
            return journal;
        }
        public bool DeleteJournalById(string id)
        {
            return _journalRepository.DeleteJournalById(id);
        }
    }
}