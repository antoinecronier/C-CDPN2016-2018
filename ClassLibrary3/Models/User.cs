using ClassLibrary3.Models.Bases;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class User : PropertyChangeEntity
    {
        #region Attributs
        private int id;
        private String firstname;
        private String lastname;
        private double sold;
        private ObservableCollection<Product> bag = new ObservableCollection<Product>();
        #endregion
        #region Properties
        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Firstname
        {
            get { return firstname; }
            set {
                firstname = value;
                OnPropertyChanged("Firstname");
            }
        }

        public String Lastname
        {
            get { return lastname; }
            set {
                lastname = value;
                OnPropertyChanged("Lastname");
            }
        }
        public double Sold
        {
            get { return sold; }
            set {
                sold = value;
                OnPropertyChanged("Sold");
            }
        }

        [Ignore]
        public ObservableCollection<Product> Bag
        {
            get { return bag; }
            set {
                bag = value;
                OnPropertyChanged("Bag");
            }
        }
        #endregion
        #region Constructors
        public User()
        {

        }
        #endregion
    }
}
