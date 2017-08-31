using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class CPays
    {
        private int id;
        private string codePays;
        private string pays;

        public int Id { get { return id; } set{ id = value; } }
        public string CodePays { get; set; }
        public string Pays { get {return pays; } set { pays = value; } }

        public string this[int i]
        {
            get {
                if(this.id==i)
                    return pays;
                return null;
            }
        }

        public CPays() { }
        public CPays(int id,string codePays, string pays)
        {
            this.id = id;
            this.codePays = codePays;
            this.pays = pays;
        }

        public List<CPays> GetListPays()
        {
            DCPays pays = new DCPays();
            List<CPays> listePays = pays.GetAllPays();
            return listePays;
        }

        public override string ToString()
        {
            return Pays;
        }
    }
}