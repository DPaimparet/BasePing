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
        private string nbrJoueur;
        private CPays pays;
        private Categorie cat;
        private SousCategorie sousCat;
        private List<Poule> lPoule= new List<Poule>();
        private Tournoi tournoi=null;


        public int Id { get{ return id; } set { id = value; } }
        public string Nom { get { return HttpUtility.HtmlDecode(nom); } set { nom = value; } }
        public DateTime DateDeb { get { return dateDeb; } set { dateDeb = value; } }
        public DateTime DateFin { get { return dateFin; } set { dateFin = value; } }
        public CPays Pays { get { return pays;  } }
        

        public string TypeComp { get { return HttpUtility.HtmlDecode(typeComp); } set { typeComp = value; } }
        public string NbrJoueur { get { return HttpUtility.HtmlDecode(nbrJoueur); } set { nbrJoueur = value; } }
        public Categorie Cat { get { return cat; } set { cat = value; } }
        public SousCategorie SousCat { get { return sousCat; } set { sousCat = value; } }
        public List<Poule> LPoule{ get { return lPoule; } set { lPoule = value; } }
        public Tournoi Tournoi { get { return tournoi; } set { tournoi = value; } }


        public Competition(int id, string nom, DateTime dateDeb,DateTime dateFin, string typeComp,string nbrJoueur,Categorie cat, SousCategorie sousCat,CPays pays)
        {
            this.id = id;
            this.nom = nom;
            this.dateDeb = dateDeb;
            this.dateFin = dateFin;
            this.typeComp = typeComp;
            this.cat = cat;
            this.sousCat = sousCat;
            this.nbrJoueur = nbrJoueur;
            this.pays = pays;

        }

        public Competition()
        {
        }


        public Competition(int id)
        {
            this.id = id;
        }

        public Competition(int id, string nom, DateTime dateDeb, DateTime dateFin, string typeComp, string nbrJoueur,CPays pays)
        {

            this.id = id;
            this.nom = nom;
            this.dateDeb = dateDeb;
            this.dateFin = dateFin;
            this.typeComp = typeComp;
            this.nbrJoueur = nbrJoueur;
            this.pays = pays;

        }

        public Competition GetInformation()
        {
            DCCompetition dc = new DCCompetition();
            Competition comp= dc.find(id);
            
            Poule p = new Poule();
            comp.Tournoi = new DCTournoi().find(id);
            if (comp.NbrJoueur == "Individuel")
            {
                comp.LPoule = p.GetListPoule(id);
                
            }else if (comp.NbrJoueur == "Equipe")
            {

                comp.LPoule = p.GetListPouleEquipe(id);

            }
            if (comp.Tournoi!=null)
                comp.Tournoi.GetListMatch(comp.nbrJoueur);
            return comp;
        }

        internal List<Competition> GetListTrie(string nom, int? an, string sexe, string nbrJ,int idSousCat)
        {
            DCCompetition dc = new DCCompetition();
            return dc.FindListTrie(nom, an, sexe,nbrJ,idSousCat);
        }

        public String GetVainqueur()
        {
            DCJoueur dc = new DCJoueur();
            DCEquipe dc2 = new DCEquipe();
            Joueur j = null;
            Equipe e = null;
            if (nbrJoueur == "Individuel")
            {
                j = dc.FindVainqueur(id);
                if(j!=null)
                    return j.Identite;

            }
            else if (nbrJoueur == "Equipe")
            {
                e = dc2.FindVainqueur(id);
                if (e != null)
                    return e.Nom;
            }
            return "Pas de vainqueur désigné";
        }

        public List<Competition> GetList()
        {
            DCCompetition dc = new DCCompetition();
            return dc.findAll();
        }

        public List<Competition> GetListNoSousCat(int id)
        {
            DCCompetition dc = new DCCompetition();
            return dc.findAllCompNoCat(id);
        }
        public void GetTournoi()
        {
            tournoi = new DCTournoi().find(id);
        }

        public List<Joueur> GetListPart()
        {
            DCJoueur dc = new DCJoueur();
            return dc.findAllComp(id);
        }


        public List<Equipe> GetListEquipe()
        {
            DCEquipe dc = new DCEquipe();
            return dc.findAllComp(id);
        }
    }
}