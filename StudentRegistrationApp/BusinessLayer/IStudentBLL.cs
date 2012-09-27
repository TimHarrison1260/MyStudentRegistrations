using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.BusinessLayer
{
    public interface IStudentBLL
    {
        List<Model.Student> GetStudents();

        void AddStudent(Model.Student student);

        Model.Student GetStudent(string Firstname, string Surname);

        Model.Student FirstStudent();
        Model.Student LastStudent();
        Model.Student NextStudent();
        Model.Student PreviousStudent();
    }
}
