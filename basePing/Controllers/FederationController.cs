﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.Models;
using basePing.DataContext;

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


        [Authorize]
        public ActionResult AddFederation(string nomFederation, int? Pays, string web)
        {
            string pays = Pays.ToString();
            Federation federation = new Federation();
            nomFederation = HttpUtility.HtmlEncode(nomFederation);
            web = HttpUtility.HtmlEncode(web);
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


        [Authorize]
        public ActionResult UpdateFederation(int id)
        {
            Federation federation = new Federation();
            List<CPays> listP = new CPays().GetListPays();
            CPays pays = null;
      
            federation = federation.GetFederation(id);
            foreach (CPays P in listP)
            {
                if (P.Pays == federation.PaysFederation)
                {
                    pays = P;
                }
            }
            listP.Insert(0, pays);
       
            Session["listP"] = new SelectList(listP,"Id","Pays");
            return View(federation);
        }


        [Authorize]
        public ActionResult UpdateFede(int id, string nomFederation, int? Pays, string web)
        {
            nomFederation = HttpUtility.HtmlEncode(nomFederation);
            web = HttpUtility.HtmlEncode(web);
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


        [Authorize]
        public ActionResult DeleteFederation(int id)
        {
            Federation federation = new Federation();
            federation.DeleteFederation(id);
            return Redirect("~/Federation/Index");
        }
    }
}