using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace App3.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class GraphicPaint : Page
    {
        PointerPoint currentPoint;
        Boolean haveToPrint = true;
        CancellationTokenSource cancelTokenS;

        public GraphicPaint()
        {
            InitializeComponent();
        }

        public void squaring(int delay, CancellationToken token)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    Random rand = new Random();
                    printSquare(0, 0, rand.Next(0, 255).ToString(), rand.Next(0, 255).ToString(), rand.Next(0, 255).ToString());
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cancelTokenS.Token);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    Random rand = new Random();
                    printSquare(600 - 95, 0, rand.Next(0, 255).ToString(), rand.Next(0, 255).ToString(), rand.Next(0, 255).ToString());
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cancelTokenS.Token);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    Random rand = new Random();
                    printSquare(0, 500 - 115, rand.Next(0, 255).ToString(), rand.Next(0, 255).ToString(), rand.Next(0, 255).ToString());
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cancelTokenS.Token);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    Random rand = new Random();
                    printSquare(600 - 95, 500 - 115, rand.Next(0, 255).ToString(), rand.Next(0, 255).ToString(), rand.Next(0, 255).ToString());
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cancelTokenS.Token);
        }

        public void printSquare(int x, int y, String r, String g, String b)
        {
            RunOnUi(()=>
            {
                Rectangle line = new Rectangle();

                line.Stroke = new SolidColorBrush();

                line.Height = 80;
                line.Width = 80;
                var brush = new SolidColorBrush(Color.FromArgb(
                    Byte.Parse(r),
                    Byte.Parse(g),
                    Byte.Parse(b),
                    Byte.Parse("0")
                    ));
                line.Fill = brush;

                Canvas.SetLeft(line, x);
                Canvas.SetTop(line, y);

                paintSurface.Children.Add(line);
            });

        }


        private void Canvas_MouseDown_1(object sender, PointerRoutedEventArgs e)
        {
            if (haveToPrint)
            {
                if (e.Pointer.IsInContact == true)
                    currentPoint = e.GetCurrentPoint(this);

                Rectangle line = new Rectangle();

                line.Stroke = new SolidColorBrush();
                /*line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(this).X;
                line.Y2 = e.GetPosition(this).Y;*/
                line.Height = 80;
                line.Width = 80;
                Random rand = new Random();
                var brush = new SolidColorBrush(Color.FromArgb(
                    Byte.Parse(rand.Next(0, 255).ToString()),
                    Byte.Parse(rand.Next(0, 255).ToString()),
                    Byte.Parse(rand.Next(0, 255).ToString()),
                    0
                    ));
                line.Fill = brush;

                Canvas.SetLeft(line, currentPoint.Position.X);
                Canvas.SetTop(line, currentPoint.Position.Y);

                currentPoint = e.GetCurrentPoint(this);

                paintSurface.Children.Add(line);

                haveToPrint = false;
            }
        }

        private void Canvas_MouseMove_1(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.IsInContact == true
                && currentPoint != e.GetCurrentPoint(this))
            {
                Rectangle line = new Rectangle();

                line.Stroke = new SolidColorBrush();
                /*line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(this).X;
                line.Y2 = e.GetPosition(this).Y;*/
                Random rand = new Random();
                line.Height = rand.Next(0, 80);
                line.Width = rand.Next(0, 80);
                var brush = new SolidColorBrush(Color.FromArgb(
                Byte.Parse(rand.Next(0, 255).ToString()),
                Byte.Parse(rand.Next(0, 255).ToString()),
                Byte.Parse(rand.Next(0, 255).ToString()),
                0
                ));
                line.Fill = brush;

                Canvas.SetLeft(line, currentPoint.Position.X);
                Canvas.SetTop(line, currentPoint.Position.Y);

                currentPoint = e.GetCurrentPoint(this);
                paintSurface.Children.Add(line);
            }
        }

        private void paintSurface_MouseUp(object sender, PointerRoutedEventArgs e)
        {
            haveToPrint = true;
        }

        private void paintSurface_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.VirtualKey == VirtualKey.A)
            {
                cancelTokenS = new CancellationTokenSource();
                squaring(150, cancelTokenS.Token);
            }
            else if (e.VirtualKey == VirtualKey.Z)
            {
                cancelTokenS.Cancel();
                cancelTokenS.Dispose();
            }
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
