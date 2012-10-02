using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentRegistrationApp.Model;

namespace StudentRegistrationApp.BusinessLayer
{

    public class StudentBLL : IStudentBLL
    {
        private Model.IRepository _repository;

        /// <summary>
        /// Constructor to inject the instance of the repository
        /// </summary>
        /// <param name="repository"></param>
        public StudentBLL(Model.IRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException();
            _repository = repository;
        }

        public List<BusinessEntities.Student> GetStudents()
        {
            List<BusinessEntities.Student> students = new List<BusinessEntities.Student>();
            Helpers helper = new Helpers();
            foreach (var student in _repository.GetStudents())
            {
                students.Add(helper.ToBusinessEntity(student));
            }
            return students;
        }

        
        public void AddStudent(BusinessEntities.Student student)
        {
            Helpers helper = new Helpers();
            _repository.AddStudent(helper.ToModel(student));
        }

        public void UpdateStudent(BusinessEntities.Student student)
        {
            Helpers helper = new Helpers();
            _repository.UpdateStudent(helper.ToModel(student));
        }


        public void DeleteStudent(BusinessEntities.Student student)
        {
            Helpers helper = new Helpers();
            _repository.DeleteStudent(helper.ToModel(student));
        }


        public BusinessEntities.Student GetStudent(string Firstname, string Surname)
        {
            Helpers helper = new Helpers();
            return helper.ToBusinessEntity(_repository.GetStudent(Firstname, Surname));
        }

        public BusinessEntities.Student GetStudentById(long id)
        {
            Helpers helper = new Helpers();
            return helper.ToBusinessEntity(_repository.GetStudentById(id));
        }

        public BusinessEntities.Student FirstStudent()
        {
            Helpers helper = new Helpers();
            return helper.ToBusinessEntity(_repository.FirstStudent());
        }

        public BusinessEntities.Student LastStudent()
        {
            Helpers helper = new Helpers();
            return helper.ToBusinessEntity( _repository.LastStudent());
        }

        public BusinessEntities.Student NextStudent()
        {
            Helpers helper = new Helpers();
            return helper.ToBusinessEntity(_repository.NextStudent());
        }

        public BusinessEntities.Student PreviousStudent()
        {
            Helpers helper = new Helpers();
            return helper.ToBusinessEntity(_repository.PreviousStudent());
        }

    }
}
