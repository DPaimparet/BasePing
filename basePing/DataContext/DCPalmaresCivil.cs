using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCPalmaresCivil
    {
        public bool AjoutPalmares(int idJoueur, string recompense, DateTime date)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "INSERT INTO palmarescivicjoueur (idJoueur,recompense,date) VALUES (" + idJoueur + ", '" + recompense + "', '" + date.ToString("yyyy-MM-dd") + "')";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                return true;
            }
            else
                return false;
        }
        public bool UpdatePalmares(int id, string recompense, DateTime date)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "UPDATE palmarescivicjoueur SET recompense = '" + recompense + "', date = '"+ date.ToString("yyyy-MM-dd") + "' WHERE id = " + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                return true;
            }
            else
                return false;
        }

        public bool DeletePalmares(int id)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "Delete FROM palmarescivicjoueur WHERE id = " + id + "";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                return true;
            }
            else
                return false;
        }

        public PalmaresCivil GetPalmares(int id)
        {
            PalmaresCivil palmares = new PalmaresCivil();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM palmarescivicjoueur Where idJoueur=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    palmares.IdPalmares = reader.GetInt32(0);
                    palmares.Recompense = reader.GetString(2);
                    palmares.Annee = reader.GetDateTime(3);
                    
                }
                reader.Close();
                return palmares;
            }
            else
                return null;
        }

        public List<PalmaresCivil> GetAllRecompense(int idJoueur)
        {
            List<PalmaresCivil> listeRecompense = new List<PalmaresCivil>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer le joueur gràce au nom du joueur
                string query = "SELECT * FROM palmarescivicjoueur Where idJoueur ='" + idJoueur + "' ORDER BY date DESC";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeRecompense.Add(new PalmaresCivil(
                        reader.GetInt32(0),
                        reader.GetString(2),
                        reader.GetDateTime(3))
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