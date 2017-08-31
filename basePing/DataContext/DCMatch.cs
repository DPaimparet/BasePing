using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCMatch
    {
        public List<Match> findAllIndiv(int idSerie)
        {
          
                List<Match> lMatch = new List<Match>();
                DBConnection con = DBConnection.Instance();
                if (con.IsConnect())
                {
                    
                    string query = "Select * FROM `serie` LEFT JOIN `rencontre` ON `rencontre`.`idSerie` = `serie`.`idSerie` LEFT JOIN `rencontre individuelle` ON `rencontre individuelle`.`idMatch` = `rencontre`.`idMatch` WHERE serie.idSerie="+idSerie;

                    var cmd = new MySqlCommand(query, con.Connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                            lMatch.Add(new Match(new Joueur(reader.GetInt32(11)),new Joueur(reader.GetInt32(12)),reader.GetInt32(4),reader.GetInt32(5),reader.GetInt32(7)));
                    }
                       reader.Close();
                       return lMatch;
                    }
                else
                    return null;
            
        }
    }
}