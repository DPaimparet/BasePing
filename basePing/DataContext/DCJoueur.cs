using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCJoueur
    {
        public Joueur GetJoueur(int id)
        {
            Joueur player = new Joueur();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer le joueur gràce à l'id
                string query = "SELECT * FROM Joueur Where id=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    player.Id = reader.GetInt32(0);
                    player.Nom = reader.GetString(1);
                    player.Prenom = reader.GetString(2);
                    player.DateNaissance = reader.GetDateTime(3);
                    player.National = reader.GetString(4);
                }
                reader.Close();
                return player;
            }
            else
                return null;
        }

        // A faire
        //public Joueur GetJoueurByNation(int id)
        //{
        //    Joueur player = new Joueur();
        //    DBConnection con = DBConnection.Instance();
        //    if (con.IsConnect())
        //    {
        //        //récupérer le joueur gràce à l'id
        //        string query = "SELECT * FROM Joueur Where id=" + id;
        //        var cmd = new MySqlCommand(query, con.Connection);
        //        var reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            player.Id = reader.GetInt32(0);
        //            player.Nom = reader.GetString(1);
        //            player.Prenom = reader.GetString(2);
        //            player.DateNaissance = reader.GetDateTime(3);
        //            player.National = reader.GetString(4);
        //        }
        //        reader.Close();
        //        return player;
        //    }
        //    else
        //        return null;
        //}
    }
}