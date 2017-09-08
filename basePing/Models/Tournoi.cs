using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class Tournoi:Serie
    {
        private int taille;
        private List<Match> lMatch = new List<Match>();

        public int Taille { get { return taille; } set { taille = value; } }
        public List<Match> LMatch{ get { return lMatch; } set { lMatch= value; } }


        public Tournoi(int id, string descriptif, int taille) : base(id, descriptif)
        {
            this.taille = taille;
            
        }

        public Tournoi(int idComp) : base()
        {
            Tournoi t = new DCTournoi().find(idComp);
            taille = t.Taille;
            Id = t.Id;
            Descriptif = t.Descriptif;
        }


        public List<Match> GetListMatch(string type)
        {
            DCMatch dc = new DCMatch();
            List<Match> match = new List<Match>();
            match = dc.findAllIndiv(Id);
            foreach (Match m in match)
            {
                m.Joueur1.RecupererJoueur();
                m.Joueur2.RecupererJoueur();
            }
            lMatch=match;
            return match;
        }
        
        public List<Match> GetListColumn(int col)
        {
           

            List<Match> match = new List<Match>();
            foreach(Match m in lMatch)
            {
                if (m.Position == col)
                    match.Add(m);
            }
            return match;
        }

        public Match[] getArrayMatch()
        {
            int e = 1;
            for (int i = 0; i < taille; i++)
                e *= 2;
            Match[] tab = new Match[e-1];
            
            foreach(Match m in lMatch)
                tab[m.Position] = m;
            return tab;
        }
    }
}