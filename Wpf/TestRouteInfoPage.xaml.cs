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

        /// <summary>
        /// 增加试验路线信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTestRouteInfo(object sender, RoutedEventArgs e)
        {
            //testRouteViewModel, "Insert", null
            TestRouteWindow testRouteWindow = new TestRouteWindow(testRouteViewModel,"Insert");
            testRouteWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;       
            testRouteWindow.Show();
            testRouteViewModel.TestRouteMessage(null);
        }

        /// <summary>
        /// 选择试验路线信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectTestRouteInfo(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 编辑试验路线信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClick(object sender, RoutedEventArgs e)
        {
            //获取当前数据传给修改窗口
            TestRouteBase trb = this.testRouteDataGrid.SelectedItem as TestRouteBase;
            //更新pavementType修改窗口的视图数据
            //更新当前视图
            var MyVM = this.testRouteViewModel;
            if (MyVM != null && MyVM.SelectPavementTypeCommand.CanExecute(trb))
                MyVM.SelectPavementTypeCommand.Execute(trb);

            //显示前更新路面类型数据//testRouteViewModel, "Update", trb
            TestRouteWindow testRouteWindow = new TestRouteWindow(testRouteViewModel,"Update");
            testRouteWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            testRouteWindow.Show();
            testRouteViewModel.TestRouteMessage(trb);
        }
    }
}
