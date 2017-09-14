using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class Equipe
    {

        private int id;
        private string nom;
        private CPays national;
        private List<Joueur> listJ = new List<Joueur>();
        private int taille;
   

        public int Id
        {
            get { return id; }
            set { id = value; }
        }




        public string Nom
        {
            get { return HttpUtility.HtmlDecode(nom); }
            set { nom = value; }
        }



        public CPays National
        {
            get { return national; }
            set { national = value; }
        }

        public List<Joueur> ListJ
        {
            get { return listJ; }
            set { listJ = value; }
        }


        public int Taille
        {
            get { return taille; }
            set { taille = value; }
        }



        public Equipe(int id, string nom, CPays pays, int taille)
        {
            this.id = id;
            this.nom = nom;
            this.national = pays;
            this.taille = taille;
        }

        public Equipe(int idE)
        {
            this.id = idE;
            Equipe dc = new DCEquipe().find(id);
            this.nom = dc.Nom;
            this.national = dc.National;
            this.taille = dc.Taille;
            this.listJ = dc.ListJ;
        }

        public List<Joueur> GetListMembre()
        {
            DCEquipe dc = new DCEquipe();
            listJ = dc.GetMembre(id);
            return listJ;
        }


    }
}