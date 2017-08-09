using basePing.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.DataContext
{
    public class DCPoule
    {
        public List<Poule> findAll(int id)
        {
            List<Poule> lPoule = new List<Poule>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Serie INNER JOIN Poule on Serie.idSerie=Poule.idSerie WHERE idComp="+id;
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lPoule.Add(new Poule(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(4),reader.GetString(5)));
                }
                reader.Close();
                return lPoule;
            }
            else
                return null;
        }

        internal void findAll()
        {
            throw new NotImplementedException();
        }
    }
}