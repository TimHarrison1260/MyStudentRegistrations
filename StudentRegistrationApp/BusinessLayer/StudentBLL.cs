using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.BusinessLayer
{

    public class StudentBLL : IStudentBLL
    {
        private Model.IRepository _repository;

        public StudentBLL(Model.IRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException();
            _repository = repository;
        }

        public List<Model.Student> GetStudents()
        {
            return _repository.GetStudents();
        }


        public void AddStudent(Model.Student student)
        {
            _repository.AddStudent(student);
        }

        public Model.Student GetStudent(string Firstname, string Surname)
        {
            return _repository.GetStudent(Firstname, Surname);
        }

        public Model.Student FirstStudent()
        {
            return _repository.FirstStudent();
        }

        public Model.Student LastStudent()
        {
            return _repository.LastStudent();
        }

        public Model.Student NextStudent()
        {
            return _repository.NextStudent();
        }

        public Model.Student PreviousStudent()
        {
            return _repository.PreviousStudent();
        }

    }
}
