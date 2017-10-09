using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.Models;
using basePing.DataContext;
using basePing.ViewModel;
using System.IO;

namespace basePing.Controllers
{
    public class JoueurController : Controller
    {



        // GET: Joueur
        public ActionResult Index()
        {
            List<CPays> listePays = new CPays().GetListPays();
            Session["listePays"] = new SelectList(listePays, "Id", "Pays");
            return View();
        }
        [HttpPost]
        public ActionResult ListeJoueurs(string nom, int? pays, string sexe)
        {
            // Initialisation
            char sex = 'f';
            DCJoueur joueur = new DCJoueur();
            List<Joueur> listeJoueur = new List<Joueur>();
            nom = HttpUtility.HtmlEncode(nom);
            ViewBag.alerte = 0;
            ViewBag.Message = null;
            // Vérification du sexe
            if (sexe == "Masculin")
            {
                sex = 'm';
            }

            // Appel des listes de joueurs selon les attribut sélectionnés
            // 1) Nom est rempli
            if (nom != "")
            {
                // Pays remplis , sexe vide (Vérifier)
                if (pays != null && sexe == "")
                {
                    listeJoueur = joueur.GetJoueurByNameAndCountry(nom,pays);
                    if(listeJoueur.Count() != 0)
                    {
                        ViewBag.listeJoueur = listeJoueur;
                    }
                    else
                    {
                        return Redirect("Index?error=Aucune information trouvée");
                    }
                }
                // Pays vide , sexe remplis
                if (pays == null && sexe != "")
                {
                    listeJoueur = joueur.GetJoueurByNameAndSex(nom,sex);
                    if (listeJoueur.Count() != 0)
                    {
                        ViewBag.listeJoueur = listeJoueur;
                    }
                    else
                    {
                        return Redirect("Index?error=Aucune information trouvée");
                    }
                }
                // Pays remplis , sexe remplis (OK)
                if (pays != null && sexe != "")
                {
                    listeJoueur = joueur.GetJoueurByNameCountryAndSex(nom, pays, sex);
                    if (listeJoueur.Count() != 0)
                    {
                        ViewBag.listeJoueur = listeJoueur;
                    }
                    else
                    {
                        return Redirect("Index?error=Aucune information trouvée");
                    }
                }
                // Pays vide , sexe vide (OK)
                else
                {
                    listeJoueur = joueur.GetJoueur(nom);
                    if (listeJoueur.Count() != 0)
                    {
                        ViewBag.listeJoueur = listeJoueur;
                    }
                    else
                    {
                        return Redirect("Index?error=Aucune information trouvée");
                    }
                }
            }
            else // Nom est vide
            {
                // Pays non sélectionné et Sexe sélectionné
                if (pays == null && sexe != "")
                {
                    listeJoueur = joueur.GetJoueurBySex(sex);
                    if (listeJoueur.Count() != 0)
                    {
                        ViewBag.listeJoueur = listeJoueur;
                    }
                    else
                    {
                        return Redirect("Index?error=Aucune information trouvée");
                    }
                }
                // Pays sélectionné et Sexe non sélectionné
                if (pays != null && sexe == "")
                {
                    listeJoueur = joueur.GetJoueurByNation(pays);
                    if (listeJoueur.Count() != 0)
                    {
                        ViewBag.listeJoueur = listeJoueur;
                    }
                    else
                    {
                        return Redirect("Index?error=Aucune information trouvée");
                    }
                }
                // Pays sélectionné et Sexe sélectionné
                if (pays != null && sexe != "")
                {
                    listeJoueur = joueur.GetJoueurByNationAndSex(pays, sex);
                    if (listeJoueur.Count() != 0)
                    {
                        ViewBag.listeJoueur = listeJoueur;
                    }
                    else
                    {
                        return Redirect("Index?error=Aucune information trouvée");
                    }
                }
                if(pays == null && sexe == "") // Pays non sélectionné et Sexe non sélectionné
                {
                    return Redirect("Index?error=Un choix minimum est requis");
                }
            }
            return View();
        }

