using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;
using MySql.Data.MySqlClient;

namespace basePing.DataContext
{
    public class DCSet
    {
        public List<Set> findAll(int id) {

            List<Set> lSet = new List<Set>();
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {

                string query = "Select * FROM `set` WHERE idMatch=" + id;

                var cmd = new MySqlCommand(query, con.Connection);
             var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lSet.Add(new Set(reader.GetInt32(0), reader.GetInt32(2), reader.GetInt32(3)));
                }
                reader.Close();
                return lSet;
            }
            return null;

        }

        public bool Insert(int idM, int point1, int point2)
        {
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "INSERT INTO `set` (idMatch,point1,point2) VALUES ('" + idM + "', '" +point1+ "', '" + point2 + "')";
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