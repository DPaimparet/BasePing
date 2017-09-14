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
            throw new NotImplementedException();
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
    }
}