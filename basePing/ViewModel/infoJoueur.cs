using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.ViewModel
{
    public class infoJoueur
    {
        public string nom {get; set;}
        public int position { get; set; }
        public int matchGagné{ get; set; }
        public int matchPerdu { get; set; }
        public int Id { get; set; }

        public List<infoJoueur> GetListJoueur(int id)
        {
            DCJoueur dc = new DCJoueur();
            return dc.GetinfoPouleJoueur(id);
        }

        public void AjouterAuPoule(infoJoueur j, int idPoule)
        {
            List<infoJoueur> lJoueur = new infoJoueur().GetListJoueur(idPoule);
            int cpt = 1;
            foreach (infoJoueur joueur in lJoueur)
            {
                if (j.matchGagné - j.matchPerdu > joueur.matchGagné - joueur.matchPerdu)
                {
                    joueur.position++;

                }
                else
                    cpt++;
            }
            j.position = cpt;
            DCJoueur dc = new DCJoueur();
            foreach (infoJoueur joueur in lJoueur)
            {
                dc.UpdateLD(joueur, idPoule);
            }
            dc.InsertLD(j, idPoule);
        }

    }
}