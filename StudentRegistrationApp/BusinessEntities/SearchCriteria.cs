using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using System.Xml.Serialization;                     //  Access to the XML Serialization stuff
using System.ComponentModel.DataAnnotations;        //  Metadata

namespace StudentRegistrationApp.BusinessEntities
{
    /// <summary>
    /// Class <c>SearchCriteria</c> contains the criteria used to search
    /// the data context.
    /// </summary>
    /// <remarks>
    /// All the properties are made public as they must all be included
    /// in the serialized persistence file.  The XmlSerialiser is being
    /// used so no [serialize] attributes are required.
    /// </remarks>
    public class SearchCriteria
    {
        [Required]
        public string Title { get; set; }
        
        public string searchString { get; set; }

        public BusinessEntities.SearchCategoriesEnum category { get; set; }
    }
}
