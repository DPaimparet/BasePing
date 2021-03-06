﻿using System;
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
        // Ajout d'un joueur dans la DB
        public bool AjoutJoueur(string nom, string prenom, DateTime dateNaiss, char sexe, string pays)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "INSERT INTO joueur (nom,prenom,dateNaiss,sexe,idPays) VALUES ('" + nom + "', '" + prenom + "', '" + dateNaiss.ToString("yyyy-MM-dd") + "', '" + sexe + "', '" + pays + "')";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                return true;
            }
            else
                return false;
        }
        // Supprimer un joueur
        public bool DeleteJoueur(int id)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "DELETE FROM joueur WHERE idJoueur="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                return true;
            }
            else
                return false;
        }

       
        public bool UpdateJoueur(int idJoueur,string nom, string prenom, DateTime dateNaiss, char sexe, int pays)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "UPDATE joueur SET nom ='" + nom + "',prenom = '" + prenom + "', dateNaiss = '" + dateNaiss.ToString("yyyy-MM-dd") + "',sexe =  '" + sexe + "' ,idPays = '" + pays + "' WHERE idJoueur=" + idJoueur ;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                return true;
            }
            else
                return false;
        }
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

        public List<Joueur> findAllComp(int id)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string query = "SELECT `joueur`.* FROM `joueur` LEFT JOIN `ld_joueur_comp` ON `ld_joueur_comp`.`idJoueur` = `joueur`.`idJoueur` WHERE `ld_joueur_comp`.`idComp`="+id;
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

        public Joueur FindVainqueur(int id)
        {
            Joueur j = null;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string query = "SELECT `joueur`.* FROM `joueur` LEFT JOIN `ld_joueur_comp` ON `ld_joueur_comp`.`idJoueur` = `joueur`.`idJoueur` WHERE position=1 AND `ld_joueur_comp`.`idComp`=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    j = new Joueur(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetChar(4),
                        reader.GetString(5)
                    );
                }
                reader.Close();
                return j;
            }
            else
                return null;
        }

        public List<Joueur> GetJoueurComp(int id)
        {
            throw new NotImplementedException();
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
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where joueur.nom LIKE '"+nom+"%'";
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
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where joueur.nom LIKE '" + nom + "%' AND pays.id=" + pays + " ORDER BY idPays , joueur.nom ";
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
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where joueur.nom LIKE '" + nom + "%' AND sexe='" + sex + "' ";
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
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where joueur.nom LIKE '" + nom + "%' AND pays.id=" + pays + " AND sexe='" + sex + "'";
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

        public bool InsertLD(infoJoueur j, int idPoule)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "INSERT INTO `ld_joueur_serie` (`idJoueur`, `idSerie`, `position`, `matchGagne`, `matchPerdu`) VALUES ('"+j.Id+"', '"+idPoule+"', '"+j.position+"', '"+j.matchGagné+"', '"+j.matchPerdu+"')";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public bool DeleteLD(int idJ, int idP)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "DELETE FROM ld_joueur_serie WHERE idSerie="+idP+" AND idJoueur="+idJ;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public bool UpdateLD(infoJoueur j, int idPoule)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "UPDATE `ld_joueur_serie` SET `position` = '" + j.position + "', `matchGagne` = '"+j.matchGagné+"', `matchPerdu` = '"+j.matchPerdu+"' WHERE `ld_joueur_serie`.`idJoueur` = " + j.Id + " AND `ld_joueur_serie`.`idSerie` = " + idPoule;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public List<Joueur> GetJoueurBySex(char sexe)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where sexe='" + sexe + "' ORDER BY idPays , joueur.nom ";
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

        public bool DeletePart(int idJ, int idC)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "DELETE FROM ld_joueur_comp WHERE idJoueur=" + idJ + " AND idComp=" + idC;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                
                return true;
            }
            else
                return false;
        }

        // Sélectionne une liste de joueurs par leur pays et leur sexe
        public List<Joueur> GetJoueurByNationAndSex(int? nation, char sexe)
        {
            List<Joueur> listeJoueur = new List<Joueur>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à la nation
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where idPays=" + nation + " AND sexe ='" + sexe+ "' ORDER BY idPays , joueur.nom";
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
                string query = "SELECT * FROM Joueur Inner Join Pays On joueur.idPays = pays.id Where idPays=" + nation + " ORDER BY idPays , joueur.nom ";
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
                string query = "SELECT ld_joueur_serie.*,joueur.* FROM Serie INNER JOIN ld_joueur_serie ON serie.idSerie=ld_joueur_serie.idSerie INNER JOIN joueur ON ld_joueur_serie.idJoueur=joueur.idJoueur Where serie.idSerie=" + id+" ORDER BY matchGagne-matchPerdu DESC";
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
        public int GetPositionComp(int id, int idComp)
        {
            int position = 0;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT position FROM ld_joueur_comp WHERE idJoueur = " + id + " AND idComp = " + idComp;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    position = reader.GetInt32(0);
                }
                reader.Close();
                return position;
            }
            else
                return position;
        }
    }
}