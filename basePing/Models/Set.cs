using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using basePing.DataContext;

namespace basePing.Models
{
    public class Set
    {
        private int id;
        private int pointJ1;
        private int pointJ2;

        public int Id 
        {
            get { return id; }
            set { id = value; }
        }

        public int PointJ1
        {
            get { return pointJ1; }
            set { pointJ1 = value; }
        }

        public int PointJ2
        {
            get { return pointJ2; }
            set { pointJ2 = value; }
        }


        public Set(int id , int pointJ1, int pointJ2)
        {
            this.id = id;
            this.pointJ1 = pointJ1;
            this.pointJ2 = pointJ2;
        }

        public Set()
        {
        }

        public List<Set> GetList(int idR)
        {
            DCSet dc = new DCSet();
            return dc.findAll(idR);
        }
    }
}