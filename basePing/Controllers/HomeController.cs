using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.DataContext;
using basePing.Models;

namespace basePing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["admin"] = false;
            Categorie cat = new Categorie();
            Session["lCat"] = cat.GetList();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
                return View();
            else
                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Connect()
        {
            Categorie cat = new Categorie();
            Session["lCat"] = cat.GetList();
            return View("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Pour toutes questions ou demandes d'informations.";

            return View();
        }

        public ActionResult LogOut()
        {
            Session["admin"] = false;
            return View("Index");
        }
    }
}