﻿using ClassLibrary1.Models;
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
    public sealed partial class ProductInputUserControl : UserControl
    {
        private Product product = new Product();

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public Button SaveBtn
        {
            get { return this.saveBtn; }
            set { this.saveBtn = value; }
        }

        public ProductInputUserControl()
        {
            this.InitializeComponent();
            this.DataContext = Product;
            this.saveBtn.Tapped += SaveBtn_Tapped;
        }

        private void SaveBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }
    }
}
