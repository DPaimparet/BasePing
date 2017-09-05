using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.Models;
using basePing.DataContext;
using basePing.ViewModel;

namespace basePing.Controllers
{
    public class CompetitionController : Controller
    {
        // GET: Competition
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetComp(int id)
        {
            Competition comp = new Competition();
            List<CPays> listePays = new CPays().GetListPays();
            Session["listePays"] = new SelectList(listePays, "Id", "Pays");
            List<Competition> triedList = new List<Competition>();
            foreach (Competition c in comp.GetList())
            {
                if (c.Cat.Id == id) {
                    c.GetTournoi();
                    triedList.Add(c);
                }
            }
            foreach(Categorie c in Categorie.GetList())
            {
                if (c.Id == id)
                    ViewBag.cat = c;
            }
            ViewBag.listComp = triedList;
            List<Competition> listCompNoCat = comp.GetListNoSousCat(id);
            ViewBag.listCompNoCat = listCompNoCat;
            return View();
        }

        public ActionResult InfoComp(int id)
        {
            Session["idComp"] = id;
            Competition comp = new Competition(id);
            comp = comp.GetInformation();           
            return View(comp);
        }

        public ActionResult AjouterTypeCompForm()
        {
            return View();
        }

        public ActionResult AjouterTypeComp(string nom,string desc)
        {
            DCCategorie dc = new DCCategorie();
            dc.Insert(nom, desc);
            return Redirect("~/Home/Connect");
        }

        public ActionResult ModifCat(int id)
        {
            ViewBag.idCat = id;
            return View();
        }

        public ActionResult SuppSousCat(int idC,int idSC)
        {
            DCSousCategorie dc = new DCSousCategorie();
            dc.Delete(idSC);

            return Redirect("GetComp/"+idC);
        }

        [HttpPost]
        public ActionResult ModifCat(int id, string nom , string desc)
        {
            DCCategorie dc = new DCCategorie();
            dc.Update(nom, desc,id);
            return Redirect("~/Home/Connect");
        }


        public ActionResult AjoutComp(int id,int idSC)
        {
            Session["idSC"] = idSC;
            List<CPays> listePays = new CPays().GetListPays();
            List<String> listeType = new List<String>();
            listeType.Add("Masculin");
            listeType.Add("Féminin");
            listeType.Add("Mixte");
            List<String> listeNbrJ= new List<String>();
            listeNbrJ.Add("Individuel");
            listeNbrJ.Add("Equipe");
            ViewBag.listePays = new SelectList(listePays, "Id", "Pays");
            ViewBag.listeType = new SelectList(listeType);
            ViewBag.listeNbrJ = new SelectList(listeNbrJ);
            ViewBag.idCat = id;
            return View();
        }




        [HttpPost]
        public ActionResult AjoutComp(String nom, DateTime dateD, DateTime dateF, string pays, string type, string nbrJ, int idCat)
        {
            DCCompetition dc = new DCCompetition();
            dc.Insert(nom, dateD, dateF, pays, type, nbrJ, idCat, (int)Session["idSC"]);
            return Redirect("~/Competition/GetComp/"+idCat);
        }


        public ActionResult ModifierComp(int id, string nom, DateTime dateD, DateTime dateF, string type, string nbrJ,int idc)
        {
            SousCategorie sc = new SousCategorie();
            List<SousCategorie> lSC = new List<SousCategorie>();
            lSC = sc.GetList(idc);
            List<CPays> listePays = new CPays().GetListPays();
            List<String> listeType = new List<String>();
            listeType.Add(type);
            listeType.Add("Masculin");
            listeType.Add("Féminin");
            listeType.Add("Mixte");
            List<String> listeNbrJ = new List<String>();
            listeNbrJ.Add(nbrJ);
            listeNbrJ.Add("Individuel");
            listeNbrJ.Add("Equipe");
            ViewBag.listeSC = new SelectList(lSC, "Id", "Nom");
            ViewBag.listePays = new SelectList(listePays, "Id", "Pays");
            ViewBag.listeType = new SelectList(listeType);
            ViewBag.listeNbrJ = new SelectList(listeNbrJ);
            ViewBag.Nom = nom;
            ViewBag.dateD = dateD.ToString();
            ViewBag.dateF = dateF.ToString();
            ViewBag.idComp = id;
            return View();
        }

        [HttpPost]
        public ActionResult ModifierComp(String nom, DateTime dateD, DateTime dateF, string pays, string type, string nbrJ,int idSC,int idComp)
        {
            DCCompetition dc = new DCCompetition();
            dc.Update(idComp,nom, dateD, dateF, pays, type, nbrJ,idSC);
            return Redirect("~/Competition/InfoComp/" + idComp);
        }


        public ActionResult SuppressionComp(int id)
        {
            DCCompetition dc = new DCCompetition();
            dc.Delete(id);
            return Redirect("~/Home/Connect");
        }
        public ActionResult AjoutParticipant()
        {
            // Liste des participants de la compétition
            int idC = Convert.ToInt32(Session["idComp"]);
            DCJoueur dCJoueur = new DCJoueur();
            Joueur participant = new Joueur();
            Joueur joueur = new Joueur();
            List<Joueur> listeParticipant = new List<Joueur>();
            foreach(Joueur j in participant.GetListJoueurComp(idC))
            {
                joueur=dCJoueur.GetJoueur(j.Id);
                listeParticipant.Add(joueur);
            }
            ViewBag.listeParticipant = listeParticipant;
            return View();
        }
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
                        return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"] + "?error=Aucune information trouvée");
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
                        return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"] + "?error=Aucune information trouvée");
                    }
                }
                if (pays == null && sexe == "") // Pays non sélectionné et Sexe non sélectionné
                {
                    return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"] + "?error=Aucune information trouvée");
                }
            }
            return View();
        }
        public ActionResult AddNewJoueur()
        {
            return View();
        }
        public ActionResult AddParticipant(int id)
        {
            // Ajouter le participant ici
            Joueur participant = new Joueur();
            int idC = Convert.ToInt32(Session["idComp"]);
            if(participant.AjouteParticipant(id,idC))
                return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"]);
            else
                return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"]+"?error=Ce joueur participe déjà a cette compétition");
        }
        public ActionResult SubJoueur(int id)
        {
            // Ajouter le participant ici
            Joueur participant = new Joueur();
            int idC = Convert.ToInt32(Session["idComp"]);
            participant.RetirerParticipant(id, idC);
            return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"]);
        }
        [HttpPost]
        public ActionResult AddJoueur(string nom, string prenom, string sexe, DateTime dateNaissance, int? pays)
        {
            // Initialisation
            char sex = 'f';
            // Vérification du sexe
            if (sexe == "Masculin")
            {
                sex = 'm';
            }
            string Pays = pays.ToString();
            Joueur joueur = new Joueur(0, nom, prenom, dateNaissance, sex, Pays);
            joueur.AjouterJoueur();
            return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"]);
        }

        public ActionResult SupprimerParticipant(int idJ,int idC)
        {
            DCJoueur dc = new DCJoueur();
            DCPoule dcP = new DCPoule();
            DCTournoi dcT = new DCTournoi();
            dc.DeletePart(idJ,idC);
            Competition comp = new Competition(idC);
            comp = comp.GetInformation();
            foreach(Poule p in comp.LPoule)
            {
                dcP.DeleteLD(idJ,p.Id);
            }
            if(comp.Tournoi!=null)
                dcT.DeleteLD(idJ,comp.Tournoi.Id);
            return Redirect("InfoComp/" + idC);
        }
    }
}