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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.ViewModel;

namespace Wpf
{
    /// <summary>
    /// CarInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class CarInfoPage : Page
    {
        CarViewModel carViewModel = new CarViewModel(); 

        public CarInfoPage()
        {
            InitializeComponent();
            this.DataContext = carViewModel;
        }

        private void carNameKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void carCodeKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void EditClick(object sender, RoutedEventArgs e)
        {

        }

        private void SelectCarInfo(object sender, RoutedEventArgs e)
        {

        }

        private void AddCarInfo(object sender, RoutedEventArgs e)
        {

        }
    }
}
