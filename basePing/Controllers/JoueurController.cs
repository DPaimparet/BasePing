﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basePing.Models;
using basePing.DataContext;

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
            player = joueur.GetJoueur(id);
            ViewBag.dateNaissance =player.DateNaissance.ToString("dddd, le dd MMMM yyyy ");
            if(player.Sexe == 'f')
            {
                ViewBag.sexe = "Féminin";
            }
            else
            {
                ViewBag.sexe = "Masculin";
            }
            ViewBag.Joueur = player;
            return View();
        }
    }
}