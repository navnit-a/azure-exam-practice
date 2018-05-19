using System.Web.Mvc;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace AzureIndexer.Controllers
{
    public class MyResult
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var searchServicename = "navz";
            var apikey = "129283F03350AA2A0E9CE79625C41CF3";

            var searchClient = new SearchIndexClient(searchServicename, "azuresql-index",
                new SearchCredentials(apikey));

            var sp = new SearchParameters
            {
                SearchMode = SearchMode.All
            };
            var docs = searchClient.Documents.Search("Fass", sp);

            return Json(docs.Results, JsonRequestBehavior.AllowGet);
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