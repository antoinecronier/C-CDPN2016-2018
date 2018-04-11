using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace App3.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class WSPage : Page, INotifyPropertyChanged
    {
        private String baseAdress = "https://jsonplaceholder.typicode.com/";

        public String BaseAdress
        {
            get { return baseAdress; }
            set
            {
                baseAdress = value;
                OnPropertyChanged("BaseAdress");
            }
        }

        private String subAdress = "users/1";

        public String SubAdress
        {
            get { return subAdress; }
            set
            {
                subAdress = value;
                OnPropertyChanged("SubAdress");
            }
        }


        public WSPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void CallWebService2()
        {
            RunAsync(() =>
            {
                Webservice ws = new Webservice(BaseAdress);
                SetUpView2(ws.HttpClientCaller(SubAdress).Result);
            });
        }

        private void SetUpView<T>(T item)
        {
            String output = JsonConvert.SerializeObject(item);
            JObject jObject = JsonConvert.DeserializeObject(output) as JObject;

            this.MainGrid.Children.Add(BuildElement(jObject));
        }

        private void SetUpView2(JObject jObject)
        {
            RunOnUi(() =>
            {
                var element = BuildElement(jObject);
                Grid.SetRow(element, 2);
                Grid.SetColumn(element, 0);
                Grid.SetColumnSpan(element, 2);
                AddElementByStep(this.MainGrid.Children, element);
            });

        }

        private void AddElementByStep(UIElementCollection children, ScrollViewer element)
        {
            ScrollViewer scrollViewer = new ScrollViewer();
            List<UIElement> elements = new List<UIElement>();
            UIElementCollection collection = (element.Content as Grid).Children;

            foreach (var item in (element.Content as Grid).Children)
            {
                elements.Add(item);
            }

            (element.Content as Grid).Children.Clear();

            this.MainGrid.Children.Add(scrollViewer);
            scrollViewer.Content = (element.Content as Grid);

            foreach (var item in elements)
            {
                (scrollViewer.Content as Grid).Children.Add(item);
            }
        }

        private ScrollViewer BuildElement(JObject jObject)
        {

            ScrollViewer scrollViewer = new ScrollViewer();
            Grid content = new Grid();

            if (jObject.Count > 0)
            {
                content.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = GridLength.Auto
                });
                content.ColumnDefinitions.Add(new ColumnDefinition());

                int currentRow = 0;
                foreach (var x in jObject)
                {
                    string name = x.Key;
                    JToken value = x.Value;

                    if (value != null)
                    {
                        content.RowDefinitions.Add(new RowDefinition());

                        TextBlock lbl = new TextBlock();
                        lbl.Text = name;
                        Grid.SetRow(lbl, currentRow);
                        Grid.SetColumn(lbl, 0);
                        content.Children.Add(lbl);

                        if (value.Type == JTokenType.Array)
                        {
                            ScrollViewer subScrollViewer = new ScrollViewer();
                            subScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;

                            StackPanel subGrid = new StackPanel();
                            subGrid.Orientation = Orientation.Horizontal;

                            foreach (var item in value)
                            {
                                subGrid.Children.Add(BuildElement(item.Value<JObject>()));
                            }

                            subScrollViewer.Content = subGrid;

                            Grid.SetRow(subScrollViewer, currentRow);
                            Grid.SetColumn(subScrollViewer, 1);
                            content.Children.Add(subScrollViewer);
                        }
                        else if (value.Type == JTokenType.Object)
                        {
                            ScrollViewer subGrid = BuildElement(value.Value<JObject>());
                            Grid.SetRow(subGrid, currentRow);
                            Grid.SetColumn(subGrid, 1);
                            content.Children.Add(subGrid);
                        }
                        else if (value.ToString().EndsWith(".png") || value.ToString().EndsWith(".jpg"))
                        {
                            Image image = new Image();
                            image.MaxHeight = 120;
                            image.MaxWidth = 120;
                            BitmapImage bmi = new BitmapImage(new Uri(value.ToString()));
                            image.Source = bmi;
                            Grid.SetRow(image, currentRow);
                            Grid.SetColumn(image, 1);
                            content.Children.Add(image);
                        }
                        else
                        {
                            TextBox txtBox = new TextBox();
                            Thickness border = new Thickness(0);
                            txtBox.BorderThickness = border;
                            txtBox.TextWrapping = TextWrapping.Wrap;
                            txtBox.IsReadOnly = true;
                            txtBox.Text = value.ToString();
                            Grid.SetRow(txtBox, currentRow);
                            Grid.SetColumn(txtBox, 1);
                            content.Children.Add(txtBox);
                        }
                    }

                    currentRow++;
                }
            }

            scrollViewer.Content = content;
            return scrollViewer;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.MainGrid.Children.RemoveAt(this.MainGrid.Children.Count - 1);
            CallWebService2();
        }

        public async void RunOnUi(Action action)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                action.Invoke();
            });
        }

        public void RunAsync(Action action)
        {
            Task.Factory.StartNew(action);
        }
    }
}