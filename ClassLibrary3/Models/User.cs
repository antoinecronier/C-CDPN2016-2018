using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class User
    {
        #region Attributs
        private String firstname;
        private String lastname;
        private double sold;
        private List<Product> bag;
        #endregion
        #region Properties
        public String Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public String Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public double Sold
        {
            get { return sold; }
            set { sold = value; }
        }
        public List<Product> Bag
        {
            get { return bag; }
            set { bag = value; }
        }
        #endregion
        #region Constructors
        public User()
        {

        }
        #endregion
    }
}
