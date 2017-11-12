using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.DataContext;
using System.Web.Security;

namespace basePing.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Disconect()
        {
            Session["admin"] =false;
            return View("Index");
        }

        [HttpPost]
        public ActionResult Connexion(string login, string password)
        {
            DCLogin dc = new DCLogin();
            if (dc.find(login, password)) {
                Session["admin"] = true;


                FormsAuthentication.SetAuthCookie("1",false);
                
                return Redirect("~/Home/Connect");
            }
            else
                return Redirect("~/Login?error=Login ou mot de passe incorrect");

        }
    }
}