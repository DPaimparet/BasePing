using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class MatchEquipe
    {

        private int id;
        private Equipe e1;
        private Equipe e2;
        private int scoreE1;
        private int scoreE2;
        private int position;
        private List<Match> lMatch = new List<Match>();

        public int Id { get { return id; } set { id = value; } }
        public Equipe Equipe1 { get { return e1; } set { e1 = value; } }
        public Equipe Equipe2 { get { return e2; } set { e2 = value; } }
        public int Score1 { get { return scoreE1; } set { scoreE1 = value; } }
        public int Score2 { get { return scoreE2; } set { scoreE2 = value; } }
        public int Position { get { return position; } set { position = value; } }
        public string Info { get { return HttpUtility.HtmlDecode(e1.Nom + " |" + scoreE1 + " Contre " + e2.Nom + " |" + scoreE2); } }
        public List<Match> LSet { get { return lMatch; } set { lMatch = value; } }


        public MatchEquipe()
        {
        }

        public MatchEquipe(int id, Equipe e1, Equipe e2, int scoreE1, int scoreE2, int position)
        {
            this.id = id;
            this.e1 = e1;
            this.e2 = e2;
            this.scoreE1 = scoreE1;
            this.scoreE2 = scoreE2;
            this.position = position;
        }

        public MatchEquipe(int id, Equipe e1, Equipe e2, int scoreE1, int scoreE2)
        {
            this.id = id;
            this.e1 = e1;
            this.e2 = e2;
            this.scoreE1 = scoreE1;
            this.scoreE2 = scoreE2;
        }



        public MatchEquipe(int id)
        {
            this.id = id;

        }

        public MatchEquipe GetInformation()
        {
            DCEquipe dc = new DCEquipe();
            MatchEquipe m = dc.FindRencontre(id);
            return m;

        }

        //public List<MatchEquipe> GetListMatch(int idSerie)
        //{
        //    DCMatch dc = new DCMatch();
        //    List<MatchEquipe> match = new List<MatchEquipe>();
        //    foreach (MatchEquipe m in dc.findAllEquipe(idSerie))
        //    {
        //        m.Equipe1.GetListMembre();
        //        m.Equipe2.GetListMembre();
        //        match.Add(m);
        //    }
        //    return match;

        //}




        public List<MatchEquipe> GetMatchComp(int idC)
        {
            DCMatch dc = new DCMatch();
            return dc.FindMatchNotLinkedEquipe(idC);
        }

        internal List<MatchEquipe> GetMatchComp(int idC, int idE)
        {
            DCEquipe dc = new DCEquipe();
            return dc.FindMatchNotLinkedEquipe(idC, idE);
        }

        public List<MatchEquipe> GetListMatch(int idP, int idE)
        {
            DCEquipe dc = new DCEquipe();
            return dc.FindRencontreMatchPouleEquipe(idP,idE);
        }
    }
}