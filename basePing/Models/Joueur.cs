using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.Models
{
    public class Joueur
    {
        private int id;
        private string nom;
        private string prenom;
        private DateTime dateNaissance;
        private string national;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }
        public DateTime DateNaissance
        {
            get { return dateNaissance; }
            set { dateNaissance = value; }
        }
        public string National
        {
            get { return national; }
            set { national = value; }
        }

        public Joueur() { }
        public Joueur(int id, string nom, string prenom, DateTime dateNaissance, string nationalite)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.national = nationalite;
        }
    }
}