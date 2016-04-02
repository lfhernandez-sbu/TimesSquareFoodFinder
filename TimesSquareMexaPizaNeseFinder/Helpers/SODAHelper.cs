using System.Linq;
using SODA;
using PagedList;
using TimesSquareMexaPizaNeseFinder.Models;

namespace TimesSquareMexaPizaNeseFinder.Helpers
{
    public class SODAHelper
    {
        private const string _AppToken = "";
        private const string _APIEndPointHost = "data.cityofnewyork.us";
        private const string _APIEndPoint4x4 = "7rwn-iw5c";

        public static PagedList<BusinessLocation> GetBusinessLocations(string SearchQuery, int PageNumber, int PageSize, string OrderBy, bool OrderByAscDesc)
        {
            var client = new SodaClient(_APIEndPointHost, _AppToken);
            var dataset = client.GetResource<PagedList<BusinessLocation>>(_APIEndPoint4x4);
            var columns = new[] { "company_name", "sub_subindustry", "phone", " location_1" };
            var aliases = new[] { "CompanyName", "SubIndustry", "Phone", "Location" };
            var soql = new SoqlQuery().Select(columns)
                .As(aliases)
                .Order((OrderByAscDesc) ? SoqlOrderDirection.ASC : SoqlOrderDirection.DESC, new[] { OrderBy });

            if(!string.IsNullOrWhiteSpace(SearchQuery))
            {
                soql = new SoqlQuery().FullTextSearch(SearchQuery);
            }

            var results = dataset.Query<BusinessLocation>(soql);

            PagedList<BusinessLocation> pagedResults = new PagedList<BusinessLocation>(results.ToList(), PageNumber, PageSize);

            return pagedResults;
        }


    }
}