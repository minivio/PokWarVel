using MarvelApi;
using MarvelApi.Model;
using PokWarVel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokWarVel.Controllers
{
    public class HomeController : Controller
    {
        MarvelRequester r = new MarvelRequester();

        public ActionResult Index()
        {
            
            List<Characters> info = r.GetCharacters(limit: 100);

            return View(info);
        }

        public ActionResult About()
        {
            StarWarsRequester r = new StarWarsRequester();

            return View(r.GetPersos());
        }

        public ActionResult Contact()
        {
            PokemonRequester r = new PokemonRequester();


            return View(r.CatchThemAll());
        }
    }
}