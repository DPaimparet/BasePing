using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCPalmaresSportif
    {
        public List<PalmaresSportif> GetAllRecompense(int idJoueur)
        {
            List<PalmaresSportif> listeRecompense = new List<PalmaresSportif>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer le joueur gràce au nom du joueur
                string query = "SELECT c.nom, ld.position, c.dateDeb FROM ld_joueur_comp ld INNER JOIN competition c ON ld.idComp = c.idComp  Where idJoueur ='" + idJoueur + "' AND position < 100 ORDER BY c.dateDeb DESC";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeRecompense.Add(new PalmaresSportif(
                        reader.GetString(0),
                        reader.GetInt32(1),
                        reader.GetDateTime(2))
                    );
                }
                reader.Close();
                return listeRecompense;
            }
            else
                return null;
        }
    }
}