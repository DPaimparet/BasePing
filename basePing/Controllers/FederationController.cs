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
            if (Pays != null && nomFederation != "" && web != "")
            {
                federation.AddFederation(nomFederation, pays, web);
                return Redirect("Index");
            }
            else
            {
                return Redirect("Index?error=Tous les champs doivent être complétés");
            }
        }
        public ActionResult UpdateFederation(int id)
        {
            Federation federation = new Federation();
            ViewBag.federation = federation.GetFederation(id);
            return View();
        }
        public ActionResult UpdateFede(int id, string nomFederation, int? Pays, string web)
        {
            if (Pays != null && nomFederation != "")
            {
                Federation federation = new Federation();
                federation.UpdateFederation(id, nomFederation, Pays, web);
                return Redirect("Index");
            }
            else
            {
                return Redirect("UpdateFederation/"+ id + "/?error=Tous les champs doivent être complétés");
            }
            
        }

        public ActionResult DeleteFederation(int id)
        {
            Federation federation = new Federation();
            federation.DeleteFederation(id);
            return Redirect("~/Federation/Index");
        }
    }
}