using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.Model
{
    /// <summary>
    /// Class <c>Address</c> represents the Address information
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the Address line
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Town part of the address
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the County of the address
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the Post Code for the address
        /// </summary>
        public string PostCode { get; set; }
    }
}
