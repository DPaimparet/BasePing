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

        [Authorize]
        public ActionResult AjouterSousCatForm()
        {
            return View();
        }

     
        public ActionResult AjouterSousCat(string nom)
        {
            DCSousCategorie dc = new DCSousCategorie();
            dc.Insert(HttpUtility.HtmlEncode(nom),(int)Session["idC"]);
            return Redirect("SousCategorie/" + Session["idC"]);
        }

        
        public ActionResult SousCategorie(int id)
        {
            Session["idC"] = id;
            SousCategorie Scat = new SousCategorie();
            Categorie cat = new DCCategorie().find(id);
            ViewBag.Cat = cat;
            ViewBag.sousCat=Scat.GetList(id);
            return View();
        }

        public ActionResult GetComp(int id)
        {
            
            Session["idSC"] = id;
            
            Competition comp = new Competition();
            if (id != -1) { 
            List<CPays> listePays = new CPays().GetListPays();
            Session["listePays"] = new SelectList(listePays, "Id", "Pays");
            List<Competition> triedList = new List<Competition>();
            foreach (Competition c in comp.GetList())
            {
                if (c.SousCat.Id== id) {
                    c.GetTournoi();
                    triedList.Add(c);
                }
            }
            foreach(Categorie c in Categorie.GetList())
            {
                if (c.Id == (int)Session["idC"])
                    ViewBag.cat = c;
            }
            ViewBag.listComp = triedList;
            }
            return View();
        }

        public ActionResult InfoComp(int id)
        {
            Session["Url"] = Request.Url.LocalPath.ToString();
            Session["idComp"] = id;
            Competition comp = new Competition(id);
            
            comp = comp.GetInformation();
            Session["CPays"] = comp.Pays;
            if (comp.NbrJoueur== "Individuel")
                return View(comp);
            else if (comp.NbrJoueur == "Equipe")
                return Redirect("~/CompetitionEquipe/InfoComp/"+id);
            return null;
        }

        [Authorize]
        public ActionResult AjouterTypeCompForm()
        {
            return View();
        }

        [Authorize]
        public ActionResult AjouterTypeComp(string nom,string desc)
        {
            DCCategorie dc = new DCCategorie();
            dc.Insert(nom, desc);
            return Redirect("~/Home/Connect");
        }

        [Authorize]
        public ActionResult ModifCat(int id)
        {
            ViewBag.idCat = id;
            return View();
        }

        [Authorize]
        public ActionResult SuppSousCat(int idSC)
        {
            DCSousCategorie dc = new DCSousCategorie();
            dc.Delete(idSC);

            return Redirect("SousCategorie/"+Session["idC"]);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ModifCat(int id, string nom , string desc)
        {
            DCCategorie dc = new DCCategorie();
            dc.Update(HttpUtility.HtmlEncode(nom), HttpUtility.HtmlEncode(desc),id);
            return Redirect("~/Home/Connect");
        }

        [Authorize]
        public ActionResult SuppCat(int id)
        {
            DCCategorie dc = new DCCategorie();
            dc.Delete(id);
            return Redirect("~/Home/Connect");
        }

        [Authorize]
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



        [Authorize]
        [HttpPost]
        public ActionResult AjoutComp(String nom, DateTime dateD, DateTime dateF, string pays, string type, string nbrJ, int idCat)
        {
            DCCompetition dc = new DCCompetition();
            dc.Insert(HttpUtility.HtmlEncode(nom), dateD, dateF, pays, HttpUtility.HtmlEncode(type), HttpUtility.HtmlEncode(nbrJ), idCat, (int)Session["idSC"]);
            return Redirect("~/Competition/GetComp/"+ Session["idSC"]);
        }

        [Authorize]
        public ActionResult ModifierComp(int id, string nom, DateTime dateD, DateTime dateF, string type, string nbrJ,int idc)
        {
            SousCategorie sc = new SousCategorie();
            List<SousCategorie> lSC = new List<SousCategorie>();
            lSC = sc.GetList(idc);
            List<CPays> listePays = new CPays().GetListPays();
            listePays.Insert(0, (CPays)Session["CPays"]);
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
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.idComp = id;
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult ModifierComp(String nom, DateTime dateD, DateTime dateF, string pays, string type, string nbrJ,int idSC,int idComp)
        {
            DCCompetition dc = new DCCompetition();
            dc.Update(idComp, HttpUtility.HtmlEncode(nom), dateD, dateF, pays, HttpUtility.HtmlEncode(type), HttpUtility.HtmlEncode(nbrJ),idSC);
            return Redirect("~/Competition/InfoComp/" + idComp);
        }

        [Authorize]
        public ActionResult SuppressionComp(int id)
        {
            DCCompetition dc = new DCCompetition();
            dc.Delete(id);
            return Redirect("~/Home/Connect");
        }



        [Authorize]
        public ActionResult AjoutParticipant()
        {
            // Liste des participants de la compétition
            int idC = Convert.ToInt32(Session["idComp"]);
            DCJoueur dCJoueur = new DCJoueur();
            Joueur participant = new Joueur();
            Joueur joueur = new Joueur();
            List<VMParticipant> listeParticipant = new List<VMParticipant>();
            foreach(Joueur j in participant.GetListJoueurComp(idC))
            {
                VMParticipant mParticipant = new VMParticipant();
                joueur =dCJoueur.GetJoueur(j.Id);
                mParticipant.Id = joueur.Id;
                mParticipant.Nom = joueur.Nom;
                mParticipant.Prenom = joueur.Prenom;
                mParticipant.National = joueur.National;
                mParticipant.Position = Convert.ToString(dCJoueur.GetPositionComp(joueur.Id, idC));
                if(mParticipant.Position == "100")
                {
                    mParticipant.Position = "Pas classé(e)";
                }
                listeParticipant.Add(mParticipant);
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




        [Authorize]
        public ActionResult AddNewJoueur()
        {
            return View();
        }



        [Authorize]
        public ActionResult AddParticipant(int id, int position)
        {
            // Ajouter le participant ici
            Joueur participant = new Joueur();
            int idC = Convert.ToInt32(Session["idComp"]);
            if(participant.AjouteParticipant(id,idC, position))
                return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"]);
            else
                return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"]+"?error=Ce joueur participe déjà a cette compétition");
        }


        [Authorize]
        public ActionResult AddPosition(int id)
        {
            Joueur participant = new Joueur();
            ViewBag.participant = participant.RecupererJoueur(id);
            return View();
        }


        [Authorize]
        public ActionResult FinalizeParticipation(int id, string position)
        {
            Joueur participant = new Joueur();
            if (position == "Pas classé(e)")
                position = "100";
            int Position = Convert.ToInt32(position);
            int idC = Convert.ToInt32(Session["idComp"]);
            if (participant.AjouteParticipant(id, idC, Position))
                return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"]);
            else
                return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"] + "?error=Ce joueur participe déjà a cette compétition");
        }


        [Authorize]
        public ActionResult SubJoueur(int id)
        {
            // Ajouter le participant ici
            Joueur participant = new Joueur();
            int idC = Convert.ToInt32(Session["idComp"]);
            participant.RetirerParticipant(id, idC);
            return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"]);
        }


        [Authorize]
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
            Joueur joueur = new Joueur(0, HttpUtility.HtmlEncode(nom), HttpUtility.HtmlEncode(prenom), dateNaissance, sex, Pays);
            joueur.AjouterJoueur();
            return Redirect("~/Competition/AjoutParticipant/" + Session["idComp"]);
        }




        [Authorize]
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