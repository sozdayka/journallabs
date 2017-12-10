﻿using System;
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
        private RemarkRepository _remarkRepository;
        public JournalService()
        {
            _journalRepository = new JournalRepository();
            _labBlockRepository=new LabBlockRepository();
            _studentRepository = new StudentRepository();
            _kindOfWorkRepository = new KindOfWorkRepository();
            _remarkRepository = new RemarkRepository();
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
                        StudentName = $"Студент {i+1}"
                    });               
                
                for (int j = 0; j < labBlocksCount; j++)
                {
                    if (kindOfWorkGuidList.Count != labBlocksCount)
                    {
                        var kindOfWork = Guid.NewGuid();
                        kindOfWorkGuidList.Add(kindOfWork);
                        _kindOfWorkRepository.CreateKindOfWork(new KindOfWork(){Id = kindOfWork,NameKindOfWork = $"Вид работы {j+1}"});
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
                _remarkRepository.CreateRemark(new Remark() { JournalId = journalId, StudentId = studentId, RemarkText = "" });
            }
            
            
        }

        public void UpdateJournal(Journal journalModel)
        {
            _journalRepository.UpdateJournal(journalModel);
        }

        public JournalGridViewModel GetJournalById(string journalId,string student_Id="")
        {
            List<Student> students = new List<Student>();
            var journal = new JournalGridViewModel();
            journal.JournalModel = _journalRepository.GetJournalById(journalId);
            journal.KindsOfWorkForJournal = _kindOfWorkRepository.GetKindsOfWorkByJournalId(journalId);
            if (student_Id == "")
            {
                students = _labBlockRepository.GetStudentsByJournalId(journalId);
            }
            if (student_Id!="")
            {
                var student =_studentRepository.GetStudentById(student_Id);
                students.Add(student); 
            }
            journal.StudentResultForJournal = new List<StudentLabBlocksViewModel>();
            foreach (var student in students)
            {
                string studentId = student.Id.ToString();
                journal.StudentResultForJournal.Add(new StudentLabBlocksViewModel()
                {
                    StudentInfo = student,
                    StudentLabBlocks = _labBlockRepository.GetLabBlockByStudentAndJournalId(studentId, journalId),
                    Remark = 
                    _remarkRepository.GetRemarkTextByJournalIdAndStudentId(journalId, studentId)
                });
            }
            return journal;
        }

        public List<JournalViewModel> GetAllJournalsByTeacherId(string teacherId)
        {
            return _journalRepository.GetAllJournalsByTeacherId(teacherId);
        }
        public List<StudentJournal> GetAllStudentJournalsByStudentName(string studentName)
        {
            return _journalRepository.GetAllStudentJournalsByStudentName(studentName);
        }
        public bool DeleteJournalById(string id)
        {
            return _journalRepository.DeleteJournalById(id);
        }
    }
}