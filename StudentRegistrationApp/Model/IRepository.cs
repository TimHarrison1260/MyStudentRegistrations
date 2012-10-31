using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.Model
{
    public interface IRepository
    {
        /// <summary>
        /// Add a <see cref="Model.Student"/> to the database.
        /// </summary>
        /// <param name="student">The student to be added</param>
        void AddStudent(Student student);

        /// <summary>
        /// Update the details of the supplied student in the
        /// data store.
        /// </summary>
        /// <param name="student"></param>
        void UpdateStudent(Student student);

        /// <summary>
        /// Delete the Student from the datastore.
        /// </summary>
        /// <param name="student">The <see cref="Model.Student"/> to be deleted.</param>
        void DeleteStudent(Student student);

        /// <summary>
        /// Returns a List of Student records.
        /// </summary>
        /// <returns>List(Student) records</returns>
        List<Student> GetStudents();

        /// <summary>
        /// Returns a specific student based on the 'id' passed
        /// </summary>
        /// <param name="Firstname">The id of the student to return (Firstname)</param>
        /// <param name="Surname">The id of the student to return (Surname)</param>
        /// <returns>The student corresponing to the id.</returns>
        Student GetStudent(string Firstname, string Surname);

        Student GetStudentById(long id);

        /// <summary>
        /// Returns a list of students that match the search string.
        /// The search string is contained in either the FirstName
        /// or Surname fields.
        /// </summary>
        /// <param name="q">The search string</param>
        /// <returns>A list of matching student records.</returns>
        List<Student> SearchStudents(string q, string category);

        /// <summary>
        /// Gets the first student in the List.
        /// </summary>
        /// <returns></returns>
        Student FirstStudent();

        /// <summary>
        /// Gets the last student in the list.
        /// </summary>
        /// <returns></returns>
        Student LastStudent();

        /// <summary>
        /// Gets the next student in the list.
        /// </summary>
        /// <returns></returns>
        Student NextStudent();

        /// <summary>
        /// Gets the previous student in the list.
        /// </summary>
        /// <returns></returns>
        Student PreviousStudent();

        void LoadStudents();

        void PersistStudents();

    }
}
