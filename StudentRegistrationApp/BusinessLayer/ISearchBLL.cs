using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.BusinessLayer
{
    public interface ISearchBLL
    {
        IList<BusinessEntities.SearchCriteria> GetSearches();
        void AddSearch(BusinessEntities.SearchCriteria search);
        void SaveSearches();
    }
}
