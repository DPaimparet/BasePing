using basePing.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.Models
{
    public class SousCategorie
    {
        private int id;
        private string nom;



        public int Id { get { return id; } set { id = value; } }
        public string Nom { get { return nom; } set { nom = value; } }

        public SousCategorie(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }

        public SousCategorie()
        {
        }

        public static List<SousCategorie> GetList(int id)
        {
            DCSousCategorie dc = new DCSousCategorie();
            return dc.findAll(id);
        }
    }
}