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
    }
}