using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.Models;
using basePing.DataContext;
using basePing.ViewModel;

namespace basePing.Controllers
{
    public class JoueurController : Controller
    {
        // GET: Joueur
        public ActionResult Index()
        {
            List<CPays> listePays = new CPays().GetListPays();
            ViewBag.listePays = new SelectList(listePays, "Id", "Pays");
            return View();
        }
        [HttpPost]
        public ActionResult ListeJoueurs(string nom, string pays, string sexe)
        {
            char sex = 'f';
            int id;
            int.TryParse(pays, out id);
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
                listeJoueur = joueur.GetJoueurByNation(id, sex);
                ViewBag.listeJoueur = listeJoueur;
            }
            //if(nom != "" && pays == null)
            //{
            //    listeJoueur = joueur.GetJoueurByNation(id, sex);
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

        public ActionResult AjoutJoueurPoule(int id)
        {
            List<Joueur> listeJ = new Joueur().GetListJoueur();
            Session["idPoule"] = id;
            ViewBag.listeJoueur = new SelectList(listeJ, "Id", "Nom");
            return View();
        }


        [HttpPost]
        public ActionResult AjoutJoueurPoule(string joueur, int pos, int matchg,int matchp)
        {

            infoJoueur j = new infoJoueur{ position=pos,matchGagné=matchg,matchPerdu=matchp,Id=Convert.ToInt32(joueur) };
            DCJoueur dc = new DCJoueur();
            dc.InsertLD(j, (int)Session["idPoule"]);
            Session["idPoule"] = null;
            return View();
        }

        public ActionResult ModifierJoueurPoule(int idP, int idJ, int matchG, int matchP,int pos)
        {
            infoJoueur j = new infoJoueur
            {
                matchGagné = matchG,
                matchPerdu = matchP,
                position=pos,
                Id = idJ
            };
            ViewBag.idP = idP;
            return View(j);
        }

        [HttpPost]
        public ActionResult ModifierJpoule(int idP, int idJ, int pos,int matchG, int matchP)
        {
            infoJoueur j = new infoJoueur
            {
                matchGagné = matchG,
                matchPerdu = matchP,
                position=pos,
                Id = Convert.ToInt32(idJ)
            };
            DCJoueur dc = new DCJoueur();
            dc.UpdateLD(j, idP);
            return View();
        }

        public ActionResult SupprimerJoueurPoule(int idP,int idJ)
        {
            DCJoueur dc = new DCJoueur();
            dc.DeleteLD(idJ, idP);
            return View();
        }
    }
}