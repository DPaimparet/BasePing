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
        public List<Match> findAll(int idSerie)
        {
          
                List<Match> lMatch = new List<Match>();
                DBConnection con = DBConnection.Instance();
                if (con.IsConnect())
                {
                    Joueur j1= new Joueur(1,"a","a", new DateTime(),"France");
                    Joueur j2 = new Joueur(2, "b", "b", new DateTime(), "Belge");
                    // string query = "SELECT Joueur,* FROM Rencontre INNER JOIN Joueur on Rencontre.idJ1=Joueur.id WHERE idSerie=" + idSerie;
                    string query = "SELECT * FROM Rencontre";

                    var cmd = new MySqlCommand(query, con.Connection);
                    var reader = cmd.ExecuteReader();
                    lMatch.Add(new Match(j1,j2,2,1));
                    lMatch.Add(new Match(j1, j2, 4, 5));

                    while (reader.Read())
                        {
                        
                        }
                        reader.Close();
                        return lMatch;
                    }
                else
                    return null;
            
        }
    }
}