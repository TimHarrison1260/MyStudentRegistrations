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

        /*
         * Read operations
         */
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


        public List<BusinessEntities.Student> SearchStudents(string q, string category)
        {
            List<BusinessEntities.Student> students = new List<BusinessEntities.Student>();
            Helpers helper = new Helpers();
            foreach (var student in _repository.SearchStudents(q, category))
            {
                students.Add(helper.ToBusinessEntity(student));
            }
            return students;
        }



        /*  
         *  C(R)UD Operations
         */

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


        /*
         * Naviagtion operations
         */
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

        /*
         * Persistence operations
         */

        public void LoadStudents()
        {
            _repository.LoadStudents();
        }

        public void PersistStudents()
        {
            _repository.PersistStudents();
        }





    }
}
