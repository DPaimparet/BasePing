using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.ViewModel
{
    public class VMParticipant
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string National { get; set; }

        public string Position { get; set; }
    }
}