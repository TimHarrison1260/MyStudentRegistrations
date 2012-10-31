using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using StudentRegistrationApp.Model;     //  Contains repository class for the unit test.


namespace StudentRegistrationUnitTests
{
    [TestClass]
    public class StudentRepositoryTests
    {
        [TestMethod]
        public void InitialListHasTwoEntries()
        {
            //  Arrange:    Set up an intance of the Student Repository
            var db = new StudentRegistrationApp.Model.StudentRepository();

            //  Act:        Perform the unit tests.
            List<StudentRegistrationApp.Model.Student> results = db.GetStudents();

            //  Assert: Result should be 2 items
            Assert.AreEqual(results.Count(), 2);

        }

        [TestMethod]
        public void GetStudentTimByKey()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();

            //  Act:
            var student = db.GetStudent("Tim", "Harrison");

            //  Assert
            Assert.AreEqual(student.Firstname, "Tim");
            Assert.AreEqual(student.Surname, "Harrison");
        }

        [TestMethod]
        public void GetStudentByKeyThatDoesNotExist()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();

            //  Act:
            var student = db.GetStudent("Fred", "Harrison");

            //  Assert
            Assert.IsNull(student);
            Assert.AreEqual(db.GetStudents().Count(), 2);
        }

        [TestMethod]
        public void AddStudentToList()
        {
            //  Arrange;
            var db = new StudentRegistrationApp.Model.StudentRepository();

            var student = new StudentRegistrationApp.Model.Student()
            {
                Firstname = "Jude",
                Surname = "Comber",
                DOB = new DateTime(1963, 9, 26),
                YearOfStudy = StudentRegistrationApp.Model.YearOfStudyEnum.First
            };
            //  Act:
            db.AddStudent(student);
            List<StudentRegistrationApp.Model.Student> results = db.GetStudents();
            //  Assert:
            Assert.AreEqual(results.Count(), 3);
        }

        [TestMethod]
        public void FindStudentNancy()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();

            //  Act:
            var result = db.SearchStudents("Nancy", "Firstname");

            //  Assert:
            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result[0].Firstname, "Nancy", "Firstname");
        }

        [TestMethod]
        public void FindStudentFredDoesNotExist()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();

            //  Act:
            var result = db.SearchStudents("Fred", "Firstname");

            //  Assert: Nothing returned, Student Fred does not exist.
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void UpdateStudentTim()
        {
            //  Arrange;
            var db = new StudentRegistrationApp.Model.StudentRepository();

            var student = new StudentRegistrationApp.Model.Student()
            {
                Firstname = "Tim",
                Surname = "Harrison",
                DOB = new DateTime(1960, 2, 1),
                YearOfStudy = StudentRegistrationApp.Model.YearOfStudyEnum.Third
            };
            //  Act:
            db.UpdateStudent(student);

            //  Assert: Tim is now in Third Year of Study
            var result = db.SearchStudents("Tim", "Firstname");

            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result[0].YearOfStudy, StudentRegistrationApp.Model.YearOfStudyEnum.Third);
        }


        [TestMethod]
        public void UpdateStudentTimNameChangeWhichShouldFail()
        {
            //  Arrange;
            var db = new StudentRegistrationApp.Model.StudentRepository();

            var student = new StudentRegistrationApp.Model.Student()
            {
                Firstname = "Tim Peter",
                Surname = "Harrison",
                DOB = new DateTime(1960, 2, 1),
                YearOfStudy = StudentRegistrationApp.Model.YearOfStudyEnum.Third
            };
            //  Act:
            db.UpdateStudent(student);

            //  Assert: Tim is still at second year, as the update should've failed.
            var result = db.SearchStudents("Tim", "Firstname");

            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result[0].YearOfStudy, StudentRegistrationApp.Model.YearOfStudyEnum.Second);
        }


        [TestMethod]
        public void UpdateStudentFredDoesNotExist()
        {
            //  Arrange;
            var db = new StudentRegistrationApp.Model.StudentRepository();

            var student = new StudentRegistrationApp.Model.Student()
            {
                Firstname = "Fred",
                Surname = "Harrison",
                DOB = new DateTime(1960, 2, 1),
                YearOfStudy = StudentRegistrationApp.Model.YearOfStudyEnum.Third
            };
            //  Act:
            db.UpdateStudent(student);

            //  Assert: Tim is now in Third Year of Study
            var results = db.GetStudents();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Firstname, "Tim");
            Assert.AreEqual(results[0].YearOfStudy, StudentRegistrationApp.Model.YearOfStudyEnum.Second);
            Assert.AreEqual(results[1].Firstname, "Nancy");
            Assert.AreEqual(results[1].YearOfStudy, StudentRegistrationApp.Model.YearOfStudyEnum.First);
        }

        

        [TestMethod]
        public void DeleteStudentNancy()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();

            var studentNancy = new StudentRegistrationApp.Model.Student();
            studentNancy.Firstname = "Nancy";
            studentNancy.Surname = "Harrison";
            studentNancy.DOB = new DateTime(1955, 9, 27);

            //  Act
            db.DeleteStudent(studentNancy);

            var result = db.SearchStudents("Nancy", "Firstname");

            //  Assert:
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void DeleteStudentNancyWrongSurname()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();
            //  Nancy has different Surname
            var studentNancy = new StudentRegistrationApp.Model.Student();
            studentNancy.Firstname = "Nancy";
            studentNancy.Surname = "Fergusson";
            studentNancy.DOB = new DateTime(1955, 9, 27);

            //  Act:    Should not delete nancy as Surname doesn't match
            db.DeleteStudent(studentNancy);
            //  Still find record Nancy.
            var result = db.SearchStudents("Nancy", "Firstname");

            //  Assert:     Still finds NAncy as it's not beed deleted.
            Assert.AreEqual(result.Count(), 1);
        }


        [TestMethod]
        public void GetTheFirstStudent()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();
            //  Act:
            var result = db.FirstStudent();
            //  Assert:
            Assert.AreEqual(result.Firstname, "Tim");
        }

        [TestMethod]
        public void GetTheLastStudent()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();
            //  Act:
            var result = db.LastStudent();
            //  Assert:
            Assert.AreEqual(result.Firstname, "Nancy");
        }


        [TestMethod]
        public void GetNextStudent()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();
            //  Act:    The initial set up is for the first element to be the current item.
            var result = db.NextStudent();
            //  Assert: We should be on the 2nd item, i.e. the last item.
            Assert.AreEqual(result.Firstname, "Nancy");
        }

        [TestMethod]
        public void GetNextStudentWhenAlreadyAtEnd()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();
            //  Act:    The initial set up is for the first element to be the current item.
            var positionAtEnd = db.NextStudent();
            var result = db.NextStudent();
            //  Assert: We should be on the 2nd item, i.e. the last item.
            Assert.AreEqual(positionAtEnd, result);
        }

        [TestMethod]
        public void GetPreviousStudent()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();
            //  Act:    The initial set up is for the first element to be the current item.
            var positionAtEnd = db.NextStudent();
            var result = db.PreviousStudent();      // should move back to firstitem.
            //  Assert: We should be on the 2nd item, i.e. the last item.
            Assert.AreEqual(result.Firstname, "Tim");
        }


        [TestMethod]
        public void GetPreviousStudentWhenAlreadyAtFirst()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();
            //  Act:    The initial set up is for the first element to be the current item.
            var positionAtStart = db.FirstStudent();
            var result = db.PreviousStudent();      // should move back to firstitem.
            //  Assert: We should be on the 2nd item, i.e. the last item.
            Assert.AreEqual(positionAtStart,result);
        }


        [TestMethod]
        public void GetLastStudentById()
        {
            //  Arrange:
            var db = new StudentRegistrationApp.Model.StudentRepository();
            //  Act:
            var lastResult = db.LastStudent();
            long lastId = lastResult.Id;
            var result = db.GetStudentById(lastId);
            //  Assert:
            Assert.AreEqual(result, lastResult, "Should get the last record");
        }


        [TestMethod]
        public void GetStudentByIdNotFound()
        {
            var db = new StudentRegistrationApp.Model.StudentRepository();
            //  Act:
            var lastResult = db.LastStudent();
            long lastId = lastResult.Id + 1;
            var result = db.GetStudentById(lastId);
            //  Assert:
            Assert.IsNull(result, "Should not find the id.");
        }
    }
}
