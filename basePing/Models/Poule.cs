using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;
using basePing.Models;
using basePing.ViewModel;


namespace basePing.Models
{
    public class Poule:Serie
    {
        private int taille;
        private string nomPoule;
        private List<infoJoueur> lJoueur;
        

        public int Taille { get { return taille; } set { taille = value; } }
        public string NomPoule { get { return nomPoule; } set { nomPoule = value; } }
        public List<infoJoueur> LJoueur { get { return lJoueur; } set { lJoueur = value; } }

        public Poule(int id,string descriptif,int taille,string nomPoule):base(id,descriptif)
        {
            this.taille = taille;
            this.nomPoule = nomPoule;
        }

        public Poule():base()
        {
        }

        public List<Poule> GetListPoule(int idComp)
        {
            DCPoule dc = new DCPoule();
            infoJoueur m = new infoJoueur();
            
            List<Poule> listP= dc.findAll(idComp);
            foreach (Poule p in listP)
            {
                p.LJoueur = m.GetListJoueur(p.Id);
            }
            return listP;
        }
    }
}