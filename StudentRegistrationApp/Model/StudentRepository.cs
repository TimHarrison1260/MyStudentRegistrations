using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.Model
{
    /// <summary>
    /// Class <c>StudentRepository</c> is reponsible for managing
    /// access to the <see cref="Model.Student"/> records.
    /// </summary>
    public class StudentRepository : IRepository
    {
        //  The instance of the database (or EF connection) to a physical db.
        private List<Student> db = new List<Student>();
        //  Holds the index of the item currently displayed.
        private int currentItem = 0;

        /// <summary>
        /// Provide constructor so we can initialise the list of
        /// Students to contain some values.
        /// </summary>
        public StudentRepository()
        {
            Student s = new Student()
            {
                Firstname = "Tim",
                Surname = "Harrison",
                DOB = new DateTime(1960, 02, 01),
                Address = new Address() { Address1 = "Cauldstanes", Town = "Farnham", County = "Aberdeenshire", PostCode = "ZZ4 9EE" },
                Course = "MSc Web Development",
                YearOfStudy = YearOfStudyEnum.Second,
                Contact = new ContactDetail() { HomePhone = "", MobilePhone = "", HomeEmail = "", StudentEmail = "" }
            };
            db.Add(s);
            
            s = new Student() 
            {
                Firstname="Nancy",
                Surname="Harrison",
                DOB=new DateTime(1955,09,27),
                Address = new Address() {Address1="Cauldstanes", Town="Farnham", County="Aberdeenshire", PostCode="ZZ4 9EE"}, 
                Course="Database Administration",
                YearOfStudy=YearOfStudyEnum.First,
                Contact = new ContactDetail() { HomePhone = "", MobilePhone = "", HomeEmail = "", StudentEmail = "" }
            };
            db.Add(s);
        }

        /// <summary>
        /// Add a <see cref="Model.Student"/> to the database.
        /// </summary>
        /// <param name="student">The student to be added</param>
        public void AddStudent (Student student) 
        {
            long nameHash = (student.Firstname + student.Surname).GetHashCode();

            db.Add(student);
        }

        /// <summary>
        /// Update the details of the supplied student in the
        /// data store.
        /// </summary>
        /// <param name="student"></param>
        public void UpdateStudent(Student student)
        {
            //  Find the student record as we could
            int idx = db.FindIndex(delegate(Student s)
            {
                return (s.Firstname == student.Firstname &&
                    s.Surname == student.Surname &&
                    s.DOB == student.DOB);
            });

            if (idx != (-1))        //ie. student was found 
            {
                //  Update the student record.
                if (student.Course != null && student.Course != "" && student.Course != db[idx].Course)
                    db[idx].Course = student.Course;

                if (student.YearOfStudy != db[idx].YearOfStudy) db[idx].YearOfStudy = student.YearOfStudy;

                //  Update remaining fields in a similar way............

            }
        }

        /// <summary>
        /// Delete the Student from the datastore.
        /// </summary>
        /// <param name="student">The <see cref="Model.Student"/> to be deleted.</param>
        public void DeleteStudent(Student student)
        {
            //  Remove the student record.
            db.Remove(student);
        }


        /// <summary>
        /// Returns a List of Student records.
        /// </summary>
        /// <returns>List(Student) records</returns>
        public List<Student> GetStudents()
        {
            return db.ToList<Student>();
        }

        /// <summary>
        /// Returns an instance of a Student record.
        /// </summary>
        /// <param name="Firstname"></param>
        /// <param name="Surname"></param>
        /// <returns></returns>
        public Student GetStudent(string Firstname, string Surname)
        {
            if (Firstname == null || Surname == null)
                return null;

            var student = (from s in db
                           where s.Firstname == Firstname && s.Surname == Surname
                           select s).FirstOrDefault();
            return student;

        }

        public Student GetStudentById(long id)
        {
            if (id == 0) return null;

            var student = (from s in db
                           where s.Id == id
                           select s).FirstOrDefault();
            return student;
        }

        /// <summary>
        /// Returns a list of students that match the search string.
        /// The search string is contained in either the FirstName
        /// or Surname fields.
        /// </summary>
        /// <param name="q">The search string</param>
        /// <returns>A list of matching student records.</returns>
        public List<Student> SearchStudents(string q)
        {
            //  Return entire db if query string is not defined
            if (q==null || q=="") return db;

            //  Return matching student records.
            var students = from s in db
                           where s.Firstname.Contains(q) || s.Surname.Contains(q)
                           select s;
            return students.ToList<Student>();
        }


        public Model.Student FirstStudent()
        {
            //  If no students exist, return nothing.
            if (db.Count() == 0) return null;
            //  Reset the current index and return thr first element
            //  in the list.
            currentItem = 0;
            return db[currentItem];
        }


        public Model.Student LastStudent()
        {
            if (db.Count() == 0) return null;

            currentItem = db.Count() - 1;
            return db[currentItem];
        }


        public Model.Student NextStudent()
        {
            if (db.Count() == 0) return null;

            if (currentItem == db.Count() - 1) return db[currentItem];

            currentItem++;
            return db[currentItem];
        }

        public Model.Student PreviousStudent()
        {
            if (db.Count() == 0) return null;

            if (currentItem == 0) return db[currentItem];

            currentItem--;
            return db[currentItem];
        }
    }
}
