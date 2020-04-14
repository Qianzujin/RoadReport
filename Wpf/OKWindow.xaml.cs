using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf
{
    /// <summary>
    /// OKWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OKWindow : Window
    {
        public bool Ret = false;
        public OKWindow(string title,string message) 
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            this.title.Text = title;
            this.message.Text = message;

        }

        private void ClickOK(object sender, RoutedEventArgs e)
        {
            Ret = true;
            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Min(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