        public ActionResult Joueur(int id)
        {
            
            DCJoueur joueur = new DCJoueur();
            Joueur player = new Joueur();
            string[] ext = new string[]{ "jpg", "jpeg", "png", "gif" };
            string file = "";
            int i = 0;
            string path = "";
            bool trouve = false;
            // Récupère les informations du joueur 
            player = joueur.GetJoueur(id);
            ViewBag.dateNaissance =player.DateNaissance.ToString("dddd, le dd MMMM yyyy ");
            do
            {
                path = id + "." + ext[i];
                if (System.IO.File.Exists(@"D:\ProjetWeb\basePing\basePing\Content\image\" + path))
                {
                    file = path;
                    trouve = true;
                }
                i++;
            } while (!trouve && i < ext.Length);
            ViewBag.photo = file;

            if (player.Sexe == 'f')
            {
                ViewBag.sexe = "Féminin";
            }
            else
            {
                ViewBag.sexe = "Masculin";
            }
            ViewBag.Joueur = player;
            // Récupère le palmarès civic du joueur
            PalmaresCivil palmares = new PalmaresCivil();
            ViewBag.listePalmaresCivic = palmares.GetListPalmaresCivic(id);

            // Récupéré le palmarès sportif du joueur
            PalmaresSportif palmaresSportif = new PalmaresSportif();
            ViewBag.palmaresSportif = palmaresSportif.GetList(id);
            return View();
        }


        [Authorize]

        public ActionResult NewJoueur()
        {
            return View();
        }


        [Authorize]
        public ActionResult DeleteJoueur(int idJoueur)
        {
            Joueur joueur = new Joueur();
            joueur.DeleteJoueur(idJoueur);
            return View("Index");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddJoueur(string nom,string prenom, string sexe, DateTime dateNaissance, int? pays)
        {
            // Initialisation
            char sex = 'f';
            // Empêché les injections
            nom = HttpUtility.HtmlEncode(nom);
            prenom = HttpUtility.HtmlEncode(prenom);
            // Vérification du sexe
            if (sexe == "Masculin")
            {
                sex = 'm';
            }

            if (nom != "" && prenom != "" && sexe != "" && dateNaissance != null && pays != null)
            {
                string Pays = pays.ToString();
                Joueur joueur = new Joueur(0, nom, prenom, dateNaissance, sex, Pays);
                joueur.AjouterJoueur();
                return Redirect("Index?error=Enregistrement effectuée");
            }
            else
            {
                return Redirect("NewJoueur?error=L'enregistrement n'a pas pu être effectué");
            }
        }

        [Authorize]
        public ActionResult UpdateJoueur(int idJoueur)
        {
            Joueur player = new Joueur();
            ViewBag.Joueur = player.RecupererJoueur(idJoueur);
            string[] ext = new string[] { "jpg", "jpeg", "png", "gif" };
            string file = "";
            int i = 0;
            string path = "";
            bool trouve = false;
            ViewBag.dateNaissance = player.DateNaissance.ToString("dddd, le dd MMMM yyyy ");
            do
            {
                path = idJoueur + "." + ext[i];
                if (System.IO.File.Exists(@"D:\ProjetWeb\basePing\basePing\Content\image\" + path))
                {
                    file = path;
                    trouve = true;
                }
                i++;
            } while (!trouve && i < ext.Length);
            ViewBag.photo = file;
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult UpdateJoueur(int idJoueur, string nom, string prenom, DateTime dateNaiss, string sexe, int pays)
        {
            // Empêché les injections
            nom = HttpUtility.HtmlEncode(nom);
            prenom = HttpUtility.HtmlEncode(prenom);
            char sex = 'm';
            Joueur joueur = new Joueur();
            if (sexe == "Féminin")
            {
                ViewBag.sex = 'f';
            }
            joueur.UpdateJoueur(idJoueur, nom, prenom, dateNaiss, sex, pays);
            return Redirect("~/Joueur/Joueur/" + idJoueur);
        }

        [Authorize]
        public ActionResult AjoutJoueurPoule(int id,int idC)
        {
            List<Joueur> listeJ = new Competition(idC).GetListPart();
            Session["idC"] = idC;
            Session["idPoule"] = id;
            Session["listJ"]= new SelectList(listeJ, "Id", "Identite");
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult AjoutJoueurPoule(int? joueur, int pos, int matchg,int matchp)
        {

            infoJoueur j = new infoJoueur{ position=pos,matchGagné=matchg,matchPerdu=matchp,Id=Convert.ToInt32(joueur) };
            DCJoueur dc = new DCJoueur();
            dc.InsertLD(j, (int)Session["idPoule"]);
            Session["idPoule"] = null;
            return Redirect("~/Competition/InfoComp/"+Session["idC"]);
        }



        [Authorize]
        public ActionResult ModifierJoueurPoule(int id, int idP, int idJ, int matchG, int matchP,int pos)
        {
            infoJoueur j = new infoJoueur
            {
                matchGagné = matchG,
                matchPerdu = matchP,
                position=pos,
                Id = idJ
            };
            Session["idC"] = id;
            ViewBag.idP = idP;
            return View(j);
        }


        [Authorize]
        [HttpPost]
        public ActionResult ModifierJpoule(int idP, int idJ, int pos,int matchG, int matchP)
        {
            infoJoueur j = new infoJoueur
            {
                matchGagné = matchG,
                matchPerdu = matchP,
                position=pos,
                Id = Convert.ToInt32(idJ)
            };
            
            DCJoueur dc = new DCJoueur();
            dc.UpdateLD(j, idP);
            return Redirect("~/Competition/InfoComp/"+Session["idC"]);
        }
        [Authorize]
        public ActionResult SupprimerJoueurPoule(int id,int idP,int idJ)
        {
            DCJoueur dc = new DCJoueur();
            dc.DeleteLD(idJ, idP);
            return Redirect("~/Competition/InfoComp/"+id);
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddPhoto(fichier model, int id)
        {
            string rep = @"D:\ProjetWeb\basePing\basePing\Content\image\";
            if (model.File != null && model.File.ContentLength > 0)
            {
                string[] ext = new string[] { "jpg", "jpeg", "png", "gif" };
                string path = "";
                bool trouve = false;
                int i = 0;
                string nomPhoto = Path.GetFileName(model.File.FileName);
                int index = nomPhoto.LastIndexOf('.');
                string newName = nomPhoto.Substring(index);
                newName = id + newName;
                do
                {
                    path = id + "." + ext[i];
                    if (System.IO.File.Exists(@"D:\ProjetWeb\basePing\basePing\Content\image\" + path))
                    {
                        System.IO.File.Delete(@"D:\ProjetWeb\basePing\basePing\Content\image\" + path);
                    }
                    i++;
                } while (!trouve && i < ext.Length);
                model.File.SaveAs(Path.Combine(rep, newName));
            }
            return RedirectToAction("/Joueur/"+id);
        }
        /////////////////////////////////////////////// Palmares ///////////////////////////////////////////////

        /// <summary>
        /// Mise à jour du palmarès d'un joueur
        /// </summary>
        /// <param name="idJoueur"></param>
        /// <param name="idPalmares"></param>
        /// <returns>Retourne un palmarès et l'id du joueur</returns>
        /// 

        [Authorize]
        public ActionResult UpdatePalmares(int idJoueur, int idPalmares)
        {
            PalmaresCivil palmares = new PalmaresCivil();
            palmares=palmares.GetPalmaresCivic(idPalmares);
            ViewBag.palmares = palmares;
            ViewBag.idJoueur = idJoueur;
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult UpdatePalmares(int idJoueur, int idPalmares, string recompense, DateTime date)
        {
            PalmaresCivil palmares = new PalmaresCivil();
            if(recompense != "" || date != null)
            {
                palmares.Update(idPalmares, recompense, date);
                return Redirect("~/Joueur/Joueur/" + idJoueur);
            }
            else
            {
                return Redirect("UpdatePalmares/"+idJoueur+"?error=Aucune information trouvée");
            }
        }


        [Authorize]
        public ActionResult Delete(int idJoueur,int idPalmares)
        {
            PalmaresCivil palmares = new PalmaresCivil();
            palmares.Delete(idPalmares);
            return Redirect("~/Joueur/Joueur/" + idJoueur);
        }


        [Authorize]
        public ActionResult AjoutPalmares(int idJoueur, string recompense, DateTime date)
        {
            PalmaresCivil palmares = new PalmaresCivil();
            recompense = HttpUtility.HtmlEncode(recompense);
            palmares.AjoutPalmares(idJoueur, recompense, date);
            return Redirect("~/Joueur/Joueur/" + idJoueur);
        }
    }
}