using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.Models;

namespace basePing.DataContext
{
    public class DCLogin
    {
        public bool find(string login,string pwd)
        {
            Moderateur m=null;
            
            DBConnection con = DBConnection.Instance();
            if (con.IsConnect())
            {
                string query = "SELECT * FROM Moderateur WHERE login='" + login +"' AND password=SHA1('"+pwd+"')";
                var cmd = new MySqlCommand(query, con.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    m = new Moderateur(reader.GetInt32(0), reader.GetString(1),reader.GetString(2));
                }
                reader.Close();
                if (m != null)
                    return true;
                else
                    return false;
            }else
                return false;
        }
    }
}