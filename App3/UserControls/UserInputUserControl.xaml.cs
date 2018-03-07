using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
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
    public sealed partial class UserInputUserControl : UserControl
    {
        private User user = new User();

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        private Product product = new Product();

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }


        public UserInputUserControl()
        {
            this.InitializeComponent();
            this.DataContext = User;
            //this.UCProductInput.DataContext = Product;

            this.saveBtn.Tapped += SaveBtn_Tapped;
            this.UCProductInput.SaveBtn.Tapped += SaveBtn_Tapped1;
            #region Test
            //Task.Factory.StartNew(()=>
            //{
            //    int a = 0;
            //    while (true)
            //    {
            //        //Task.Delay(TimeSpan.FromSeconds(2)).Wait();
            //        CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
            //            Windows.UI.Core.CoreDispatcherPriority.Normal,
            //            () =>
            //            {
            //                User.Firstname = "toto"+a;
            //            });
            //        a++;
            //    }
            //});
            #endregion

        }

        private void SaveBtn_Tapped1(object sender, TappedRoutedEventArgs e)
        {
            //User.Bag.Add(Product);
            User.Bag.Add(new Product
            {
                Name = this.UCProductInput.Product.Name,
                Price = this.UCProductInput.Product.Price,
                Quantity = this.UCProductInput.Product.Quantity
            });
        }

        private void SaveBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }
    }
}
