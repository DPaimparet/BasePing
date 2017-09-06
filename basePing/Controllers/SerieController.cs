using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.DataContext;
using basePing.Models;

namespace basePing.Controllers
{
    public class SerieController : Controller
    {
        // GET: Serie
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjoutPoule(string nom , int taille , int idComp)
        {
            DCPoule dc = new DCPoule();
            dc.Create(nom,taille,idComp);
            return Redirect("~/Competition/InfoComp/" + idComp);
        }

        public ActionResult ModifPouleForm(int idC, int id,string desc,string nom,int taille)
        {
            Poule p = new Poule(id,desc,taille,nom);
            Session["idC"] = idC;
            return View(p);
        }
        [HttpPost]
        public ActionResult ModifPoule(int id, string nom, string desc, int taille)
        {
            Poule p = new Poule(id, desc, taille, nom);
            DCPoule dc = new DCPoule();
            dc.Update(p);
            return Redirect("~/Competition/InfoComp/" + Session["idC"]);
        }


        public ActionResult SuppPoule(int id,int idC)
        {
           
            DCPoule dc = new DCPoule();
            dc.Delete(id);
            return Redirect("~/Competition/InfoComp/" + idC);
        }

        public ActionResult AjouterTournoi(int id)
        {
            ViewBag.idComp = id;
            return View();
        }

        [HttpPost]
        public ActionResult AjouterPhaseFinale(int id,int taille, string desc)
        {
            DCTournoi dc = new DCTournoi();
            dc.Create(id,taille,desc);
            return Redirect("~/Competition/InfoComp/" + id);
        }

        public ActionResult ListMatchPoule(int idP ,int idC ,int IdJ)
        {
            Session["idC"] = idC;
            Session["idS"] = idP;
            Session["idJ"] = IdJ;
            List<Match> listM = new Match().GetListMatch(idP,IdJ);
            foreach (Match m in listM)
            {
                m.Joueur1.RecupererJoueur();
                m.Joueur2.RecupererJoueur();
            }
            Session["listM"] =listM;
            return View();
        }
    }
}