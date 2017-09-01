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
    }
}