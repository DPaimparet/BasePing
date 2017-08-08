using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.Models;

namespace basePing.Controllers
{
    public class JoueurController : Controller
    {
        // GET: Joueur
        public ActionResult Index()
        {
            Session["admin"] = true;
            return View();
        }

        public ActionResult Joueur()
        {
            //Joueur player = new Joueur();
            return View();
        }
    }
}