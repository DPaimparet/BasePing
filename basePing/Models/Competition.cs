﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class Competition
    {
        private int id;
        private string nom;
        private DateTime dateDeb;
        private DateTime dateFin;
        private string typeComp;
        private string nbrJoueur;
        private Categorie cat;
        private SousCategorie sousCat;
        private List<Poule> lPoule= new List<Poule>();
        private Tournoi tournoi=null;


        public int Id { get{ return id; } set { id = value; } }
        public string Nom { get { return nom; } set { nom = value; } }
        public DateTime DateDeb { get { return dateDeb; } set { dateDeb = value; } }
        public DateTime DateFin { get { return dateFin; } set { dateFin = value; } }

        

        public string TypeComp { get { return typeComp; } set { typeComp = value; } }
        public string NbrJoueur { get { return nbrJoueur; } set { nbrJoueur = value; } }
        public Categorie Cat { get { return cat; } set { cat = value; } }
        public SousCategorie SousCat { get { return sousCat; } set { sousCat = value; } }
        public List<Poule> LPoule{ get { return lPoule; } set { lPoule = value; } }
        public Tournoi Tournoi { get { return tournoi; } set { tournoi = value; } }


        public Competition(int id, string nom, DateTime dateDeb,DateTime dateFin, string typeComp,string nbrJoueur,Categorie cat, SousCategorie sousCat)
        {
            this.id = id;
            this.nom = nom;
            this.dateDeb = dateDeb;
            this.dateFin = dateFin;
            this.typeComp = typeComp;
            this.cat = cat;
            this.sousCat = sousCat;
            this.nbrJoueur = nbrJoueur;

        }

        public Competition()
        {
        }


        public Competition(int id)
        {
            this.id = id;
        }

        public Competition(int id, string nom, DateTime dateDeb, DateTime dateFin, string typeComp, string nbrJoueur)
        {

            this.id = id;
            this.nom = nom;
            this.dateDeb = dateDeb;
            this.dateFin = dateFin;
            this.typeComp = typeComp;
            this.nbrJoueur = nbrJoueur;

        }

        public Competition GetInformation()
        {
            DCCompetition dc = new DCCompetition();
            Competition comp= dc.find(id);
            
            Poule p = new Poule();
            comp.Tournoi = new DCTournoi().find(id);
            comp.LPoule=p.GetListPoule(id);
            if(comp.Tournoi!=null)
                comp.Tournoi.GetListMatch(nbrJoueur);
            return comp;
        }

        public List<Competition> GetList()
        {
            DCCompetition dc = new DCCompetition();
            return dc.findAll();
        }
        public void GetTournoi()
        {
            tournoi = new DCTournoi().find(id);
        }

        public List<Joueur> GetListPart()
        {
            DCJoueur dc = new DCJoueur();
            return dc.findAllComp(id);
        }
    }
}