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
        public Competition find(int id)
        {
            Competition comp=null;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //recuperer la compet en entier et l information par equipe ou pas
                string query = "SELECT * FROM Competition WHERE idComp="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   comp=new Competition(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(5));
                }
                reader.Close();
                return comp;
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
                string query = "SELECT * FROM Competition INNER JOIN Categorie ON Competition.idCat=Categorie.idCat";
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