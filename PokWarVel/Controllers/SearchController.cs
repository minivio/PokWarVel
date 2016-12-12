using MarvelApi;
using MarvelApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokWarVel.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string toLookFor)
        {
            ViewBag.Message = toLookFor;
            return View();
        }

        [HttpPost]
        public ActionResult Results(string toLookFor)
        {
            MarvelRequester r = new MarvelRequester();
            List<Characters> resultList = new List<Characters>();
            resultList = (toLookFor != "")? r.SearchCharacters(limit: 100, offset: 0, SearchString: toLookFor) : r.GetCharacters(limit:100, offset: 0);
            ViewBag.Message = toLookFor;
            return View(resultList);
        }
    }
}