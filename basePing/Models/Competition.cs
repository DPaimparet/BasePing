using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class Competition
    {
        private int id;
        private string nom;
        private DateTime dateDeb;
        private DateTime dateFin;
        private string typeComp;
        private Categorie cat;
        private List<Poule> lPoule= new List<Poule>();


        public int Id { get{ return id; } set { id = value; } }
        public string Nom { get { return nom; } set { nom = value; } }
        public DateTime DateDeb { get { return dateDeb; } set { dateDeb = value; } }
        public DateTime DateFin { get { return dateFin; } set { dateFin = value; } }
        public string TypeComp { get { return typeComp; } set { typeComp = value; } }
        public Categorie Cat { get { return cat; } set { cat = value; } }
        public List<Poule> LPoule{ get { return lPoule; } set { lPoule = value; } }


        public Competition(int id, string nom, DateTime dateDeb,DateTime dateFin, string typeComp,Categorie cat)
        {
            this.id = id;
            this.nom = nom;
            this.dateDeb = dateDeb;
            this.dateFin = dateFin;
            this.typeComp = typeComp;
            this.cat = cat;
        }

        public Competition()
        {
        }


        public Competition(int id)
        {
            this.id = id;
        }

        public Competition(int id, string nom, DateTime dateDeb, DateTime dateFin, string typeComp)
        {

            this.id = id;
            this.nom = nom;
            this.dateDeb = dateDeb;
            this.dateFin = dateFin;
            this.typeComp = typeComp;
        }

        public Competition GetInformation()
        {
            DCCompetition dc = new DCCompetition();
            Competition comp= dc.find(id);
            
            Poule p = new Poule();
            comp.LPoule=p.GetListPoule(id);
            return comp;
        }

        public List<Competition> GetList()
        {
            DCCompetition dc = new DCCompetition();
            return dc.findAll();
        }

       
    }
}