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
        private List<PalmaresCivil> palmCivil;
        private List<PalmaresSportif> palmSportif;

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
        public List<PalmaresCivil> ListePalmCivil
        {
            get { return ListePalmCivil; }
        }
        public List<PalmaresSportif> ListePalmSportif
        {
            get { return ListePalmSportif; }
        }
        public Joueur(int id)
        {
            this.id = id;
            GetJoueur(id);
        }

        public Joueur GetJoueur(int id)
        {

        }
    }
}