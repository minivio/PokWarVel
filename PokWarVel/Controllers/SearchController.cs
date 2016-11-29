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
            resultList = r.SearchCharacters(limit: 100, offset: 0, SearchString: toLookFor);
            //r.GetCharactersContainingString(toLookFor);
            //bool moreResults = true;
            //List<Characters> resultats = new List<Characters>();
            //int newOffset = 0;

            //do
            //{
            //    List<Characters> newListe = r.GetCharacters(limit: 100, offset: newOffset);

            //    if (newListe != null)
            //    {
            //        foreach (Characters perso in newListe)
            //        {
            //            if (perso.name.ToLower().Contains(toLookFor.ToLower()))
            //            {
            //                resultats.Add(perso);
            //            }
            //        }
            //        newOffset += 100;
            //    }
            //    else
            //    {
            //        moreResults = false;
            //    }
                
            //} while (moreResults);

       
            
            
            // filtre
            

            ViewBag.Message = toLookFor;
            //return View(laListe.Where(l => l.name.ToLower().Contains(toLookFor.ToLower())));
            return View(resultList);
        }
    }
}