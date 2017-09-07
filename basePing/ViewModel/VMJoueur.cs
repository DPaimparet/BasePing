using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace basePing.ViewModel
{
    public class VMJoueur
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public DateTime DateNaissance { get; set; }
        [Required]
        public char Sexe { get; set; }
        public string National { get; set; }
    }
}