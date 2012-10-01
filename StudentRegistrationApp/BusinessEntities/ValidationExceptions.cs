using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.BusinessEntities
{
    public class ValidationExceptions
    {
    }

    public class RequiredDetailsNotEnteredException : Exception
    {
        //  Implement the 3 base constructors for Exception
        public RequiredDetailsNotEnteredException()
            : base()
        {
        }

        public RequiredDetailsNotEnteredException(string txt)
            : base(txt)
        {
        }

        public RequiredDetailsNotEnteredException(string txt, Exception e)
            : base(txt, e)
        {
        }
    }

    public class DateOfBirthException : Exception
    {
        public DateOfBirthException() 
            : base() 
        { 
        }
        public DateOfBirthException(string txt)
            : base(txt)
        {
        }
        public DateOfBirthException(string txt, Exception e)
            : base(txt, e)
        {
        }
    }

}
