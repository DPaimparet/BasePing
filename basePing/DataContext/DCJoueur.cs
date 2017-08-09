using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;
using basePing.ViewModel;

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

        public List<infoJoueur> GetinfoPouleJoueur(int id)
        {
            List<infoJoueur> lInfoJoueur = new List<infoJoueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer le joueur gràce à l'id
                string query = "SELECT ld_joueur_serie.*,joueur.* FROM Serie INNER JOIN ld_joueur_serie ON serie.idSerie=ld_joueur_serie.idSerie INNER JOIN joueur ON ld_joueur_serie.idJoueur=joueur.idJoueur Where serie.idSerie=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lInfoJoueur.Add(new infoJoueur {nom=reader.GetString(6)+" "+reader.GetString(7) ,position=reader.GetInt32(2) , matchGagné=reader.GetInt32(3),matchPerdu=reader.GetInt32(4),Id=reader.GetInt32(0)});
                }
                reader.Close();
                return lInfoJoueur;
            }
            else
                return null;
        }
    }
}