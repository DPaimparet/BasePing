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
        private List<SousCategorie> lSC = new List<SousCategorie>();
      

        public int Id { get { return id; } set { id = value; } }
        public string Nom { get { return HttpUtility.HtmlDecode(nom); } set { nom = value; } }
        public List<SousCategorie> List { get { return lSC; } set { lSC = value; } }

        public Categorie(int id,string nom, string descriptif)
        {
            this.id = id;
            this.nom = nom;
            this.descriptif = descriptif;
        }

        public Categorie()
        {
        }


        public void GetListSousCat()
        {
            DCSousCategorie dc = new DCSousCategorie();
            lSC = dc.findAll(id);
        }
        public static List<Categorie> GetList()
        {
            DCCategorie dc = new DCCategorie();
            return dc.findAll();
        }
    }
}