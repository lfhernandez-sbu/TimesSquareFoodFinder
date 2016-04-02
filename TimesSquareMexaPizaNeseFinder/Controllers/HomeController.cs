using System.Web.Mvc;
using TimesSquareMexaPizaNeseFinder.Helpers;

namespace TimesSquareMexaPizaNeseFinder.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index(string SortOrder, string CurrentFilter, string SearchQuery, int? CurrentPage)
        {
            // Paging
            int pageSize = 20;
            int pageNumber = (CurrentPage ?? 1);

            // Sort by column
            if (string.IsNullOrEmpty(SortOrder))
            {
                SortOrder = "";
            }

            ViewBag.CurrentSort = SortOrder;
            ViewBag.NameSortParm = (SortOrder == "company_name") ? "company_name_desc" : "company_name";
            ViewBag.DBASortParm = (SortOrder == "sub_subindustry") ? "sub_subindustry_desc" : "sub_subindustry";
            ViewBag.ZipSortParm = (SortOrder == "phone") ? "phone_desc" : "phone";
            ViewBag.IssuedSortParm = (SortOrder == "location_1") ? "location_1_desc" : "location_1";

            bool sortAsc = (!SortOrder.Contains("_desc")) ? true : false;

            // Filtering
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                CurrentPage = 1;
            }
            else
            {
                SearchQuery = CurrentFilter;
            }

            ViewBag.CurrentFilter = SearchQuery;

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                SearchQuery = SearchQuery.ToUpper().Trim();
            }
            var bigData = SODAHelper.GetBusinessLocations(SearchQuery, pageNumber, pageSize, SortOrder.Replace("_desc", ""), sortAsc);

            return View(bigData);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}