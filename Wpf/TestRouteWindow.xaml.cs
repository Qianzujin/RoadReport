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
using Wpf.Model;
using Wpf.ViewModel;

namespace Wpf
{
    /// <summary>
    /// TestRouteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestRouteWindow : Window
    {
        private TestRouteViewModel testRouteViewModelSelf;
        private int Index = -1;

        public TestRouteWindow(TestRouteViewModel testRouteViewModel, string OperationFlag,int Index)
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            this.testRouteViewModelSelf = testRouteViewModel;

            //获取选中项，触发路面类型视图对象更新
            for (int i = 0; i <  testRouteViewModel.TestRouteBaseView.Count();i++)
            {
                if (testRouteViewModel.TestRouteBaseView[i].Index == Index)
                {
                    testRouteViewModel.UpdatePavementTypeViewData(testRouteViewModel.TestRouteBaseView[i].Index);
                }
            }

            //将路面类型赋值给数据源
            this.DataContext = testRouteViewModel;
            this.Index = Index;
        }



        private void Min(object sender, MouseButtonEventArgs e)
        {

        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            var pavementType = this.pavementTypeInfoDataGrid.SelectedItem as PavementType;
            
            PavementTypeWindow pavementTypeWindow = new PavementTypeWindow(testRouteViewModelSelf,"", Index, pavementType.Id);
            pavementTypeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pavementTypeWindow.Show();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {

        }
    }
}
