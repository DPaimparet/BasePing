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


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public List<Joueur> GetListJoueurComp(int idC)
        {
           
            DCJoueur dc = new DCJoueur();
            List<Joueur> j = dc.findAllComp(idC);
            return j;

        }

        public string Nom
        {
            get { return HttpUtility.HtmlDecode(nom); }
            set { nom = value; }
        }
        public string Prenom
        {
            get { return HttpUtility.HtmlDecode(prenom); }
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
            Joueur j = new DataContext.DCJoueur().GetJoueur(id);
            this.nom = j.Nom;
            this.prenom = j.Prenom;
            this.sexe = j.Sexe;
            this.dateNaissance = j.DateNaissance;
            this.national = j.National;
        }
        public Joueur RecupererJoueur(int id)
        {
            Joueur j = new DataContext.DCJoueur().GetJoueur(id);
            return j;
        }
        public void AjouterJoueur()
        {
            DataContext.DCJoueur dCJoueur = new DataContext.DCJoueur();
            dCJoueur.AjoutJoueur(Nom, Prenom, DateNaissance, Sexe, National);
        }

        public void DeleteJoueur(int id)
        {
            DataContext.DCJoueur dCJoueur = new DCJoueur();
            dCJoueur.DeleteJoueur(id);
        }
        public void UpdateJoueur(int idJoueur, string nom, string prenom, DateTime dateNaiss, char sexe, int pays)
        {
            DataContext.DCJoueur dCJoueur = new DCJoueur();
            dCJoueur.UpdateJoueur(idJoueur,nom,prenom,dateNaiss,sexe,pays);
        }

        public bool AjouteParticipant(int idJoueur, int idCompetition)
        {
            DataContext.DCCompetition participant = new DataContext.DCCompetition();
            return participant.insertJoueurIntoComp(idJoueur, idCompetition);
        }
        public void RetirerParticipant(int idJoueur, int idCompetition)
        {
            DataContext.DCCompetition participant = new DataContext.DCCompetition();
            participant.DeleteJoueurInComp(idJoueur, idCompetition);
        }
        
    }
}
