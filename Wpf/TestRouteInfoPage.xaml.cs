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
using Wpf.Model;
using Wpf.ViewModel;

namespace Wpf
{
    /// <summary>
    /// TestRouteInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class TestRouteInfoPage : Page
    {
        TestRouteViewModel testRouteViewModel = new TestRouteViewModel();
        public TestRouteInfoPage()
        {
            InitializeComponent();
            this.DataContext = testRouteViewModel;
        }

        private void AddTestRouteInfo(object sender, RoutedEventArgs e)
        {

        }

        private void SelectTestRouteInfo(object sender, RoutedEventArgs e)
        {

        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            //获取当前数据传给修改窗口
            TestRouteBase trb = this.testRouteDataGrid.SelectedItem as TestRouteBase;
            //var tr = testRouteViewModel.GetCurrectTestRoute(trb.Index);
            trb.IsChecked = true;
            //equ.IsChecked = true;
            //显示前更新路面类型数据
            TestRouteWindow testRouteWindow = new TestRouteWindow(testRouteViewModel, "update", trb.Index);
            testRouteWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            testRouteWindow.Show();
        }
    }
}
