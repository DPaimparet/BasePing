using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.DataContext;

namespace basePing.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
           
           
            return View();
        }

        
        [HttpPost]
        public ActionResult Connexion(string login, string password)
        {
            DCLogin dc = new DCLogin();
            if (dc.find(login, password)) {
                Session["admin"] = true;
                return Redirect("~/Home/Connect");
            }
            else
                return Redirect("~/Login?error=Login ou mot de passe incorrect");

        }
    

        /*
        [HttpPost]
        public ActionResult Register(string nom, string prenom, string tel, DateTime dateNaiss, string mail, string spec, string pass)
        {
            CPersonnel p;
            p = new CPersonnel(0, nom, prenom, tel, dateNaiss, mail, spec, "");
            if (p.AjoutPersonnel(pass))
            {
                return View("Index");
            }
            else
            {
                ViewBag.Message = "Erreur: émail déja existant";
                return View("Register");
            }
        }*/
    }
}