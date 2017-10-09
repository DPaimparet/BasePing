using basePing.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.DataContext
{
    public class DCSousCategorie
    {


        public List<SousCategorie> findAll(int id)
        {
            List<SousCategorie> lCat = new List<SousCategorie>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM sous_categorie WHERE idCat="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lCat.Add(new SousCategorie(reader.GetInt32(0), reader.GetString(2)));
                }
                reader.Close();
                return lCat;
            }
            else
                return null;
        }

        internal bool Insert(string nom, int idC)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "INSERT INTO `sous_categorie` (`idSousCat`, `idCat`, `nom`) VALUES (NULL, '" + idC + "', '" + nom + "')";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public bool Update(int id,string nom)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "UPDATE `sous_categorie` SET `nom` = '" + nom + "' WHERE `sous_categorie`.`idSousCat` =" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
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
                string query = "DELETE FROM sous_categorie WHERE idSousCat=" + id;
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