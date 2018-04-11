using ClassLibrary3.Models.Bases;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class Product : PropertyChangeEntity
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private int id;
        private String name;
        private double price;
        private int quantity;
        private int userId;
        #endregion

        #region Properties
        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Name
        {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public double Price
        {
            get { return price; }
            set {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Product()
        {

        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        #endregion

        #region Events
        #endregion


    }
}
