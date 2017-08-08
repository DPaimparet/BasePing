using basePing.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.DataContext
{
    public class DCCompetition
    {
        public List<Competition> find(int id)
        {
            List<Competition> lComp = new List<Competition>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //recuperer la compet en entier et l information par equipe ou pas
                string query = "SELECT * FROM Compétition INNER JOIN Match ON Compétition.idComp=Match.idCompet INNER JOIN ldjoueur ON Match.idMatch=ldjoueur.idMatch WHERE idComp="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lComp.Add(new Competition(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(5), new Categorie(reader.GetInt32(6), reader.GetString(8), reader.GetString(9))));
                }
                reader.Close();
                return lComp;
            }
            else
                return null;
        }
        public List<Competition> findAll()
        {
            List<Competition> lComp = new List<Competition>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Compétition INNER JOIN Categorie ON Compétition.idCat=Categorie.idCat";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lComp.Add(new Competition(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(5), new Categorie(reader.GetInt32(6), reader.GetString(8), reader.GetString(9))));
                }
                reader.Close();
                return lComp;
            }
            else
                return null;
        }
    }
}