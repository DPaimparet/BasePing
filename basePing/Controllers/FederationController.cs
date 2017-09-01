using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.Models;

namespace basePing.Controllers
{
    public class FederationController : Controller
    {
        // GET: Federation
        public ActionResult Index()
        {
            List<Federation> listeFederation = new List<Federation>();
            Federation federation = new Federation();
            listeFederation = federation.GetList();
            ViewBag.liste = listeFederation;
            return View();
        }
        public ActionResult AddFederation(string nomFederation, int? Pays, string web)
        {
            string pays = Pays.ToString();
            Federation federation = new Federation();
            federation.AddFederation(nomFederation, pays, web);
            return Redirect("Index");
        }
    }
}