using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class Categorie
    {
        private int id;
        private string nom;
        private string descriptif;
      

        public int Id { get { return id; } set { id = value; } }
        public string Nom { get { return nom; } set { nom = value; } }
        public string Descriptif { get { return descriptif; } set { descriptif = value; } }

        public Categorie(int id,string nom, string descriptif)
        {
            this.id = id;
            this.nom = nom;
            this.descriptif = descriptif;
        }

        public Categorie()
        {
        }

        public List<Categorie> GetList()
        {
            DCCategorie dc = new DCCategorie();
            return dc.findAll();
        }
    }
}