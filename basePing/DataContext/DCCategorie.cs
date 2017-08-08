using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCCategorie
    {
        
        public List<Categorie> findAll()
        {
            List<Categorie> lCat = new List<Categorie>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT* FROM Categorie";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lCat.Add(new Categorie(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
                reader.Close();
                return lCat;
            }
            else
                return null;
        }
    }
}