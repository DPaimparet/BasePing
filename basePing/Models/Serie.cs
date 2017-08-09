using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.Models
{
    public class Serie
    {
        private int id;
        private string descriptif = "pas de description";

        public int Id {get{return id;}set{ id = value; }}
        public string Descriptif { get{ return descriptif; }set{ descriptif = value; } }

        public Serie(int id,string descriptif)
        {
            this.id = id;
            this.descriptif = descriptif;
        }

        public Serie()
        {
        }
    }
}