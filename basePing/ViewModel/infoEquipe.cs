using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;


namespace basePing.ViewModel
{
    public class infoEquipe
    {

        public string nom { get; set; }
        public int position { get; set; }
        public int matchGagné { get; set; }
        public int matchPerdu { get; set; }
        public int Id { get; set; }

        public List<infoEquipe> GetListEquipe(int id)
        {
            DCEquipe dc = new DCEquipe();
            return dc.GetinfoPouleEquipe(id);
        }
    }
}