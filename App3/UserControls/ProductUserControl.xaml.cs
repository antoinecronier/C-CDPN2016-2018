using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ProductUserControl : UserControl
    {
        private Product product;

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public ProductUserControl()
        {
            Product = new Product
            {
                Name = "myProduct",
                Price = 20,
                Quantity = 5
            };

            this.InitializeComponent();
            this.DataContext = Product;
        }
    }
}
