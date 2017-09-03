using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.Models;
using basePing.DataContext;

namespace basePing.Controllers
{
    public class MatchController : Controller
    {
        // GET: Match
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjoutMatch(int id)
        {
            ViewBag.idComp = id;
            List<Joueur> j = new Joueur().GetListJoueurComp(id);
            Session["listJ"] = new SelectList(j, "Id", "Nom");
            return View();
        }


        [HttpPost]
        public ActionResult AjoutMatch(int id,int? joueur1,int score1,int? joueur2,int score2)
        {
            DCMatch dc = new DCMatch();
            dc.Create(id,joueur1,score1,joueur2,score2);
            return Redirect("~/Competition/InfoComp/" + id);
        }

        public ActionResult SuppMatch(int id)
        {
            DCMatch dc = new DCMatch();
            dc.Delete(id);
            return Redirect("~/Competition/InfoComp/"+ Session["idC"]);
        }

        public ActionResult LieMatch(int pos,int idC,int idS)
        {
            Session["pos"] = pos;
            Session["idC"]=idC;
            Session["idS"] = idS;
            List<Joueur> listJ = new Joueur().GetListJoueurComp(idC);
           
            List<Match> listM = new Match().GetMatchComp(idC);
            foreach (Match m in listM)
            {
                m.Joueur1.RecupererJoueur();
                m.Joueur2.RecupererJoueur();
            }
            Session["listJ"] = new SelectList(listJ, "Id", "Nom");
            Session["listM"] = new SelectList(listM, "Id", "Info");
            return View();
        }


        [HttpPost]
        public ActionResult LieMatch(int match)
        {
            DCMatch dc = new DCMatch();
            dc.LinkMatch((int)Session["pos"],(int) Session["idS"],(int)Session["idC"]);
            return Redirect("~/Competition/InfoComp/" + Session["idC"]);
        }


        public ActionResult LieMatchPoule(int idJ, int idC, int idS)
        {
            Session["idC"] = idC;
            Session["idS"] = idS;
            Session["pos"] = 0;
            Joueur jToRemove = null;
            List<Joueur> listJ = new Joueur().GetListJoueurComp(idC);
            foreach (Joueur j in listJ)
            {
                if (j.Id == idJ)
                    jToRemove = j;
            }
            listJ.Remove(jToRemove);
            List<Match> listM = new Match().GetMatchComp(idC,idJ);
            foreach (Match m in listM)
            {
                m.Joueur1.RecupererJoueur();
                m.Joueur2.RecupererJoueur();
            }
            Session["listJ"] = new SelectList(listJ, "Id", "Nom");
            Session["listM"] = new SelectList(listM, "Id", "Info");
            return View();
        }


        [HttpPost]
        public ActionResult LieMatchPoule(int match)
        {
            DCMatch dc = new DCMatch();
            dc.LinkMatch((int)Session["pos"], (int)Session["idS"], (int)Session["idC"]);
            return Redirect("~/Competition/InfoComp/" + Session["idC"]);
        }

        [HttpPost]
        public ActionResult AjoutEtLieMatch(int? joueur1, int score1, int? joueur2, int score2)
        {
            DCMatch dc = new DCMatch();
            dc.Create(joueur1, score1, joueur2, score2,(int)Session["pos"],(int)Session["idS"], (int)Session["idC"]);
            return Redirect("~/Competition/InfoComp/" + Session["idC"]);
        }



        [HttpPost]
        public ActionResult AjoutEtLieMatchPoule(int score1,int? joueur, int score2)
        {
            DCMatch dc = new DCMatch();
            dc.Create((int)Session["idJ"], score1, joueur, score2, 0, (int)Session["idS"], (int)Session["idC"]);
            return Redirect("~/Competition/InfoComp/" + Session["idC"]);
        }
    }
}