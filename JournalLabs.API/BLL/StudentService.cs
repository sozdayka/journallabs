using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;

namespace JournalLabs.API.BLL
{
    public class StudentService
    {
        private StudentRepository _studentRepository;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
        }

        public void CreateStudent(Student studentModel)
        {
            studentModel.Id = Guid.NewGuid();
            _studentRepository.CreateStudent(studentModel);
        }

        public void UpdateStudent(Student studentModel)
        {
            _studentRepository.UpdateStudent(studentModel);
        }

        public Student GetStudentById(string studentId)
        {
            return _studentRepository.GetStudentById(studentId);
        }
        public bool DeleteStudentById(string id)
        {
            return _studentRepository.DeleteStudentById(id);
        }
    }
}