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
        public List<Joueur> GetAllJoueur()
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string query = "SELECT * FROM Joueur";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetString(4))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
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
        public List<Joueur> GetJoueurBySex(char sex)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "SELECT * FROM Joueur Where sex=" + sex;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetString(4))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
        public List<Joueur> GetJoueurByNation(string nation)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à la nation
                string query = "SELECT * FROM Joueur Where nationalite=" + nation;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetString(4))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
    }
}