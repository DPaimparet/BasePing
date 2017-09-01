using basePing.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.DataContext
{
    public class DCCompetition
    {
        public Competition find(int id)
        {
            Competition comp=null;
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                //recuperer la compet en entier et l information par equipe ou pas
                string query = "SELECT * FROM Competition WHERE idComp="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   comp=new Competition(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(5),reader.GetString(6));
                }
                reader.Close();
                return comp;
            }
            else
                return null;
        }
        public List<Competition> findAll()
        {
            List<Competition> lComp = new List<Competition>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
               //public Competition(int id, string nom, DateTime dateDeb, DateTime dateFin, string typeComp, string nbrJoueur, Categorie cat)

                string query = "SELECT * FROM Competition INNER JOIN Categorie ON Competition.idCat=Categorie.idCat";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lComp.Add(new Competition(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(5),reader.GetString(6), new Categorie(reader.GetInt32(8), reader.GetString(9), reader.GetString(10))));
                }
                reader.Close();
                return lComp;
            }
            else
                return null;
        }

        public bool Insert(String nom,DateTime dateD, DateTime dateF, string pays, string type, string nbrJ, int idCat)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "INSERT INTO `competition` (`idComp`, `nom`, `dateDeb`, `dateFin`, `idPays`, `typeCompetition`, `nbrJoueur`, `idCat`) VALUES(NULL, '" + nom + "', '" + dateD.ToString("yyyy-MM-dd") + "', '" + dateF.ToString("yyyy-MM-dd") + "', '" + Convert.ToInt32(pays) + "', '" + type + "', '" + nbrJ + "', '" + idCat + "')";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public bool Update(int idcomp,string nom, DateTime dateD, DateTime dateF, string pays, string type, string nbrJ)
        {
           DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {          
                string query = "UPDATE `competition` SET `nom` = '" + nom + "', `dateDeb` = '" + dateD.ToString("yyyy-MM-dd") + "', `dateFin` = '" + dateF.ToString("yyyy-MM-dd") + "', `idPays` = '"+pays+"', `typeCompetition` = '"+type+"', `nbrJoueur` = '"+nbrJ+"' WHERE `competition`.`idComp` =" + idcomp;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public bool Delete(int id)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "DELETE FROM competition WHERE idComp="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public bool insertJoueurIntoComp(int idJoueur, int idCompetition)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "INSERT INTO ld_joueur_comp (idJoueur, idComp, position) VALUES ('" + idJoueur + "', '" + idCompetition + "', '0')";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }
        public bool DeleteJoueurInComp(int idJoueur, int idCompetition)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "DELETE FROM ld_joueur_comp WHERE idJoueur=" + idJoueur + " AND idComp=" + idCompetition;
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