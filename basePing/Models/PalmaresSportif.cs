﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.Models
{
    public class PalmaresSportif
    {
        private int idCompetition;
        private string intituleCompetition;
        private int position;
        private DateTime annee;

        public int IdCompetition
        {
            get { return idCompetition; }
            set { idCompetition = value; }
        }
        public string IntituleCompetition
        {
            get { return intituleCompetition; }
            set { intituleCompetition = value; }
        }

        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        public DateTime Annee
        {
            get { return annee; }
            set { annee = value; }
        }
        public PalmaresSportif() { }
        public PalmaresSportif(int id, string intituleCompetition, int position, DateTime annee)
        {
            this.idCompetition = id;
            this.intituleCompetition = intituleCompetition;
            this.position = position;
            this.annee = annee;
        }
        public List<PalmaresSportif> GetList(int idJoueur)
        {
            List<PalmaresSportif> list = new List<PalmaresSportif>();
            DataContext.DCPalmaresSportif dCPalmaresSportif = new DataContext.DCPalmaresSportif();
            list = dCPalmaresSportif.GetAllRecompense(idJoueur);
            list = list.OrderByDescending(o => o.annee).ToList();
            return list;
        }
    }
}