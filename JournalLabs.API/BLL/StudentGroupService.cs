using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;
using JournalLabs.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.BLL
{
    public class StudentGroupService
    {
        private StudentGroupRepository _studentGroupRepository;
        private StudentRepository _studentRepository;

        public StudentGroupService()
        {
            _studentGroupRepository = new StudentGroupRepository();
            _studentRepository = new StudentRepository();
        }
        //public List<StudentGroup> GetStudentGroups()
        //{
            //return _studentGroupRepository.StudentGroups();
        //}
        public void CreateStudentGroup(StudentGroup studentGroupModel)
        {
            studentGroupModel.Id = Guid.NewGuid();
            _studentGroupRepository.CreateStudentGroup(studentGroupModel);
        }
        public Guid AddStudentToGroup(AddStudentToGroupViewModel addStudentToGroupViewModel)
        {
            addStudentToGroupViewModel.Student.Id = Guid.NewGuid();
            _studentRepository.CreateStudent(addStudentToGroupViewModel.Student);
            var studentGroup = new StudentGroup();
            studentGroup.Id = Guid.NewGuid();
            studentGroup.StudentId = addStudentToGroupViewModel.Student.Id;
            studentGroup.GroupId = addStudentToGroupViewModel.GroupId;
            _studentGroupRepository.CreateStudentGroup(studentGroup);
            return addStudentToGroupViewModel.Student.Id;
        }
        public void UpdateStudentGroup(StudentGroup studentGroupModel)
        {
            _studentGroupRepository.UpdateStudentGroup(studentGroupModel);
        }

        public StudentGroup GetStudentGroupById(string studentGroupId)
        {
            return _studentGroupRepository.GetStudentGroupById(studentGroupId);
        }
        public bool DeleteStudentGroupById(string id)
        {
            return _studentGroupRepository.DeleteStudentGroupById(id);
        }
    }
}