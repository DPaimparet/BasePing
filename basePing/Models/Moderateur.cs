using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basePing.Models
{
    public class Moderateur
    {
        private int id;
        private string login;
        private string email;
        
        public int Id { get { return id; } set { id = value; } }
        public string Login { get { return HttpUtility.HtmlDecode(login); } set {  login = value; } }

        public string Email{ get { return HttpUtility.HtmlDecode(email); } set { email = value; } }


        public Moderateur(int id, string login, string email)
        {
            this.id=id;
            this.login = login;
            this.email = email;

        }
    }
}