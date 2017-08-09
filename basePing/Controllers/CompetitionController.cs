using MySql.Data.MySqlClient;
using System;
using System.Web.Mvc;
using basePing.DataContext;
using basePing.Models;
using System.Collections.Generic;

namespace basePing.Controllers
{
    public class CompetitionController : Controller
    {
        // GET: Competition
        public ActionResult Index()
        {
            Categorie cat = new Categorie();
            ViewBag.listCat = cat.GetList();
            
            return View();
        }

        public ActionResult GetComp(int id)
        {
            Competition comp = new Competition();
            List<Competition> triedList = new List<Competition>();
            foreach (Competition c in comp.GetList())
            {
                if (c.Cat.Id == id)
                    triedList.Add(c);
            }
            ViewBag.listComp = triedList;
            return View();
        }

        public ActionResult InfoComp(int id)
        {
            Competition comp = new Competition(id);
            return View();
        }
    }
}