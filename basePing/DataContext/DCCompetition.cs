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
                string query = "SELECT * FROM Competition LEFT JOIN Categorie ON competition.idCat=Categorie.idCat LEFT JOIN Pays on competition.idPays=Pays.id WHERE idComp="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   comp=new Competition(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(5),reader.GetString(6), new Categorie(reader.GetInt32(9), reader.GetString(10), reader.GetString(11)), null, new CPays(reader.GetInt32(12),reader.GetString(13),reader.GetString(14)));
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

                string query = "SELECT `competition`.*,`sous_categorie`.*, `categorie`.* ,Pays.* FROM `categorie` LEFT JOIN `sous_categorie` ON `sous_categorie`.`idCat` = `categorie`.`idCat` LEFT JOIN `competition` ON `competition`.`idSousCat` = `sous_categorie`.`idSousCat` LEFT JOIN Pays on competition.idPays=Pays.id  WHERE idComp is not NULL AND `competition`.`idSousCat` is not NULL";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lComp.Add(new Competition(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(5),reader.GetString(6), new Categorie(reader.GetInt32(12), reader.GetString(13), reader.GetString(14)),new SousCategorie(reader.GetInt32(9),reader.GetString(11)),new CPays(reader.GetInt32(15), reader.GetString(16), reader.GetString(17))));
                }
                reader.Close();
                return lComp;
            }
            else
                return null;
        }


        public List<Competition> findAllCompNoCat(int id)
        {
            List<Competition> lComp = new List<Competition>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
               

                string query = "SELECT `competition`.*, `categorie`.*,Pays.*  FROM `categorie` LEFT JOIN `competition` ON `competition`.`idCat` = `categorie`.`idCat` LEFT JOIN Pays on competition.idPays=Pays.id WHERE  `competition`.`idSousCat` is NULL AND `competition`.`idCat`=" + id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lComp.Add(new Competition(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(5), reader.GetString(6), new Categorie(reader.GetInt32(9), reader.GetString(10), reader.GetString(11)),null, new CPays(reader.GetInt32(12), reader.GetString(13), reader.GetString(14))));
                }
                reader.Close();
                return lComp;
            }
            else
                return null;
        }

        internal List<Competition> FindListTrie(string nom, int? an, string sexe, string nbrJ, int idSousCat)
        {
            List<Competition> lComp = new List<Competition>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
               
                string query = "SELECT `competition`.*,`sous_categorie`.*, `categorie`.* ,Pays.* FROM `categorie` LEFT JOIN `sous_categorie` ON `sous_categorie`.`idCat` = `categorie`.`idCat` LEFT JOIN `competition` ON `competition`.`idSousCat` = `sous_categorie`.`idSousCat` LEFT JOIN Pays on competition.idPays=Pays.id  WHERE idComp is not NULL AND `competition`.`idSousCat` is not NULL";
                if (nom !="")
                    query += " AND competition.nom LIKE'%" + nom + "%'";
                if (an != null)
                    query += " AND (year(competition.dateDeb) ="+an+ " OR year(competition.dateFin) = "+an+")";
                if (sexe != "") { 
                    if(sexe=="Féminin")
                        query += " AND competition.typeCompetition LIKE'F%'";

                    else
                        query += " AND competition.typeCompetition='"+sexe+"'";
                }
                if (nbrJ !="")
                    query += " AND competition.nbrJoueur='" + nbrJ + "'";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lComp.Add(new Competition(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(5), reader.GetString(6), new Categorie(reader.GetInt32(12), reader.GetString(13), reader.GetString(14)), new SousCategorie(reader.GetInt32(9), reader.GetString(11)), new CPays(reader.GetInt32(15), reader.GetString(16), reader.GetString(17))));
                }
                reader.Close();
                return lComp;
            }
            else
                return null;
        }

        public bool Insert(String nom,DateTime dateD, DateTime dateF, string pays, string type, string nbrJ, int idCat,int idSCat)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = null;
                if (idSCat == -1)
                    query = "INSERT INTO `competition` (`idComp`, `nom`, `dateDeb`, `dateFin`, `idPays`, `typeCompetition`, `nbrJoueur`, `idCat`,`idSousCat`) VALUES(NULL, '" + nom + "', '" + dateD.ToString("yyyy-MM-dd") + "', '" + dateF.ToString("yyyy-MM-dd") + "', '" + Convert.ToInt32(pays) + "', '" + type + "', '" + nbrJ + "', '" + idCat + "',NULL)";
                else
                    query = "INSERT INTO `competition` (`idComp`, `nom`, `dateDeb`, `dateFin`, `idPays`, `typeCompetition`, `nbrJoueur`, `idCat`,`idSousCat`) VALUES(NULL, '" + nom + "', '" + dateD.ToString("yyyy-MM-dd") + "', '" + dateF.ToString("yyyy-MM-dd") + "', '" + Convert.ToInt32(pays) + "', '" + type + "', '" + nbrJ + "', '" + idCat + "','" + idSCat + "')";

                var cmd = new MySqlCommand(query, con.Connection);
              var reader = cmd.ExecuteReader();

                reader.Close();
                return true;
            }
            else
                return false;
        }

        public bool Update(int idcomp,string nom, DateTime dateD, DateTime dateF, string pays, string type, string nbrJ,int idSC)
        {
           DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {          
                string query = "UPDATE `competition` SET `nom` = '" + nom + "', `dateDeb` = '" + dateD.ToString("yyyy-MM-dd") + "', `dateFin` = '" + dateF.ToString("yyyy-MM-dd") + "', `idPays` = '"+pays+"', `typeCompetition` = '"+type+"', `nbrJoueur` = '"+nbrJ+"',idSousCat='"+idSC+"' WHERE `competition`.`idComp` =" + idcomp;
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

        public bool insertJoueurIntoComp(int idJoueur, int idCompetition, int position)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query1 = "Select * FROM ld_joueur_comp WHERE idJoueur="+idJoueur+" AND idComp="+idCompetition;
                var cmd1 = new MySqlCommand(query1, con.Connection);
                var reader1 = cmd1.ExecuteReader();
                
                if (!reader1.HasRows)
                {
                    reader1.Close();
                    string query = "INSERT INTO ld_joueur_comp (idJoueur, idComp, position) VALUES ('" + idJoueur + "', '" + idCompetition + "', '" + position + "')";
                    var cmd = new MySqlCommand(query, con.Connection);
                    var reader = cmd.ExecuteReader();
                    reader.Close();
                }
                else
                {
                    reader1.Close();
                    return false;
                }
                    
                
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