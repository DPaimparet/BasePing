using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.Models
{
    public class PalmaresCivil
    {
        private int idPalmares;
        private string recompense;
        private DateTime annee;

        public int IdPalmares
        {
            get { return idPalmares; }
            set { idPalmares = value; }
        }

        public string Recompense
        {
            get { return recompense; }
            set { recompense = value; }
        }

        public DateTime Annee
        {
            get { return annee; }
            set { annee = value; }
        }
        public PalmaresCivil() { }
        public PalmaresCivil(int id, string recompense, DateTime date)
        {
            idPalmares = id;
            this.recompense = recompense;
            annee = date;
        }

        public PalmaresCivil GetPalmaresCivic(int id)
        {
            PalmaresCivil palmares = new PalmaresCivil();
            DataContext.DCPalmaresCivil dCPalmares = new DataContext.DCPalmaresCivil();
            return palmares = dCPalmares.GetPalmares(id);
        }

        public List<PalmaresCivil> GetListPalmaresCivic(int idJoueur)
        {
            List<PalmaresCivil> liste = new List<PalmaresCivil>();
            DataContext.DCPalmaresCivil dCPalmares = new DataContext.DCPalmaresCivil();
            return liste = dCPalmares.GetAllRecompense(idJoueur);
        }
        public void Update(int idPalmares, string recompense, DateTime date)
        {
            DataContext.DCPalmaresCivil dCPalmares = new DataContext.DCPalmaresCivil();
            dCPalmares.UpdatePalmares(idPalmares, recompense, date);
        }
        public void Delete(int idPalmares)
        {
            DataContext.DCPalmaresCivil dCPalmares = new DataContext.DCPalmaresCivil();
            dCPalmares.DeletePalmares(idPalmares);
        }
        public void AjoutPalmares(int idJoueur, string recompense, DateTime date)
        {
            DataContext.DCPalmaresCivil dCPalmares = new DataContext.DCPalmaresCivil();
            dCPalmares.AjoutPalmares(idJoueur, recompense, date);
        }
    }
}