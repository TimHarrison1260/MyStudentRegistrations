using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.Model
{
    public interface ISearchRepository
    {
        IList<SearchCriteria> GetSearches();
        void AddSearch(SearchCriteria search);
        void SaveSearches();
        void LoadDB();
    }
}
