using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class Match
    {
        private int id;
        private Joueur j1;
        private Joueur j2;
        private int scoreJ1;
        private int scoreJ2;
        private int position;
        private List<Set> lSet = new List<Set>();

        public int Id { get { return id; } set { id = value; } }
        public Joueur Joueur1 { get { return j1; } set { j1 = value; } }
        public Joueur Joueur2 { get { return j2; } set { j2 = value; } }
        public int Score1 { get { return scoreJ1; } set { scoreJ1 = value; } }
        public int Score2 { get { return scoreJ2; } set { scoreJ2 = value; } }
        public int Position { get { return position; }set { position = value; } }
        public string Info { get { return HttpUtility.HtmlDecode(j1.Nom + " " + j1.Prenom + " |" + scoreJ1 + " Contre " + j2.Nom + " " + j2.Prenom + " |" + scoreJ2); } }
        public List<Set> LSet { get { return lSet; } set { lSet = value; } }


        public Match()
        {
        }

        public Match(int id,Joueur j1,Joueur j2,int scorej1,int scorej2, int position)
        {
            this.id = id;
            this.j1 = j1;
            this.j2 = j2;
            this.scoreJ1 = scorej1;
            this.scoreJ2 = scorej2;
            this.position = position;
        }

        public Match(int id, Joueur j1, Joueur j2, int scorej1, int scorej2)
        {
            this.id = id;
            this.j1 = j1;
            this.j2 = j2;
            this.scoreJ1 = scorej1;
            this.scoreJ2 = scorej2;
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


        public List<Match> GetListMatch(int idSerie,int idJoueur)
        {
            DCMatch dc = new DCMatch();
            List<Match> match = new List<Match>();
            foreach (Match m in dc.findMatchJoueur(idSerie,idJoueur))
            {
                m.Joueur1.RecupererJoueur();
                m.Joueur2.RecupererJoueur();
                match.Add(m);
            }
            return match;

        }

        public List<Match> GetMatchComp(int idC)
        {
            DCMatch dc = new DCMatch();
            return dc.FindMatchNotLinked(idC);
        }

        internal List<Match> GetMatchComp(int idC, int idJ)
        {
            DCMatch dc = new DCMatch();
            return dc.FindMatchNotLinked(idC,idJ);
        }
    }
}