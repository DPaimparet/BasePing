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
        // Sélectionne tous les joueurs
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
                        reader.GetChar(4),
                        reader.GetString(5))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
        // Sélectionne un joueur par son ID
        public Joueur GetJoueur(int id)
        {
            Joueur player = new Joueur();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where idJoueur="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    player.Id = reader.GetInt32(0);
                    player.Nom = reader.GetString(1);
                    player.Prenom = reader.GetString(2);
                    player.Sexe = reader.GetChar(4);
                    player.DateNaissance = reader.GetDateTime(3);
                    player.National = reader.GetString(9);
                }
                reader.Close();
                return player;
            }
            else
                return null;
        }
        // Sélectionne un joueur par son nom
        public List<Joueur> GetJoueur(string nom)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer le joueur gràce au nom du joueur
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where joueur.nom='"+nom+"'";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetChar(4),
                        reader.GetString(9))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
        // Sélectionne une liste de joueurs par leur nom et leur pays
        public List<Joueur> GetJoueurByNameAndCountry(string nom, int? pays)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where joueur.nom='" + nom + "' AND pays.id=" + pays;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetChar(4),
                        reader.GetString(9))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
        // Sélectionne une liste de joueurs par leur nom et leur sexe
        public List<Joueur> GetJoueurByNameAndSex(string nom, char sex)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where joueur.nom='" + nom + "' AND sexe='" + sex + "'";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetChar(4),
                        reader.GetString(9))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
        // Sélectionne une liste de joueurs par leur nom ,leur pays et leur sexe 
        public List<Joueur> GetJoueurByNameCountryAndSex(string nom,int? pays, char sex)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where joueur.nom='" + nom + "' AND pays.id=" + pays + " AND sexe='" + sex + "'";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetChar(4),
                        reader.GetString(9))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
        // Sélectionne une liste de joueurs par leur sexe 
        public List<Joueur> GetJoueurBySex(char sexe)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where sexe='" + sexe +"'";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetChar(4),
                        reader.GetString(9))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
        // Sélectionne une liste de joueurs par leur pays et leur sexe
        public List<Joueur> GetJoueurByNationAndSex(int? nation, char sexe)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à la nation
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where idPays=" + nation + " AND sexe ='" + sexe+ "'";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetChar(4),
                        reader.GetString(9))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
        // Sélectionne une liste de joueurs par leur pays
        public List<Joueur> GetJoueurByNation(int? nation)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à la nation
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where idPays=" + nation;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeJoueur.Add(new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetChar(4),
                        reader.GetString(9))
                    );
                }
                reader.Close();
                return listeJoueur;
            }
            else
                return null;
        }
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