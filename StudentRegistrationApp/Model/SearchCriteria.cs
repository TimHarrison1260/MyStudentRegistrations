using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationApp.Model
{
    public class SearchCriteria
    {
        [Required]
        public string Title { get; set; }

        public string searchString { get; set; }

        public BusinessEntities.SearchCategoriesEnum category { get; set; }
    }
}
