using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.Models;
using basePing.DataContext;

namespace basePing.Controllers
{
    public class JoueurController : Controller
    {
        // GET: Joueur
        public ActionResult Index()
        {
            Session["admin"] = true;
            DCJoueur joueur = new DCJoueur();
            List<Joueur> listeJoueur = new List<Joueur>();
            listeJoueur = joueur.GetAllJoueur();
            ViewBag.listeJoueur = listeJoueur;
            return View();
        }

        public ActionResult Joueur(int id)
        {
            DCJoueur joueur = new DCJoueur();
            Joueur player = new Joueur();
            player = joueur.GetJoueur(id);
            ViewBag.Joueur = player;
            return View();
        }
    }
}