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
    public class HeroController : Controller
    {
        MarvelRequester r = new MarvelRequester();

        // GET: Hero
        public ActionResult Index()
        {
            return View("FichePerso");
        }

        [HttpGet]
        public ActionResult FichePerso(long id)
        {
            Characters newHero = r.GetCharacterById(id);
            EvalPerso newEval = new EvalPerso();
            newEval.Hero = newHero;
            newEval.IdHero = id;
            //ViewBag.idPerso = id;
            return View(newEval);
        }

        [HttpPost]
        public ActionResult FichePerso(EvalPerso newEval, long id)
        {

            newEval.IdHero = id;


            if (newEval.Save()) // Si la méthode Save renvoie true,
            {
                // tout va bien
                return RedirectToAction("Merci");
            }
            else
            {
                Characters newHero = r.GetCharacterById(newEval.IdHero);
                newEval.Hero = newHero;

                ViewBag.Erreur = "Ooooops!!! Erreur d'insertion de votre évaluation...";
                return View(newEval);
            }
        }

        

        public ActionResult Merci()
        {
            return View();
        }
    }
}