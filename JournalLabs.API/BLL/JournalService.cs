using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Dapper;
using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;
using JournalLabs.API.Models.Enums;
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
        private TeacherJournalRepository _teacherJournalRepository;
        private UserRepository _userRepository;
        public JournalService()
        {
            _journalRepository = new JournalRepository();
            _labBlockRepository=new LabBlockRepository();
            _studentRepository = new StudentRepository();
            _kindOfWorkRepository = new KindOfWorkRepository();
            _remarkRepository = new RemarkRepository();
            _teacherJournalRepository = new TeacherJournalRepository();
            _userRepository = new UserRepository();
        }
        public void CreateJournal(CreateJournalViewModel createJournalViewModel)
        {
            var kindOfWorkGuidList = new List<Guid>();
            var journalId = Guid.NewGuid();

            Journal journal = new Journal();
            journal.Id = journalId;
            journal.LessonName = createJournalViewModel.LessonName;

            _journalRepository.CreateJournal(journal);

            for (int i = 0; i < createJournalViewModel.TeacherIds.Count; i++)
            {
                _teacherJournalRepository.AddTeacherToJournal(new TeacherJournal {
                    Id =Guid.NewGuid(),
                    JournalId = journalId,
                    TeacherId = createJournalViewModel.TeacherIds[i]
                });
            }

            foreach(Student student in createJournalViewModel.Students)
            {
                CreateStudentInJournal(journalId, student.StudentName, kindOfWorkGuidList, createJournalViewModel.LabBlocksSettings);
            }
            
            
        }
        public void CreateStudentInJournal(Guid journalId,string studentName, List<Guid> kindOfWorkGuidList, List<LabBlockViewModel> labBlocksSettings)
        {
            var studentId = Guid.NewGuid();
            _studentRepository.CreateStudent(
                new Student()
                {
                    Id = studentId,
                    StudentName = studentName
                });
            var j = 0;
            foreach (var labBlock in labBlocksSettings)
            {
                if (kindOfWorkGuidList.Count != labBlocksSettings.Count)
                {
                    var kindOfWork = Guid.NewGuid();
                    kindOfWorkGuidList.Add(kindOfWork);
                    _kindOfWorkRepository.CreateKindOfWork(new KindOfWork() {
                        Id = kindOfWork, NameKindOfWork = $"Вид работы {j + 1}",
                        IsKindOfWorkVisible = labBlock.IsKindOfWorkVisible,
                        IsVisibleToStudent= labBlock.IsVisibleToStudent,
                        IsSecondBlock = labBlock.IsSecondBlock
                    });
                }
                var createLabBlock = new LabBlock();
                createLabBlock.IsBoolField = labBlock.IsBoolField;
                createLabBlock.IsCalculateMark = labBlock.IsCalculateMark;
                createLabBlock.Color = "";
                createLabBlock.KindOfMark = KindOfMark.FirstMark;
                createLabBlock.Id = Guid.NewGuid();
                createLabBlock.JournalId = journalId;
                createLabBlock.StudentId = studentId;
                createLabBlock.KindOfWorkId = kindOfWorkGuidList[j];

                _labBlockRepository.CreateLabBlock(createLabBlock);

                if (labBlock.IsSecondBlock)
                {
                    createLabBlock.KindOfMark = KindOfMark.SecondMark;
                    createLabBlock.Id = Guid.NewGuid();

                    _labBlockRepository.CreateLabBlock(createLabBlock);
                }
                j++;
            }
            _remarkRepository.CreateRemark(new Remark() { JournalId = journalId, StudentId = studentId, RemarkText = "", IsHideStudent = false });
        }
        public void UpdateJournal(Journal journalModel)
        {
            _journalRepository.UpdateJournal(journalModel);
        }

        public JournalGridViewModel GetJournalById(string journalId, bool isTeacher,string student_Id="")
        {
            List<Student> students = new List<Student>();
            var journal = new JournalGridViewModel();
            journal.JournalModel = _journalRepository.GetJournalById(journalId);
            var kindOfWorks = _kindOfWorkRepository.GetKindsOfWorkByJournalId(journalId);
            journal.KindsOfWorkForJournal = isTeacher? kindOfWorks: kindOfWorks.Where(x=>x.IsKindOfWorkVisible==true).ToList();
                      
            if (student_Id == "")
            {
                students = _labBlockRepository.GetStudentsByJournalId(journalId);
            }
            if (student_Id!="")
            {
                var student =_studentRepository.GetStudentById(student_Id);
                students.Add(student);
                journal.KindsOfWorkForJournal = kindOfWorks.Where(x => x.IsVisibleToStudent == true).ToList();
            }
            if (journal.KindsOfWorkForJournal.Count == 0)
            {
                return new JournalGridViewModel();
            }
            var kindOfWorkVisibleQueryString = StringForKindOfWorkBulkQuery(journal.KindsOfWorkForJournal.Select(x => x.Id).ToList());
            journal.StudentResultForJournal = new List<StudentLabBlocksViewModel>();
            foreach (var student in students)
            {
                string studentId = student.Id.ToString();
                var remark = _remarkRepository.GetRemarkTextByJournalIdAndStudentId(journalId, studentId);
                if (remark.IsHideStudent==true && isTeacher==false)
                {
                    continue;
                }
                var labBlocks = _labBlockRepository.GetLabBlockByStudentAndJournalId(studentId, journalId, kindOfWorkVisibleQueryString);
                for (int i = 0; i < labBlocks.Count; i++)
                {
                    if (i+1< labBlocks.Count&&labBlocks[i].KindOfWorkId== labBlocks[i+1].KindOfWorkId &&
                        labBlocks[i].KindOfMark == KindOfMark.SecondMark&& labBlocks[i + 1].KindOfMark== KindOfMark.FirstMark)
                    {
                        var temp = labBlocks[i];
                        labBlocks[i] = labBlocks[i+1];
                        labBlocks[i + 1] = temp;
                    }
                    if (labBlocks[i].MarkTeacherId!=null)
                    {
                        labBlocks[i].MarkTeacherName = _userRepository.GetUserById(labBlocks[i].MarkTeacherId.ToString()).Login;
                    }                   
                }
                

                journal.StudentResultForJournal.Add(new StudentLabBlocksViewModel()
                {
                    StudentInfo = student,
                    StudentLabBlocks = labBlocks,
                    Remark = remark
                });
            }
            return journal;
        }
        public bool RemoveStudentFromJournal(string id)
        {
            return _journalRepository.DeleteJournalById(id);
        }
        public List<JournalViewModel> GetAllJournalsByTeacherId(string teacherId)
        {
            return _journalRepository.GetAllJournalsByTeacherId(teacherId);
        }
        public List<JournalViewModel> GetAllJournalsByAssistantId(string teacherId)
        {
            return _journalRepository.GetAllJournalsByAssistantId(teacherId);
        }
        public List<StudentJournal> GetAllStudentJournalsByStudentName(string studentName)
        {
            return _journalRepository.GetAllStudentJournalsByStudentName(studentName);
        }
        public bool DeleteJournalById(string id)
        {
            return _journalRepository.DeleteJournalById(id);
        }
        public void AddStudentToJournal(AddStudentToJournalViewModel studentToJournalModel)
        {
            
            var kindOfWorks = _kindOfWorkRepository.GetKindsOfWorkByJournalId(studentToJournalModel.JournalId).Select(x=>x.Id).ToList();
            foreach (Student student in studentToJournalModel.Students)
            {
                var kindOfWorkVisibleQueryString = StringForKindOfWorkBulkQuery(kindOfWorks);
                var labBlocksSettings = _labBlockRepository.GetLabBlockByStudentAndJournalId(student.Id.ToString(), studentToJournalModel.JournalId, kindOfWorkVisibleQueryString);
                var labBlocksSettingsResult = new List<LabBlockViewModel>();
                for (int i = 0; i < labBlocksSettings.Count; i++)
                {
                    if (i + 1 == labBlocksSettings.Count || labBlocksSettings[i].KindOfWorkId != labBlocksSettings[i + 1].KindOfWorkId)
                    {
                        labBlocksSettingsResult.Add(labBlocksSettings[i]);
                        continue;
                    }
                    if (i + 1 != labBlocksSettings.Count && labBlocksSettings[i].KindOfWorkId == labBlocksSettings[i + 1].KindOfWorkId)
                    {
                        labBlocksSettings[i].IsSecondBlock = true;
                        labBlocksSettingsResult.Add(labBlocksSettings[i]);
                        i++;
                    }
                }
                CreateStudentInJournal(Guid.Parse(studentToJournalModel.JournalId), student.StudentName, kindOfWorks, labBlocksSettingsResult);
            }
            
        }

        public void AddKindOfWorkToJournal(string journalId)
        {
            var students = _labBlockRepository.GetStudentsByJournalId(journalId);
            var lastKindOfWork = _kindOfWorkRepository.GetKindsOfWorkByJournalId(journalId).LastOrDefault();
            var kindOfWorkIndex = int.Parse(Regex.Match(lastKindOfWork.NameKindOfWork, @"\d+").Value);
            var kindOfWorkName = $"Вид работы {kindOfWorkIndex + 1}";
            var kindOfWorkId = Guid.NewGuid();
                _kindOfWorkRepository.CreateKindOfWork(new KindOfWork() { Id = kindOfWorkId,
                    NameKindOfWork = kindOfWorkName,
                    IsKindOfWorkVisible = true,
                    IsVisibleToStudent=true});
            for (int i = 0; i < students.Count; i++)
            {
                var createLabBlock = new LabBlock();
                createLabBlock.IsBoolField = false;
                createLabBlock.IsCalculateMark = true;
                createLabBlock.Color = "";
                createLabBlock.KindOfMark = KindOfMark.FirstMark;
                createLabBlock.Id = Guid.NewGuid();
                createLabBlock.JournalId = new Guid(journalId);
                createLabBlock.StudentId = students[i].Id;
                createLabBlock.KindOfWorkId = kindOfWorkId;

                _labBlockRepository.CreateLabBlock(createLabBlock);
            }
            
        }

            public string StringForKindOfWorkBulkQuery(List<Guid> kindOfWorks)
        {
            StringBuilder queryString = new StringBuilder();
            for (int i = 0; i < kindOfWorks.Count; i++)
            {
                queryString.Append($"'{kindOfWorks[i]}'");
                if (i+1<kindOfWorks.Count)
                {
                    queryString.Append(",");
                }
            }
            return queryString.ToString();
        }
    }
}