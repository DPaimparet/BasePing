using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.Models
{
    public class Federation
    {
        private int     id;
        private string  paysFederation;
        private string  nomFederation;
        private string  web;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string PaysFederation
        {
            get { return paysFederation; }
            set { paysFederation = value; }
        }
        public string NomFederation
        {
            get { return nomFederation; }
            set { nomFederation = value; }
        }

        public  string Web
        {
            get { return web; }
            set { web = value; }
        }

        public Federation() { }
        public Federation(int id, string paysFederation, string nomFederation, string web)
        {
            this.id = id;
            this.paysFederation = paysFederation;
            this.nomFederation = nomFederation;
            this.web = web;
        }
        public List<Federation> GetList()
        {
            List<Federation> liste = new List<Federation>();
            DataContext.DCFederation federation = new DataContext.DCFederation();
            return liste = federation.GetAllFederation();
        }
        public Federation GetFederation(int id)
        {
            Federation federation = new Federation();
            DataContext.DCFederation fede = new DataContext.DCFederation();
            return federation = fede.GetFederation(id);
        }
        public void AddFederation(string nomFederation, string pays, string web)
        {
            DataContext.DCFederation federation = new DataContext.DCFederation();
            string Pays = federation.GetPays(pays);
            federation.AddFederation(nomFederation , Pays, web);
        }
        public void DeleteFederation(int id)
        {
            DataContext.DCFederation federation = new DataContext.DCFederation();
            federation.DelFederation(id);
        }
        public void UpdateFederation(int id, string nomFedePays, int? pays, string web)
        {
            DataContext.DCFederation federation = new DataContext.DCFederation();
            string Pays = federation.GetPays(pays);
            federation.UpdateFederation(id,nomFedePays,Pays,web);
        }
    }

}