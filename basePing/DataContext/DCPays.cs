using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCPays
    {
        public List<CPays> GetAllPays()
        {
            List<CPays> listePays = new List<CPays>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string query = "SELECT * FROM Pays";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listePays.Add(new CPays(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2))
                    );
                }
                reader.Close();
                return listePays;
            }
            else
                return null;
        }
    }
}