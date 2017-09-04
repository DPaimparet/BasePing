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
    }
}