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

    }

}