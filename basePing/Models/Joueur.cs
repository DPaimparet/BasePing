using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;
using basePing.ViewModel;

namespace basePing.Models
{
    public class Joueur
    {
        private int         id;
        private string      nom;
        private string      prenom;
        private DateTime dateNaissance;
        private char        sexe;
        private string      national;
        private DCJoueur dc;

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
        public char Sexe
        {
            get { return sexe; }
            set { sexe = value; }
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

        internal List<Joueur> GetListJoueur()
        {
            DCJoueur dc = new DCJoueur();

            return dc.GetAllJoueur();
        }

      

        //public List<PalmaresCivil> ListePalmCivil
        //{
        //    get { return ListePalmCivil; }
        //}
        //public List<PalmaresSportif> ListePalmSportif
        //{
        //    get { return ListePalmSportif; }
        //}
        public Joueur() { }
        public Joueur(int id, string nom, string prenom, DateTime dateNaissance,char sexe , string nationalite)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.sexe = sexe;
            this.dateNaissance = dateNaissance;
            this.national = nationalite;
        }

        public Joueur(int id)
        {
            this.id = id;

        }

         public void RecupererJoueur()
         {
            Joueur j = new DCJoueur().GetJoueur(id);
            this.nom = j.Nom;
            this.prenom = j.Prenom;
            this.sexe = j.Sexe;
            this.dateNaissance = j.DateNaissance;
            this.national = j.National;
        }

    

        public void AjouterJoueur()
        {
            DataContext.DCJoueur dCJoueur = new DataContext.DCJoueur();
            dCJoueur.AjoutJoueur(Nom, Prenom, DateNaissance, Sexe, National);
        }
    }
}
