using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.BusinessLayer
{
    public interface IStudentBLL
    {
        List<BusinessEntities.Student> GetStudents();

        void AddStudent(BusinessEntities.Student student);

        BusinessEntities.Student GetStudent(string Firstname, string Surname);

        BusinessEntities.Student FirstStudent();
        BusinessEntities.Student LastStudent();
        BusinessEntities.Student NextStudent();
        BusinessEntities.Student PreviousStudent();
    }
}
