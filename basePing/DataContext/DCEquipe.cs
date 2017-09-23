using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using basePing.ViewModel;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCEquipe
    {
        public List<Equipe> findAllComp(int id)
        {
            List<Equipe> listeEquipe = new List<Equipe>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string query = "SELECT `equipe`.* FROM `equipe` LEFT JOIN `ld_equipe_comp` ON `ld_equipe_comp`.`idE` = `equipe`.`idEquipe` WHERE `ld_equipe_comp`.`idComp`=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listeEquipe.Add(new Equipe(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        new CPays(reader.GetInt32(2)),
                        reader.GetInt32(3)));
                }
                reader.Close();
                return listeEquipe;
            }
            else
                return null;
        }

        public bool Create(string nom, int taille, int idPays, int idC, int pos)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                int id = -1;
                string query = "INSERT INTO `equipe` (`nomEquipe`, `idPays`, `taille`) VALUES ('" + nom + "', '" + idPays + "', '" + taille + "')";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                string query2 = "Select MAX(idEquipe) FROM equipe";
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                    id = reader2.GetInt32(0);
                reader2.Close();

                string query3 = "INSERT INTO `ld_equipe_comp` (`idE`, `idComp`, `position`) VALUES ('" + id + "', '" + idC + "', '" + pos + "')";
                var cmd3 = new MySqlCommand(query3, con.Connection);
                var reader3 = cmd3.ExecuteReader();
                reader3.Close();


                return true;
            }
            else
                return false;


        }

        internal MatchEquipe FindRencontre(int id)
        {
            MatchEquipe m = null;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM `rencontre`  LEFT JOIN `rencontre equipe` ON `rencontre equipe`.`idMatch` = `rencontre`.`idMatch` WHERE `rencontre`.idMatch=" + id;

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    m=new MatchEquipe(reader.GetInt32(0), new Equipe(reader.GetInt32(8)), new Equipe(reader.GetInt32(9)), reader.GetInt32(1), reader.GetInt32(2));
                }
                reader.Close();
                return m;
            }
            return null;
        }

        internal List<MatchEquipe> findAllMatchTournoi(int id)
        {
            List<MatchEquipe> lMatch = new List<MatchEquipe>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM `serie` LEFT JOIN `rencontre` ON `rencontre`.`idSerie` = `serie`.`idSerie` LEFT JOIN `rencontre equipe` ON `rencontre equipe`.`idMatch` = `rencontre`.`idMatch` WHERE `rencontre equipe`.`idMatch` IS NOT NULL AND rencontre.idSerie=" + id;

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lMatch.Add(new MatchEquipe(reader.GetInt32(3), new Equipe(reader.GetInt32(11)), new Equipe(reader.GetInt32(12)), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(7)));
                }
                reader.Close();
                return lMatch;
            }
            return null;
        }

        public Equipe FindVainqueur(int id)
        {
            Equipe e = null;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer tous les joueurs
                string query = "SELECT `equipe`.* FROM `equipe` LEFT JOIN `ld_equipe_comp` ON `ld_equipe_comp`.`idE` = `equipe`.`idEquipe` WHERE position=1 AND `ld_equipe_comp`.`idComp`=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    e = new Equipe(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        new CPays(reader.GetInt32(2)),
                        reader.GetInt32(3));
                }
                reader.Close();
                return e;
            }
            else
                return null;
            
        }

        public List<MatchEquipe> FindMatchNotLinkedEquipe(int idC, int idJ)
        {
            List<MatchEquipe> lMatch = new List<MatchEquipe>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "Select * FROM `rencontre`  LEFT JOIN `rencontre equipe` ON `rencontre equipe`.`idMatch` = `rencontre`.`idMatch` WHERE idSerie is NULL AND position is NULL AND idCompet=" + idC + " AND (idEquipe1=" + idJ + " OR idEquipe2=" + idJ + ")";

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



        public bool Create(int? equipe1, int score1, int? equipe2, int score2, int pos, int ids, int idC)
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


                string query3 = "INSERT INTO `rencontre equipe` (`idMatch`, `idEquipe1`, `idEquipe2`) VALUES ('" + id + "', '" + equipe1 + "', '" + equipe2 + "')";
                var cmd3 = new MySqlCommand(query3, con.Connection);
                var reader3 = cmd3.ExecuteReader();

                reader3.Close();
                return true;
            }
            else
                return false;
        }

        internal List<MatchEquipe> FindRencontreMatchPouleEquipe(int idP, int idE)
        {
            List<MatchEquipe> lMatch = new List<MatchEquipe>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM  `rencontre`  LEFT JOIN `rencontre equipe` ON `rencontre equipe`.`idMatch` = `rencontre`.`idMatch` WHERE rencontre.idSerie=" + idP +" AND (idEquipe1="+idE+" OR idEquipe2="+idE+")";

                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lMatch.Add(new MatchEquipe(reader.GetInt32(0), new Equipe(reader.GetInt32(8)), new Equipe(reader.GetInt32(9)), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(4)));
                }
                reader.Close();
                return lMatch;
            }
            return null;
        }

        public List<Equipe> FindEquipeComp(int id)
        {
            List<Equipe> l = new List<Equipe>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "SELECT idE FROM ld_equipe_comp WHERE idComp=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    l.Add(new Equipe(reader.GetInt32(0)));
                }
                reader.Close();
                foreach (Equipe e in l)
                    e.RecupererEquipe();


                return l;
            }
            else
                return null;
        }


        //public bool Create(int? equipe1, int score1, int? equipe2, int score2, int pos, int ids, int idC)
        //{
        //    DBConnection con = DBConnection.Instance();
        //    if (con.IsConnect())
        //    {

        //        string query1 = "INSERT INTO `rencontre` (`idMatch`, `score1`, `score2`, `nbrSet`, `position`, `idCompet`, `idSerie`) VALUES (NULL, '" + score1 + "', '" + score2 + "', '" + (score1 + score2) + "', '" + pos + "', '" + idC + "','" + ids + "')";
        //        var cmd1 = new MySqlCommand(query1, con.Connection);
        //        var reader1 = cmd1.ExecuteReader();

        //        reader1.Close();


        //        int id = 0;
        //        string query2 = "SELECT MAX(idMatch) FROM rencontre";
        //        var cmd2 = new MySqlCommand(query2, con.Connection);
        //        var reader2 = cmd2.ExecuteReader();

        //        while (reader2.Read())
        //        {
        //            id = reader2.GetInt32(0);
        //        }

        //        reader2.Close();


        //        string query3 = "INSERT INTO `rencontre equipe` (`idMatch`, `idEquipe1`, `idEquipe2`) VALUES ('" + id + "', '" + equipe1 + "', '" + equipe2 + "')";
        //        var cmd3 = new MySqlCommand(query3, con.Connection);
        //        var reader3 = cmd3.ExecuteReader();

        //        reader3.Close();
        //        return true;
        //    }
        //    else
        //        return false;
        //}


        public bool DeleteLd(int idJ, int idE)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "DELETE FROM Eclat_Joueur_Equipe WHERE idEquipe=" + idE+" AND idJoueur="+idJ;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public Equipe find(int id)
        {
            Equipe e = null;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Equipe WHERE idEquipe="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    e = new Equipe(reader.GetInt32(0), reader.GetString(1), new CPays(reader.GetInt32(2)), reader.GetInt32(3));
                }
                reader.Close();
                e.National.Pays = new DCPays().GetPays(e.National.Id);
                string query2 = "SELECT joueur.* FROM eclat_Joueur_Equipe LEFT JOIN joueur ON eclat_Joueur_Equipe.idJoueur=joueur.idJoueur WHERE idEquipe="+ id;
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {

                    e.ListJ.Add(new Joueur(
                        reader2.GetInt32(0),
                        reader2.GetString(1),
                        reader2.GetString(2),
                        reader2.GetDateTime(3),
                        reader2.GetChar(4),
                        e.National.Pays));
                }
                reader2.Close();
                return e;
            }
            else
                return null;
        }

        public bool Delete(int idE, int idC)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "DELETE FROM Equipe WHERE idEquipe="+idE;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        internal List<infoEquipe> GetinfoPouleEquipe(int id)
        {
            List<infoEquipe> lInfoEquipe = new List<infoEquipe>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer le joueur gràce à l'id
                string query = "SELECT ld_equipe_serie.*,equipe.* FROM Serie INNER JOIN ld_equipe_serie ON serie.idSerie=ld_equipe_serie.idSerie INNER JOIN equipe ON ld_equipe_serie.idE=equipe.idEquipe Where serie.idSerie=" + id + " ORDER BY pointGagné-pointPerdu DESC";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lInfoEquipe.Add(new infoEquipe { nom = reader.GetString(6) , position = reader.GetInt32(4), matchGagné = reader.GetInt32(2), matchPerdu = reader.GetInt32(3), Id = reader.GetInt32(0) });
                }
                reader.Close();
                return lInfoEquipe;
            }
            else
                return null;
        }

        internal List<Joueur> GetMembre(int id)
        {
            throw new NotImplementedException();
        }

        public bool CreateLD(int idJ, int idE)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query2 = "Select * FROM eclat_joueur_equipe WHERE idJoueur="+idJ+" AND idEquipe="+idE;
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();
                if (!reader2.HasRows)
                {
                    reader2.Close();
                    string query = "INSERT INTO `eclat_joueur_equipe` (`idEquipe`, `idJoueur`) VALUES ('" + idE + "', '" + idJ + "')";
                    var cmd = new MySqlCommand(query, con.Connection);
                    var reader = cmd.ExecuteReader();
                    reader.Close();

                    return true;
                }
                else
                {
                    reader2.Close();
                    return false;

                }
            }
            else
                return false;

        }

        public bool DeleteMatch(int id)
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

        public bool VerifJoueurEquipe(int idJ, int idC)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query2 = "SELECT `ld_equipe_comp`.*, `equipe`.*, `eclat_joueur_equipe`.* FROM `equipe` LEFT JOIN `ld_equipe_comp` ON `ld_equipe_comp`.`idE` = `equipe`.`idEquipe` LEFT JOIN `eclat_joueur_equipe` ON `eclat_joueur_equipe`.`idEquipe` = `equipe`.`idEquipe` WHERE ld_equipe_comp.idComp="+idC+" AND eclat_joueur_equipe.idJoueur="+idJ+" AND eclat_joueur_equipe.idJoueur IS NOT null";
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();
                if (!reader2.HasRows)
                {
                    reader2.Close();
                    return true;
                }
                else
                {
                    reader2.Close();
                    return false;

                }
            }
            else
                return false;
        }


        public bool LinkMatch(int pos, int ids, int idcomp,int idMatch)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "UPDATE `rencontre` SET `position` = '" + pos + "', idSerie=" + ids + " WHERE `rencontre`.`idCompet` =" + idcomp+" AND idMatch = "+idMatch;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }



        public bool InsertLD(infoEquipe j, int idP,int idC)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query2 = "SELECT `poule`.*, `serie`.*, `ld_equipe_serie`.* FROM `serie` LEFT JOIN `poule` ON `poule`.`idSerie` = `serie`.`idSerie` LEFT JOIN `ld_equipe_serie` ON `ld_equipe_serie`.`idSerie` = `serie`.`idSerie` WHERE idComp="+idC+" AND idE="+j.Id;
                var cmd2 = new MySqlCommand(query2, con.Connection);
                var reader2 = cmd2.ExecuteReader();
                if (!reader2.HasRows) {
                    reader2.Close();
                    string query = "INSERT INTO `ld_equipe_serie` (`idE`, `idSerie`, `position`, `pointGagné`, `pointPerdu`) VALUES ('" + j.Id + "', '" + idP + "', '" + j.position + "', '" + j.matchGagné + "', '" + j.matchPerdu + "')";
                    var cmd = new MySqlCommand(query, con.Connection);
                    var reader = cmd.ExecuteReader();

                    reader.Close();
                    return true;
                }
                else
                {
                    reader2.Close();

                    return false;
                }
                
            }
            else
                return false;
        }

        public bool UpdateLD(infoEquipe j, int idPoule)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "UPDATE `ld_equipe_serie` SET `position` = '" + j.position + "', `pointGagné` = '" + j.matchGagné + "', `pointPerdu` = '" + j.matchPerdu + "' WHERE `ld_equipe_serie`.`idE` = " + j.Id + " AND `ld_equipe_serie`.`idSerie` = " + idPoule;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }


        public bool DeleteLD(int idE, int idP)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //récupérer les joueurs grâce à leur sex
                string query = "DELETE FROM ld_equipe_serie WHERE idSerie=" + idP + " AND idE=" + idE;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

    
    }
}