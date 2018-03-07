using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace App3.UserControls
{
    public sealed partial class UserUserControl : UserControl
    {
        private User user;

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public UserUserControl()
        {
            User = new User
            {
                Firstname = "testFirstname",
                Lastname = "testLastname",
                Sold = 1000,
                Bag = new List<Product>()
                {
                    new Product
                    {
                        Name = "product1",
                        Price = 5,
                        Quantity = 1
                    },
                    new Product
                    {
                        Name = "product2",
                        Price = 50,
                        Quantity = 10
                    },new Product
                    {
                        Name = "product3",
                        Price = 5.6,
                        Quantity = 4
                    }
                }
            };

            this.InitializeComponent();
            this.DataContext = User;

            Task.Factory.StartNew(()=>
            {
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                User.Firstname = "toto";
            });
        }
    }
}
