using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.BusinessEntities
{
    /// <summary>
    /// Class <c>ContactDetail</c> represents the contact information
    /// </summary>
    public class ContactDetail
    {
        /// <summary>
        /// Gets or sets the Home Phony Number 
        /// </summary>
        public string HomePhone { get; set; }

        /// <summary>
        /// Gets or sets the Mobile Phone number
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Gets or sets the Home email address
        /// </summary>
        public string HomeEmail { get; set; }

        /// <summary>
        /// Gets or sets the Student Email address.
        /// </summary>
        public string StudentEmail { get; set; }
    }
}
