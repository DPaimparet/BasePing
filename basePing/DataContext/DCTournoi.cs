using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCTournoi
    {
        public Tournoi find(int idComp)
        {
            Tournoi t=null;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                
                string query = "SELECT Serie.*,tournoi.* FROM `competition` LEFT JOIN `serie` ON `serie`.`idComp` = `competition`.`idComp` LEFT JOIN `tournoi` ON `tournoi`.`idSerie` = `serie`.`idSerie` WHERE competition.idComp="+idComp+" AND tournoi.idSerie is not null";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    t = new Tournoi(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(4));
                }
                reader.Close();
                return t;
            }
            else
                return null;
        }

        public bool Create(int idComp,int taille, string desc)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                int id = 0;
                string query1 = "INSERT INTO `serie` (`idSerie`, `descriptif`, `idComp`) VALUES (NULL, 'Poule', '" + idComp+ "')";
                var cmd1 = new MySqlCommand(query1, con.Connection);
                var reader1 = cmd1.ExecuteReader();

                reader1.Close();



                string query2 = "SELECT MAX(idSerie) FROM serie";
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    id = reader2.GetInt32(0);
                }

                reader2.Close();


                string query3 = "INSERT INTO `Tournoi` (`idSerie`, `nbrTour`) VALUES ('" + id + "', '" + taille + "')";
                var cmd3 = new MySqlCommand(query3, con.Connection);
                var reader3 = cmd3.ExecuteReader();

                reader3.Close();
                return true;
            }
            else
                return false;
        }

        public bool DeleteLD(int idJ, int id)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "DELETE FROM ld_joueur_serie WHERE idJoueur=" + idJ + " AND idSerie=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();

                return true;
            }
            else
                return false;
        }
    }
}