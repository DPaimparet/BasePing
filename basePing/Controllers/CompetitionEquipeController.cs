using basePing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.DataContext;
using basePing.ViewModel;

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
            e.RecupererEquipe();
            return View(e);
        }

        [Authorize]
        public ActionResult SupprimerMembre(int idJ,int idE)
        {
            DCEquipe dc = new DCEquipe();
            dc.DeleteLd(idJ,idE);
            return Redirect("~/CompetitionEquipe/DetailEquipe?idE="+idE+"&idC="+Session["idComp"]);
        }

        [Authorize]
        public ActionResult ModifierNomForm(int idE)
        {
            Session["idE"] = idE;
  
            return View();
        }


        [Authorize]
        public ActionResult ModifierNom(string nom)
        {
            DCEquipe dc = new DCEquipe();
            dc.UpdateNom((int)Session["idE"],nom);
            return Redirect("~/CompetitionEquipe/DetailEquipe?idE=" + (int)Session["idE"] + "&idC=" + Session["idComp"]);
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

        [Authorize]
        public ActionResult AjoutEquipePoule(int id, int idC)
        {
            List<Equipe> listeE = new Equipe(idC).GetListEquipeComp(idC);
            Session["idC"] = idC;
            Session["idPoule"] = id;
            Session["listE"] = new SelectList(listeE, "Id", "Nom");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjoutEquipePoule(int? equipe, int pos, int matchg, int matchp)
        {

            infoEquipe j = new infoEquipe { position = pos, matchGagné = matchg, matchPerdu = matchp, Id = Convert.ToInt32(equipe) };
            DCEquipe dc = new DCEquipe();
            if (dc.InsertLD(j, (int)Session["idPoule"], (int)Session["idC"]))
            {
                Session["idPoule"] = null;
                return Redirect("~/CompetitionEquipe/InfoComp/" + Session["idC"]);
            }
            else
            {
                return Redirect("~/CompetitionEquipe/AjoutEquipePoule/"+(int)Session["idPoule"]+"?idC="+ (int)Session["idC"] + "&error=Erreur cette equipe est déja inscrite dans un poule");
            }
          
        }



        [Authorize]
        public ActionResult ModifierEquipePoule(int id, int idP, int idE, int matchG, int matchP, int pos)
        {
            infoEquipe e = new infoEquipe
            {
                matchGagné = matchG,
                matchPerdu = matchP,
                position = pos,
                Id = idE
            };
            Session["idC"] = id;
            ViewBag.idP = idP;
            return View(e);
        }


        [Authorize]
        [HttpPost]
        public ActionResult ModifierEquipePoule(int idP, int idE, int pos, int matchG, int matchP)
        {
            infoEquipe e = new infoEquipe
            {
                matchGagné = matchG,
                matchPerdu = matchP,
                position = pos,
                Id = idE
            };

            DCEquipe dc = new DCEquipe();
            dc.UpdateLD(e, idP);
            return Redirect("~/CompetitionEquipe/InfoComp/" + Session["idC"]);
        }


        [Authorize]
        public ActionResult SupprimerEquipePoule(int id, int idP, int idE)
        {
            DCEquipe dc = new DCEquipe();
            dc.DeleteLD(idE, idP);
            return Redirect("~/CompetitionEquipe/InfoComp/" + id);
        }


        [Authorize]
        public ActionResult LieMatch(int pos, int idC, int idS)
        {
            Session["pos"] = pos;
            Session["idC"] = idC;
            Session["idS"] = idS;
            List<Equipe> listE= new Equipe().GetListEquipeComp(idC);
            
            List<MatchEquipe> listM = new MatchEquipe().GetMatchComp(idC);
            foreach (MatchEquipe m in listM)
            {
                m.Equipe1.RecupererEquipe();
                m.Equipe2.RecupererEquipe();
            }
            Session["listE"] = new SelectList(listE, "Id", "Nom");
            Session["listM"] = new SelectList(listM, "Id", "Info");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult LieRencontre(int match)
        {
            DCEquipe dc = new DCEquipe();
            dc.LinkMatch((int)Session["pos"], (int)Session["idS"], (int)Session["idC"],match);
            return Redirect("~/CompetitionEquipe/InfoComp/" + Session["idC"]);
        }

        [Authorize]
        public ActionResult LieRencontrePoule(int idE, int idC, int idS)
        {
            Session["idC"] = idC;
            Session["idS"] = idS;
            Session["pos"] = 0;
            Equipe eToRemove = null;
            List<Equipe> listE = new Equipe().GetListEquipeComp(idC);
            foreach (Equipe e in listE)
            {
                if (e.Id == idE)
                    eToRemove = e;
            }
            listE.Remove(eToRemove);
            List<MatchEquipe> listM = new MatchEquipe().GetMatchComp(idC,idE);
            foreach (MatchEquipe m in listM)
            {
                m.Equipe1.RecupererEquipe();
                m.Equipe2.RecupererEquipe();
            }
            Session["listE"] = new SelectList(listE, "Id", "Nom");
            Session["listM"] = new SelectList(listM, "Id", "Info");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult LieRencontrePoule(int match)
        {
            DCMatch dc = new DCMatch();
            dc.LinkMatch((int)Session["pos"], (int)Session["idS"], (int)Session["idC"],match);
            return Redirect("~/CompetitionEquipe/InfoComp/" + Session["idC"]);
        }


        [Authorize]
        [HttpPost]
        public ActionResult AjoutEtLieMatch(int? equipe1, int score1, int? equipe2, int score2)
        {
            DCEquipe dc = new DCEquipe();

            if (equipe1 == equipe2)
                return Redirect("/CompetitionEquipe/LieMatch?pos=" + (int)Session["pos"] + "&idC=" + (int)Session["idC"] + "&idS=" + (int)Session["idS"] + "&error=Les 2 joueurs choisis sont le même.");
            else
            {
                dc.Create(equipe1, score1, equipe2, score2, (int)Session["pos"], (int)Session["idS"], (int)Session["idC"]);
                return Redirect("~/CompetitionEquipe/InfoComp/" + Session["idC"]);
            }
        }

        [Authorize]
        public ActionResult SuppRencontreEquipe(int id)
        {
            DCEquipe dc = new DCEquipe();
            dc.DeleteMatch(id);

            return Redirect("~/Serie/ListRencontrePouleEquipe?idP="+Session["idS"] +"&idC=" + (int)Session["idC"]+"&IdE="+ (int)Session["idE"]);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjoutEtLieRencontrePoule(int score1, int? equipe, int score2)
        {
            DCEquipe dc = new DCEquipe();
            dc.Create((int)Session["idE"], score1, equipe, score2, 0, (int)Session["idS"], (int)Session["idC"]);
            return Redirect("~/Serie/ListRencontrePouleEquipe?idP=" + Session["idS"] + "&idC=" + (int)Session["idC"] + "&IdE=" + (int)Session["idE"]);
        }


        
        public ActionResult InfoRencontre(int id)
        {
            
            MatchEquipe m = new MatchEquipe(id).GetInformation();
            List<Match> list = new List<Match>();
            MatchDouble[] tab = null;
            List<MatchDouble> listD = new List<MatchDouble>();
            List<MatchDouble> TriedListD = new List<MatchDouble>();

            m.Equipe1.RecupererEquipe();
            m.Equipe2.RecupererEquipe();
            Session["idMatch"] = id;
            Session["idE1"] = m.Equipe1.Id;
            Session["idE2"] = m.Equipe2.Id;
            foreach (Joueur j in m.Equipe1.ListJ)
            {
                list.AddRange(j.GetMatchSerie((int)Session["idS"]));
                listD.AddRange(j.GetMatchDoubleSerie((int)Session["idS"]));

              
            }

            tab=listD.ToArray();

            for(int i=0; i < tab.Length; i++)
            {
                for(int j = i + 1; j < tab.Length; j++)
                {
                    if (tab[j] != null && tab[i].Id == tab[j].Id)
                        tab[j] = null;

                }
            }

            listD = tab.ToList<MatchDouble>();
 
            foreach(MatchDouble md in listD)
            {
                if (md != null)
                    TriedListD.Add(md);
            }



            foreach (Match match in list)
            {
                match.Joueur1.RecupererJoueur();
                match.Joueur2.RecupererJoueur();
            }

            foreach (MatchDouble match in TriedListD)
            {
                match.Joueur1.RecupererJoueur();
                match.Joueur2.RecupererJoueur();
                match.Joueur3.RecupererJoueur();
                match.Joueur4.RecupererJoueur();
            }


            ViewBag.listM = list;
            ViewBag.listMD = TriedListD;
            return View(m);
        }



        public ActionResult InfoRencontrePF(int id,int idS)
        {
            Session["idMatch"] = id;

            Session["idS"] = idS;
            MatchEquipe m = new MatchEquipe(id).GetInformation();
            List<Match> list = new List<Match>();
            MatchDouble[] tab = null;
            List<MatchDouble> listD = new List<MatchDouble>();
            List<MatchDouble> TriedListD = new List<MatchDouble>();
            m.Equipe1.RecupererEquipe();
            m.Equipe2.RecupererEquipe();

            Session["idE1"] = m.Equipe1.Id;
            Session["idE2"] = m.Equipe2.Id;
            foreach (Joueur j in m.Equipe1.ListJ)
            {
                list.AddRange(j.GetMatchSerie(idS));
                listD.AddRange(j.GetMatchDoubleSerie(idS));

            }
            foreach (Match match in list)
            {
                match.Joueur1.RecupererJoueur();
                match.Joueur2.RecupererJoueur();
            }

            tab = listD.ToArray();

            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = i + 1; j < tab.Length; j++)
                {
                    if (tab[j] != null && tab[i].Id == tab[j].Id)
                        tab[j] = null;

                }
            }

            listD = tab.ToList<MatchDouble>();

            foreach (MatchDouble md in listD)
            {
                if (md != null)
                    TriedListD.Add(md);
            }

            foreach (MatchDouble match in TriedListD)
            {
                match.Joueur1.RecupererJoueur();
                match.Joueur2.RecupererJoueur();
                match.Joueur3.RecupererJoueur();
                match.Joueur4.RecupererJoueur();
            }

            ViewBag.listMD = TriedListD;

            ViewBag.listM = list;
            return View(m);
        }
    }
}