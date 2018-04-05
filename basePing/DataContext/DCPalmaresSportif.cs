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
                string query = "SELECT ld.idComp, c.nom, ld.position, c.dateDeb FROM ld_joueur_comp ld INNER JOIN competition c ON ld.idComp = c.idComp  Where idJoueur ='" + idJoueur + "' AND position < 100 ORDER BY c.dateDeb DESC";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeRecompense.Add(new PalmaresSportif(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetDateTime(3))
                    );
                }
                reader.Close();

                string query2 = "SELECT C.idComp, C.nom, LD.position, C.dateDeb " +
                    "FROM competition C " +
                    "INNER JOIN ld_equipe_comp LD " +
                    "ON C.idComp = LD.idComp " +
                    "WHERE idE IN ( " +
                        "SELECT EC.idEquipe " +
                        "FROM equipe E " +
                        "INNER JOIN eclat_joueur_equipe EC " +
                        "ON E.idEquipe = EC.idEquipe " +
                        "WHERE idJoueur ='" + idJoueur + "') " +
                    "AND position < 100 ORDER BY c.dateDeb DESC";
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    listeRecompense.Add(new PalmaresSportif(
                        reader2.GetInt32(0),
                        reader2.GetString(1),
                        reader2.GetInt32(2),
                        reader2.GetDateTime(3))
                    );
                }

                reader2.Close();

                return listeRecompense;
            }
            else
                return null;
        }
    }
}