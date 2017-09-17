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
        private List<MatchEquipe> lMatchEquipe = new List<MatchEquipe>();


        public int Taille { get { return taille; } set { taille = value; } }
        public List<Match> LMatch{ get { return lMatch; } set { lMatch= value; } }
        public List<MatchEquipe> LMatchEquipe { get { return lMatchEquipe; } set { lMatchEquipe = value; } }



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


        public dynamic GetListMatch(string type)
        {
            if (type == "Individuel") {
                DCMatch dc = new DCMatch();
                List<Match> match = new List<Match>();
                match = dc.findAllIndiv(Id);
                foreach (Match m in match)
                {
                    m.Joueur1.RecupererJoueur();
                    m.Joueur2.RecupererJoueur();
                }
                lMatch = match;
                return match;
            }
            else if (type == "Equipe")
            {
                DCEquipe dc = new DCEquipe();
                List<MatchEquipe> match = new List<MatchEquipe>();
                match = dc.findAllMatchTournoi(Id);
                foreach (MatchEquipe m in match)
                {
                    m.Equipe1.RecupererEquipe();
                    m.Equipe2.RecupererEquipe();

                }
                lMatchEquipe = match;
                return match;
            }
            return null;
           
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


        public MatchEquipe[] getArrayMatchEquipe()
        {
            int e = 1;
            for (int i = 0; i < taille; i++)
                e *= 2;
            MatchEquipe[] tab = new MatchEquipe[e - 1];

            foreach (MatchEquipe m in lMatchEquipe)
                tab[m.Position] = m;
            return tab;
        }
    }
}