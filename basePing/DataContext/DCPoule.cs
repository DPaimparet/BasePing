using basePing.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.DataContext
{
    public class DCPoule
    {
        public List<Poule> findAll(int id)
        {
            List<Poule> lPoule = new List<Poule>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Serie INNER JOIN Poule on Serie.idSerie=Poule.idSerie WHERE idComp="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lPoule.Add(new Poule(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(4),reader.GetString(5)));
                }
                reader.Close();
                return lPoule;
            }
            else
                return null;
        }

        public bool Create(string nom, int taille, int idComp)
        {

            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                int id=0;
                string query1 = "INSERT INTO `serie` (`idSerie`, `descriptif`, `idComp`) VALUES (NULL, 'Poule', '"+idComp+"')";
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


                string query3 = "INSERT INTO `poule` (`idSerie`, `taillePoule`, `nomPoule`) VALUES ('"+id+"', '"+taille+"', '"+nom+"')";
                var cmd3 = new MySqlCommand(query3, con.Connection);
                var reader3= cmd3.ExecuteReader();

                reader3.Close();
                return true;
            }
            else
                return false;
        }

        public bool Delete(int id)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "Delete FROM Serie WHERE idSerie="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public bool Update(Poule p)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "UPDATE `Poule` SET `nomPoule` = '" + p.NomPoule + "', `taillePoule` = '" +p.Taille + "' WHERE `Poule`.`idSerie` =" +p.Id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
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