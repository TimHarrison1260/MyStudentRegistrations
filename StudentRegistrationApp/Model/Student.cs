using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace StudentRegistrationApp.Model
{
    /// <summary>
    /// Class <c>Student</c> represents the information about
    /// a student in the Registration system.
    /// </summary>
    public class Student : IEquatable<Student>
    {
        private string _firstName;
        private string _surname;

        /// <summary>
        /// Gets or sets the id for the Student record
        /// It is represented as a hashcode of the 
        /// concatenation of the firstname and surname.
        /// </summary>
        /// <remarks>
        /// This Id (hashCode) is set when both the firstname
        /// and surname properties are set.
        /// The system identifies student records by comparing
        /// the firstname and surname, such that once a record
        /// is created, the firstname and surnme cannot be altered
        /// both fields are required, therefore, when can generate
        /// this id uniquely and safely.
        /// This will allow us to identify the record for updates,
        /// deletion and browsing by using this id.
        /// </remarks>
        public long Id { get; set; }

        /// <summary>
        /// Gets or Sets the Name of the Student
        /// </summary>
        [Required(ErrorMessage="Student Name cannot be blank.  Please enter a valid Firstname.")]
        public string Firstname { 
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                if ((_surname != null && _surname != string.Empty) && (_firstName != null && _firstName != string.Empty))
                    this.Id = (_firstName + _surname).GetHashCode();
            }
        }

        /// <summary>
        /// Gets or sets the Surname of the Student
        /// </summary>
        [Required(ErrorMessage="Student Surname cannot be blank.  Please enter a valid name.")]
        public string Surname {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                if ((_firstName != null && _firstName != string.Empty) && (_surname != null && _surname != string.Empty))
                    this.Id = (_firstName + _surname).GetHashCode();
            }
        }

        /// <summary>
        /// Gets or Sets the Date of Birth of the Student
        /// </summary>
        [Required(ErrorMessage="Date of Birth is a required value.  Please enter a valid date")]
        [DataType(DataType.DateTime, ErrorMessage="Entered value is not a date, please enter a valid date")]
        public DateTime DOB { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Address"/>Address of the Student.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the Title of the Course.
        /// </summary>
        [Required(ErrorMessage="Course title cannot be blank, please enter a valid course title.")]
        public string Course { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="YearOfStudy"/> Year Of Study for the Student.
        /// </summary>
        [Required(ErrorMessage="Year of study is required, please select a valid value from the list.")]
        public YearOfStudyEnum YearOfStudy { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Contact"/> Contact information for the Student
        /// </summary>
        public ContactDetail Contact { get; set; }


        /// <summary>
        /// We implement the IEquatable interface so that we can
        /// determine the rules that make student records equal.
        /// </summary>
        /// <param name="other">The Student record being compared to this student record</param>
        /// <returns>True if equal otherwise false.</returns>
        public bool Equals(Student other)
        {
            //  Determine that Student Records are Equal 'the same'
            //  if the Firstname and Surname are the same.
            if (other == null) throw new ArgumentNullException("No valid Student available for equality check");

            return 
                (this.Firstname == other.Firstname &&
                this.Surname == other.Surname &&
                this.DOB == other.DOB);
        }

        /// <summary>
        /// Temporary method as we'll replace it with serialization attribute later.
        /// So here we'll just serialise the Contact and Address classes here too.
        /// </summary>
        /// <returns></returns>
        public string ToSerialisedStudent()
        {
            string s = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}",
                this._firstName, this._surname, this.Course, this.DOB.ToString(), this.YearOfStudy,
                this.Address.Address1, this.Address.Town, this.Address.County, this.Address.PostCode,
                this.Contact.HomePhone, this.Contact.MobilePhone, this.Contact.HomeEmail, this.Contact.StudentEmail
                );
            return s;
        }

        /// <summary>
        /// Temporary method as we'll replace it with erialization attribute later.
        /// </summary>
        /// <param name="serialisedStudent"></param>
        public void FromSerialisedStudent(string serialisedStudent)
        {
            if (serialisedStudent == null || serialisedStudent == string.Empty)
            {
                this._firstName = string.Empty;
                this._surname = string.Empty;
                this.Course = string.Empty;
                this.YearOfStudy = YearOfStudyEnum.First;
                this.Address = new Address();
                this.Contact = new ContactDetail();
            }
            else
            {
                //  Separate out the string
                char[] sep = { '|' };
                string[] elements = serialisedStudent.Split(sep);
                this.Firstname = elements[0];
                this.Surname = elements[1];
                this.Course = elements[2];
                this.DOB = DateTime.Parse(elements[3]);
                this.YearOfStudy = (YearOfStudyEnum)Enum.Parse(typeof(YearOfStudyEnum), elements[4]);

                this.Address = new Address();
                this.Address.Address1 = elements[5];
                this.Address.Town = elements[6];
                this.Address.County = elements[7];
                this.Address.PostCode = elements[8];

                this.Contact = new ContactDetail();
                this.Contact.HomePhone = elements[9];
                this.Contact.MobilePhone = elements[10];
                this.Contact.HomeEmail = elements[11];
                this.Contact.StudentEmail = elements[12];





            }
        }
    }
}
