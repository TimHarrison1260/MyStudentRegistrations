using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationApp.BusinessLayer
{
    public class SearchBLL :ISearchBLL
    {
        private readonly Model.ISearchRepository _repository;
        private readonly Helpers helper = new Helpers();

        public SearchBLL(Model.ISearchRepository repository)
        {   
            if (repository == null)
                throw new ArgumentNullException("Null SearchRepository instance");
            _repository = repository;
        }

        public IList<BusinessEntities.SearchCriteria> GetSearches()
        {
            IList<BusinessEntities.SearchCriteria> searches = new List<BusinessEntities.SearchCriteria>();
            foreach (Model.SearchCriteria c in _repository.GetSearches())
            {
                searches.Add(helper.ToBusinessEntitySearchCriteria(c));
            }
            return searches;
        }

        public void AddSearch(BusinessEntities.SearchCriteria search)
        {
            if (search != null)
                _repository.AddSearch(helper.ToModelSearchCriteria(search));
        }

        public void SaveSearches()
        {
            _repository.SaveSearches();
        }
    }
}
