using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class MatchDouble
    {
        private int id;
        private Joueur j1;
        private Joueur j2;
        private Joueur j3;
        private Joueur j4;

        private int score1;
        private int score2;
        private int position;
        private List<Set> lSet = new List<Set>();

        public int Id { get { return id; } set { id = value; } }
        public Joueur Joueur1 { get { return j1; } set { j1 = value; } }
        public Joueur Joueur2 { get { return j2; } set { j2 = value; } }
        public Joueur Joueur3 { get { return j3; } set { j3 = value; } }
        public Joueur Joueur4 { get { return j4; } set { j4 = value; } }
        public int Score1 { get { return score1; } set { score1 = value; } }
        public int Score2 { get { return score2; } set { score2 = value; } }
        public int Position { get { return position; } set { position = value; } }
        public string Info { get { return HttpUtility.HtmlDecode(j1.Nom + " " + j1.Prenom + " |" + score1 + " Contre " + j2.Nom + " " + j2.Prenom + " |" + score2); } }
        public List<Set> LSet { get { return lSet; } set { lSet = value; } }


        public MatchDouble()
        {
        }

        public MatchDouble(int id, Joueur j1, Joueur j2,Joueur j3,Joueur j4, int scorej1, int scorej2, int position)
        {
            this.id = id;
            this.j1 = j1;
            this.j2 = j2;
            this.j3 = j3;
            this.j4 = j4;
            this.score1 = scorej1;
            this.score2 = scorej2;
            this.position = position;
        }

        public MatchDouble(int id, Joueur j1, Joueur j2, int scorej1, int scorej2)
        {
            this.id = id;
            this.j1 = j1;
            this.j2 = j2;
            this.score1 = scorej1;
            this.score2 = scorej2;
        }

        public List<Match> GetListMatch(int idSerie)
        {
            DCMatch dc = new DCMatch();
            List<Match> match = new List<Match>();
            foreach (Match m in dc.findAllIndiv(idSerie))
            {
                m.Joueur1.RecupererJoueur();
                m.Joueur2.RecupererJoueur();
                match.Add(m);
            }
            return match;

        }


        public List<Match> GetListMatch(int idSerie, int idJoueur)
        {
            DCMatch dc = new DCMatch();
            List<Match> match = new List<Match>();
            foreach (Match m in dc.findMatchJoueur(idSerie, idJoueur))
            {
                m.Joueur1.RecupererJoueur();
                m.Joueur2.RecupererJoueur();
                match.Add(m);
            }
            return match;

        }

        //public List<MatchDouble> GetMatchComp(int idC)
        //{
        //    DCMatch dc = new DCMatch();
        //    //return dc.FindMatchNotLinked(idC);
        //}

        //public List<MatchDouble> GetMatchComp(int idC, int idJ)
        //{
        //    DCMatch dc = new DCMatch();
        //    //return dc.FindMatchNotLinked(idC, idJ);
        //}


    }
}