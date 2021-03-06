﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;
using basePing.Models;
using basePing.ViewModel;


namespace basePing.Models
{
    public class Poule:Serie
    {
        private int taille;
        private string nomPoule;
        private List<infoJoueur> lJoueur;
        private List<infoEquipe> lEquipe;

        public int Taille { get { return taille; } set { taille = value; } }
        public string NomPoule { get { return nomPoule; } set { nomPoule = value; } }
        public List<infoJoueur> LJoueur { get { return lJoueur; } set { lJoueur = value; } }
        public List<infoEquipe> LEquipe{ get { return lEquipe; } set { lEquipe = value; } }

        public Poule(int id,string descriptif,int taille,string nomPoule):base(id,descriptif)
        {
            this.taille = taille;
            this.nomPoule = nomPoule;
        }

        public Poule():base()
        {
        }

        public List<Poule> GetListPoule(int idComp)
        {
            DCPoule dc = new DCPoule();
            infoJoueur m = new infoJoueur();
            
            List<Poule> listP= dc.findAll(idComp);
            foreach (Poule p in listP)
            {
                p.LJoueur = m.GetListJoueur(p.Id);
            }
            return listP;
        }

        internal List<Poule> GetListPouleEquipe(int id)
        {
            DCPoule dc = new DCPoule();
            infoEquipe m = new infoEquipe();

            List<Poule> listP = dc.findAll(id);
            foreach (Poule p in listP)
            {
                p.LEquipe = m.GetListEquipe(p.Id);
            }
            return listP;
        }
    }
}