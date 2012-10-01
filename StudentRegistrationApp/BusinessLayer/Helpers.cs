using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace StudentRegistrationApp.BusinessLayer
{
    public class Helpers
    {
        /// <summary>
        /// Converts a <see cref="Model.Student"/> class to  <see cref="BusinessEntities.Student"/>
        /// class.  The Model.Student class is referenced by the Data Access Layer, while the
        /// BusinessEntities.Student is referenced by this Business Logic Layer.
        /// </summary>
        /// <param name="modelStudent">The instance of the Model.Student class</param>
        /// <returns>An instance of the BusinessEntities.Student class</returns>
        public BusinessEntities.Student ToBusinessEntity(Model.Student modelStudent)
        {
            BusinessEntities.Student businessStudent = new BusinessEntities.Student();

            businessStudent.Firstname = modelStudent.Firstname;
            businessStudent.Surname = modelStudent.Surname;
            businessStudent.DOB = modelStudent.DOB.ToString();
            businessStudent.Course = modelStudent.Course;
            businessStudent.YearOfStudy = (BusinessEntities.YearOfStudyEnum)modelStudent.YearOfStudy;

            businessStudent.Address = new BusinessEntities.Address();
            businessStudent.Address.Address1 = modelStudent.Address.Address1;
            businessStudent.Address.Town = modelStudent.Address.Town;
            businessStudent.Address.County = modelStudent.Address.County;
            businessStudent.Address.PostCode = modelStudent.Address.PostCode;

            businessStudent.Contact = new BusinessEntities.ContactDetail();
            businessStudent.Contact.HomePhone = modelStudent.Contact.HomePhone;
            businessStudent.Contact.MobilePhone = modelStudent.Contact.MobilePhone;
            businessStudent.Contact.HomeEmail = modelStudent.Contact.HomeEmail;
            businessStudent.Contact.StudentEmail = modelStudent.Contact.StudentEmail;

            return businessStudent;
        }

        /// <summary>
        /// Converts a <see cref="BusinessEntities.Student"/> class to a <see cref="Model.Student"/>
        /// class. The BusinessEntities.Student class is referenced by this Business Logic Layer
        /// while the Model.Student class is referenced by the Data Access Layer.
        /// </summary>
        /// <param name="businessStudent">The instance of the BusinessEntities.Student class.</param>
        /// <returns>A corresponding instance of the Model.Student class.</returns>
        public Model.Student ToModel(BusinessEntities.Student businessStudent)
        {
            Model.Student modelStudent = new Model.Student();

            modelStudent.Firstname = businessStudent.Firstname;
            modelStudent.Surname = businessStudent.Surname;
            modelStudent.DOB = DateTime.Parse(businessStudent.DOB);
            modelStudent.Course = businessStudent.Course;
            modelStudent.YearOfStudy = (Model.YearOfStudyEnum)businessStudent.YearOfStudy;

            modelStudent.Address = new Model.Address();
            modelStudent.Address.Address1 = businessStudent.Address.Address1;
            modelStudent.Address.Town = businessStudent.Address.Town;
            modelStudent.Address.County = businessStudent.Address.County;
            modelStudent.Address.PostCode = businessStudent.Address.PostCode;

            modelStudent.Contact = new Model.ContactDetail();
            modelStudent.Contact.HomePhone = businessStudent.Contact.HomePhone;
            modelStudent.Contact.MobilePhone = businessStudent.Contact.MobilePhone;
            modelStudent.Contact.HomeEmail = businessStudent.Contact.HomeEmail;
            modelStudent.Contact.StudentEmail = businessStudent.Contact.StudentEmail;

            return modelStudent;
        }
    }
}
