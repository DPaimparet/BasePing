using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class Match
    {
        private Joueur j1;
        private Joueur j2;
        private int scoreJ1;
        private int scoreJ2;
        private int position;
        private List<Set> lSet = new List<Set>();

        public Joueur Joueur1 { get { return j1; } set { j1 = value; } }
        public Joueur Joueur2 { get { return j2; } set { j2 = value; } }
        public int Score1 { get { return scoreJ1; } set { scoreJ1 = value; } }
        public int Score2 { get { return scoreJ2; } set { scoreJ2 = value; } }
        public int Position { get { return position; }set { position = value; } }



        public Match()
        {
        }

        public Match(Joueur j1,Joueur j2,int scorej1,int scorej2, int position)
        {
            this.j1 = j1;
            this.j2 = j2;
            this.scoreJ1 = scorej1;
            this.scoreJ2 = scorej2;
            this.position = position;
        }

        public List<Match> GetListMatch(int idSerie)
        {
            DCMatch dc = new DCMatch();
            List<Match> match = new List<Match>();
            foreach(Match m in dc.findAllIndiv(idSerie))
            {
                m.Joueur1.RecupererJoueur();
                m.Joueur2.RecupererJoueur();
                match.Add(m);
            }
            return match;

        }
    }
}