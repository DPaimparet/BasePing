using basePing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.DataContext;

namespace basePing.Controllers
{
    public class CompetitionEquipeController : Controller
    {
        // GET: CompetitionEquipe
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult InfoComp(int id)
        {
            Session["Url"] = Request.Url.LocalPath.ToString();
            Session["idComp"] = id;
            Competition comp = new Competition(id);

            comp = comp.GetInformation();
            Session["CPays"] = comp.Pays;
            return View(comp);
          
        }

        [Authorize]
        public ActionResult AjoutEquipe(int id)
        {
            List<CPays> listePays = new CPays().GetListPays();
            List<string> listePos = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8","9","10","Pas classé"};
            ViewBag.listePays = new SelectList(listePays, "Id", "Pays");
            ViewBag.listePos = new SelectList(listePos);
            Session["idComp"] = id;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjoutEquipe(string nom, int taille,int pays,string pos)
        {
            DCEquipe dc = new DCEquipe();
            int res;
            if(int.TryParse(pos,out res))
                dc.Create(nom,taille, pays,(int)Session["idComp"],res);
            else
                dc.Create(nom, taille, pays, (int)Session["idComp"], 100);



            return Redirect("~/CompetitionEquipe/InfoComp/" + Session["idComp"]);
        }

        [Authorize]
        public ActionResult SupprimerEquipe(int idE, int idC)
        {
            DCEquipe dc = new DCEquipe();
            dc.Delete(idE,idC);
            return Redirect("~/CompetitionEquipe/InfoComp/"+idC);
        }

        public ActionResult DetailEquipe(int idE, int idC)
        {
            Session["idComp"] = idC;
            Equipe e = new Equipe(idE);
            return View(e);
        }

        [Authorize]
        public ActionResult SupprimerMembre(int idJ,int idE)
        {
            DCEquipe dc = new DCEquipe();
            dc.DeleteLd(idJ,idE);
            return Redirect("~/CompetitionEquipe/DetailEquipe/");
        }

        [Authorize]
        public ActionResult AddMembre(int idE )
        {
           // ViewBag.idPays = idPays;
            Session["idE"] = idE;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ListeParticipant(string nom, int? pays, string sexe)
        {
            // Initialisation
            char sex = 'f';
            DCJoueur joueur = new DCJoueur();
            List<Joueur> listeJoueur = new List<Joueur>();
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
                    listeJoueur = joueur.GetJoueurByNameAndCountry(nom, pays);
                    if (listeJoueur.Count() != 0)
                    {
                        ViewBag.listeJoueur = listeJoueur;
                    }
                    else
                    {
                        return Redirect("~/CompetitionEquipe/AddMembre/" + Session["idE"] + "?error=Aucune information trouvée");
                    }
                }
                // Pays vide , sexe remplis
                if (pays == null && sexe != "")
                {
                    listeJoueur = joueur.GetJoueurByNameAndSex(nom, sex);
                    if (listeJoueur.Count() != 0)
                    {
                        ViewBag.listeJoueur = listeJoueur;
                    }
                    else
                    {
                        return Redirect("~/CompetitionEquipe/AddMembre/" + Session["idE"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/CompetitionEquipe/AddMembre/" + Session["idE"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/CompetitionEquipe/AddMembre/" + Session["idE"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/CompetitionEquipe/AddMembre/" + Session["idE"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/CompetitionEquipe/AddMembre/" + Session["idE"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/CompetitionEquipe/AddMembre/" + Session["idE"] + "?error=Aucune information trouvée");
                    }
                }
                if (pays == null && sexe == "") // Pays non sélectionné et Sexe non sélectionné
                {
                    return Redirect("~/CompetitionEquipe/AddMembre/" + Session["idE"] + "?error=Aucune information trouvée");
                }
            }
            return View();
        }


        public ActionResult AddToTeam(int idJ)
        {
            DCEquipe dc = new DCEquipe();
            if (!dc.VerifJoueurEquipe(idJ, (int)Session["idComp"]))
            {
                return Redirect("~/CompetitionEquipe/AddMembre/?idE=" + (int)Session["idE"] + "&error=Erreur: la personne fait déja partie de cette compétition");
            }
            if(dc.CreateLD(idJ,(int)Session["idE"]))
                return Redirect("~/CompetitionEquipe/AddMembre/?idE=" + (int)Session["idE"]);
            else
                return Redirect("~/CompetitionEquipe/AddMembre/?idE="+ (int)Session["idE"]+"&error=Erreur: la personne fait déja partie de cette équipe");
            
        }
    }
}