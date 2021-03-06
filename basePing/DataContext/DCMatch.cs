﻿using System;
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
                    
                    string query = "Select * FROM `serie` LEFT JOIN `rencontre` ON `rencontre`.`idSerie` = `serie`.`idSerie` LEFT JOIN `rencontre individuelle` ON `rencontre individuelle`.`idMatch` = `rencontre`.`idMatch` WHERE rencontre.idSerie="+idSerie;

                    var cmd = new MySqlCommand(query, con.Connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                            lMatch.Add(new Match(reader.GetInt32(3),new Joueur(reader.GetInt32(11)),new Joueur(reader.GetInt32(12)),reader.GetInt32(4),reader.GetInt32(5),reader.GetInt32(7)));
                    }
                       reader.Close();
                       return lMatch;
                    }
                    return null;
            
        }

        internal MatchDouble findDouble(int id)
        {
            MatchDouble match = null;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM rencontre LEFT JOIN `rencontre double` ON `rencontre double`.`idMatch` = `rencontre`.`idMatch` WHERE rencontre.idMatch=" + id;

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    match = new MatchDouble(reader.GetInt32(0), new Joueur(reader.GetInt32(8)), new Joueur(reader.GetInt32(9)),new Joueur(reader.GetInt32(10)),new Joueur(reader.GetInt32(11)), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(4));
                }
                reader.Close();
                return match;
            }
            return null;
        }

        public Match find(int id)
        {
            Match match = null;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM rencontre LEFT JOIN `rencontre individuelle` ON `rencontre individuelle`.`idMatch` = `rencontre`.`idMatch` WHERE rencontre.idMatch=" + id;

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    match=new Match(reader.GetInt32(0), new Joueur(reader.GetInt32(8)), new Joueur(reader.GetInt32(9)), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(4));
                }
                reader.Close();
                return match;
            }
            return null;
        }

        public bool Delete(int id)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "DELETE FROM rencontre WHERE idMatch=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public List<MatchEquipe> FindMatchNotLinkedEquipe(int idC)
        {
            List<MatchEquipe> lMatch = new List<MatchEquipe>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM `rencontre`  LEFT JOIN `rencontre equipe` ON `rencontre equipe`.`idMatch` = `rencontre`.`idMatch` WHERE idSerie is NULL AND position is NULL AND idCompet=" + idC;

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lMatch.Add(new MatchEquipe(reader.GetInt32(0), new Equipe(reader.GetInt32(8)), new Equipe(reader.GetInt32(9)), reader.GetInt32(1), reader.GetInt32(2)));
                }
                reader.Close();
                return lMatch;
            }
            return null;
        }

        public bool Create(int id, int? joueur1, int score1, int? joueur2, int score2)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query1 = "INSERT INTO `rencontre` (`idMatch`, `score1`, `score2`, `nbrSet`, `position`, `idCompet`, `idSerie`) VALUES (NULL, '"+score1+"', '"+score2+"', '"+(score1+score2)+"', NULL, '"+id+"',NULL)";
                var cmd1 = new MySqlCommand(query1, con.Connection);
                var reader1 = cmd1.ExecuteReader();

                reader1.Close();



                string query2 = "SELECT MAX(idMatch) FROM rencontre";
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    id = reader2.GetInt32(0);
                }

                reader2.Close();


                string query3 = "INSERT INTO `rencontre individuelle` (`idMatch`, `idJ1`, `idJ2`) VALUES ('"+id+"', '"+joueur1+"', '"+joueur2+"')";
                var cmd3 = new MySqlCommand(query3, con.Connection);
                var reader3 = cmd3.ExecuteReader();

                reader3.Close();
                return true;
            }
            else
                return false;
        }

        internal List<MatchDouble> findMatchDoubleEquipe(int idSerie, int idJoueur)
        {
            List<MatchDouble> lMatch = new List<MatchDouble>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM `serie` LEFT JOIN `rencontre` ON `rencontre`.`idSerie` = `serie`.`idSerie` LEFT JOIN `rencontre double` ON `rencontre double`.`idMatch` = `rencontre`.`idMatch` WHERE rencontre.idSerie=" + idSerie + " AND `rencontre double`.`idJoueur`=" + idJoueur + " OR `rencontre double`.`idJoueur2`=" + idJoueur;

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lMatch.Add(new MatchDouble(reader.GetInt32(3), new Joueur(reader.GetInt32(11)), new Joueur(reader.GetInt32(12)), new Joueur(reader.GetInt32(13)), new Joueur(reader.GetInt32(14)), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(7)));
                }
                reader.Close();

                string query2 = "Select * FROM `serie` LEFT JOIN `rencontre` ON `rencontre`.`idSerie` = `serie`.`idSerie` LEFT JOIN `rencontre double` ON `rencontre double`.`idMatch` = `rencontre`.`idMatch` WHERE rencontre.idSerie=" + idSerie + " AND  `rencontre double`.`idJoueur3`=" + idJoueur + " OR `rencontre double`.`idJoueur4`=" + idJoueur;

                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    lMatch.Add(new MatchDouble(reader.GetInt32(3), new Joueur(reader.GetInt32(11)), new Joueur(reader.GetInt32(12)), new Joueur(reader.GetInt32(13)), new Joueur(reader.GetInt32(14)), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(7)));
                }
                reader2.Close();

                return lMatch;
            }
            return null;
        }

        public bool CreateEquipe(int id, int? equipe1, int score1, int? equipe2, int score2)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query1 = "INSERT INTO `rencontre` (`idMatch`, `score1`, `score2`, `nbrSet`, `position`, `idCompet`, `idSerie`) VALUES (NULL, '" + score1 + "', '" + score2 + "', '" + (score1 + score2) + "', NULL, '" + id + "',NULL)";
                var cmd1 = new MySqlCommand(query1, con.Connection);
                var reader1 = cmd1.ExecuteReader();

                reader1.Close();

                string query2 = "SELECT MAX(idMatch) FROM rencontre";
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    id = reader2.GetInt32(0);
                }

                reader2.Close();

                string query3 = "INSERT INTO `rencontre equipe` (`idMatch`, `idEquipe1`, `idEquipe2`) VALUES ('" + id + "', '" + equipe1 + "', '" + equipe2 + "')";
                var cmd3 = new MySqlCommand(query3, con.Connection);
                var reader3 = cmd3.ExecuteReader();

                reader3.Close();
                return true;
            }
            else
                return false;
        }

        public List<Match> FindMatchNotLinked(int idC, int idJ)
        {
            List<Match> lMatch = new List<Match>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "Select * FROM `rencontre`  LEFT JOIN `rencontre individuelle` ON `rencontre individuelle`.`idMatch` = `rencontre`.`idMatch` WHERE idSerie is NULL AND position is NULL AND idCompet=" + idC+" AND (idJ1="+idJ+" OR idJ2="+idJ+")";

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lMatch.Add(new Match(reader.GetInt32(0), new Joueur(reader.GetInt32(8)), new Joueur(reader.GetInt32(9)), reader.GetInt32(1), reader.GetInt32(2)));
                }
                reader.Close();
                return lMatch;
            }
            return null;

        }

        public bool CreateDouble(int? joueur1, int? joueur2, int score1, int? joueur3, int? joueur4, int score2, int pos, int ids, int idC)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query1 = "INSERT INTO `rencontre` (`idMatch`, `score1`, `score2`, `nbrSet`, `position`, `idCompet`, `idSerie`) VALUES (NULL, '" + score1 + "', '" + score2 + "', '" + (score1 + score2) + "', '" + pos + "', '" + idC + "','" + ids + "')";
                var cmd1 = new MySqlCommand(query1, con.Connection);
                var reader1 = cmd1.ExecuteReader();

                reader1.Close();


                int id = 0;
                string query2 = "SELECT MAX(idMatch) FROM rencontre";
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    id = reader2.GetInt32(0);
                }

                reader2.Close();


                string query3 = "INSERT INTO `rencontre double` (`idMatch`, `idJoueur`, `idJoueur2`, `idJoueur3`, `idJoueur4`) VALUES ('" + id + "', '" + joueur1 + "', '" + joueur2 + "', '" + joueur3 + "', '" + joueur4 + "')";
                var cmd3 = new MySqlCommand(query3, con.Connection);
                var reader3 = cmd3.ExecuteReader();

                reader3.Close();
                return true;
            }
            else
                return false;
        }

        public List<Match> findMatchJoueur(int idSerie, int idJoueur)
        {
            List<Match> lMatch = new List<Match>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM `serie` LEFT JOIN `rencontre` ON `rencontre`.`idSerie` = `serie`.`idSerie` LEFT JOIN `rencontre individuelle` ON `rencontre individuelle`.`idMatch` = `rencontre`.`idMatch` WHERE rencontre.idSerie=" + idSerie+ " AND (`rencontre individuelle`.`idJ1`="+idJoueur+" OR `rencontre individuelle`.`idJ2`="+idJoueur+")";

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lMatch.Add(new Match(reader.GetInt32(3), new Joueur(reader.GetInt32(11)), new Joueur(reader.GetInt32(12)), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(7)));
                }
                reader.Close();
                return lMatch;
            }
            return null;
        }



        public List<Match> findMatchJoueurEquipe(int idSerie, int idJoueur)
        {
            List<Match> lMatch = new List<Match>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM `serie` LEFT JOIN `rencontre` ON `rencontre`.`idSerie` = `serie`.`idSerie` LEFT JOIN `rencontre individuelle` ON `rencontre individuelle`.`idMatch` = `rencontre`.`idMatch` WHERE rencontre.idSerie=" + idSerie + " AND `rencontre individuelle`.`idJ1`=" + idJoueur;

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lMatch.Add(new Match(reader.GetInt32(3), new Joueur(reader.GetInt32(11)), new Joueur(reader.GetInt32(12)), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(7)));
                }
                reader.Close();
                string query2 = "Select * FROM `serie` LEFT JOIN `rencontre` ON `rencontre`.`idSerie` = `serie`.`idSerie` LEFT JOIN `rencontre individuelle` ON `rencontre individuelle`.`idMatch` = `rencontre`.`idMatch` WHERE rencontre.idSerie=" + idSerie + " AND `rencontre individuelle`.`idJ2`=" + idJoueur;

                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    lMatch.Add(new Match(reader.GetInt32(3), new Joueur(reader.GetInt32(12)), new Joueur(reader.GetInt32(11)), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(7)));
                }
                reader2.Close();
                return lMatch;
            }
            return null;
        }

        public bool Create(int? joueur1, int score1, int? joueur2, int score2, int pos, int ids, int idC)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query1 = "INSERT INTO `rencontre` (`idMatch`, `score1`, `score2`, `nbrSet`, `position`, `idCompet`, `idSerie`) VALUES (NULL, '" + score1 + "', '" + score2 + "', '" + (score1 + score2) + "', '"+pos+"', '" + idC + "','"+ids+"')";
                var cmd1 = new MySqlCommand(query1, con.Connection);
                var reader1 = cmd1.ExecuteReader();

                reader1.Close();


                int id =0;
                string query2 = "SELECT MAX(idMatch) FROM rencontre";
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    id = reader2.GetInt32(0);
                }

                reader2.Close();


                string query3 = "INSERT INTO `rencontre individuelle` (`idMatch`, `idJ1`, `idJ2`) VALUES ('" + id + "', '" + joueur1 + "', '" + joueur2 + "')";
                var cmd3 = new MySqlCommand(query3, con.Connection);
                var reader3 = cmd3.ExecuteReader();

                reader3.Close();
                return true;
            }
            else
                return false;
        }

        public bool LinkMatch(int pos, int ids,int idcomp,int idMatch)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "UPDATE `rencontre` SET `position` = '" + pos + "', idSerie="+ids+" WHERE `rencontre`.`idCompet` =" + idcomp+" AND idMatch="+idMatch;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public List<Match> FindMatchNotLinked(int idC)
        {
            List<Match> lMatch = new List<Match>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM `rencontre`  LEFT JOIN `rencontre individuelle` ON `rencontre individuelle`.`idMatch` = `rencontre`.`idMatch` WHERE idSerie is NULL AND position is NULL AND idCompet="+idC;

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lMatch.Add(new Match(reader.GetInt32(0),new Joueur(reader.GetInt32(8)), new Joueur(reader.GetInt32(9)), reader.GetInt32(1),reader.GetInt32(2)));
                }
                reader.Close();
                return lMatch;
            }
            return null;
        }
    }
}