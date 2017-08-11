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
            List<CPays> listePays = new CPays().GetListPays();
            ViewBag.ListePays = new SelectList(listePays, "Id", "Pays");

            return View();
        }
        [HttpPost]
        public ActionResult ListeJoueurs(string nom, int? pays, string sexe)
        {
            char sex = 'f';
            DCJoueur joueur = new DCJoueur();
            List<Joueur> listeJoueur = new List<Joueur>();
            if (sexe == "Masculin")
            {
                sex = 'm';
            }

            if (nom == "" && pays == null)
            {
                listeJoueur = joueur.GetJoueurBySex(sex);
                ViewBag.listeJoueur = listeJoueur;
            }
            if(nom == "" && pays != null)
            {
                listeJoueur = joueur.GetJoueurByNation(pays, sex);
                ViewBag.listeJoueur = listeJoueur;
            }
            //if(nom != "" && pays == null)
            //{
            //    listeJoueur = joueur.GetJoueurByNation(pays, sex);
            //    ViewBag.listeJoueur = listeJoueur;
            //}
            if(nom != "")
            {
                listeJoueur = joueur.GetJoueur(nom);
                ViewBag.listeJoueur = listeJoueur;
            }
            return View();
        }

        public ActionResult Joueur(int id)
        {
            DCJoueur joueur = new DCJoueur();
            Joueur player = new Joueur();
            player = joueur.GetJoueur(id);
            ViewBag.dateNaissance =player.DateNaissance.ToString("dddd, le dd MMMM yyyy ");
            if(player.Sexe == 'f')
            {
                ViewBag.sexe = "Féminin";
            }
            else
            {
                ViewBag.sexe = "Masculin";
            }
            ViewBag.Joueur = player;
            return View();
        }
    }
}