using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCFederation
    {
        public Federation GetFederation(int id)
        {
            Federation federation = new Federation();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string query = "SELECT * FROM federation WHERE id="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    federation.Id = reader.GetInt32(0);
                    federation.PaysFederation = reader.GetString(1);
                    federation.PaysFederation = reader.GetString(2);
                    federation.Web = reader.GetString(3);
                }
                reader.Close();
                return federation;
            }
            else
                return null;
        }
        // Sélectionne tous les joueurs
        public List<Federation> GetAllFederation()
        {
            List<Federation> listeFederation = new List<Federation>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string query = "SELECT * FROM federation";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeFederation.Add(new Federation(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3))
                    );
                }
                reader.Close();
                return listeFederation;
            }
            else
                return null;
        }
        public string GetPays(string pays)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string query = "SELECT nom FROM pays Where id="+ pays;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pays = reader.GetString(0);
                }
                reader.Close();
                return pays;
            }
            else
                return null;
        }
        public string GetPays(int? pays)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string Pays="";
                string query = "SELECT nom FROM pays Where id=" + pays;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pays = reader.GetString(0);
                }
                reader.Close();
                return Pays;
            }
            else
                return null;
        }
        public bool AddFederation(string nomFedePays, string pays, string web)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "INSERT INTO federation (nomFede, pays, web) VALUES ('" + nomFedePays + "' , '" + pays + "' , '" + web + "')";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                return true;
            }
            else
                return false;
        }
        public bool UpdateFederation(int id, string nomFedePays, string pays, string web)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "UPDATE federation SET pays='" + pays + "', nomFede = '" + nomFedePays + "', web='" + web + "' Where id=" + id + "";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }
        public bool DelFederation(int id)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "DELETE FROM federation WHERE id=" + id;
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