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
        private string OperationFlag;

        public TestRouteWindow(TestRouteViewModel testRouteViewModel, string OperationFlag, int Index)
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


            if (Index != -1)
            {
                //获取选中项，触发路面类型视图对象更新
                for (int i = 0; i < testRouteViewModel.TestRouteBaseView.Count(); i++)
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


            this.OperationFlag = OperationFlag;
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
            PavementTypeWindow pavementTypeWindow = new PavementTypeWindow(testRouteViewModelSelf, "", Index, pavementType.Id);
            pavementTypeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pavementTypeWindow.Show();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {

            //如果是Insert必须保证路面类型必须有一条
        }

        //插入一项路面类型
        private void Insert(object sender, RoutedEventArgs e)
        {
            //获取增加数据时Index索引


            ////获取增加数据时Index索引
            //if (OperationFlag == "Insert")
            //{
            //  
            //}

            //当当前操作是插入一条路面类型，首先需要知道该操作是否,必须保证
            if (OperationFlag == "Insert")
            {
                var idxList = testRouteViewModelSelf.TestRouteBaseView.Select(a => a.Index).ToList();
                if (idxList.Count() == 0)
                {
                    Index = 1;
                }
                else
                {
                    Index = idxList.Max() + 1;
                }

                //如果是插入则自己产生Index索引，如何告诉下级上级是插入还是更新
                PavementTypeWindow pavementTypeWindow = new PavementTypeWindow(testRouteViewModelSelf, "Insert", Index, -1);
                pavementTypeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pavementTypeWindow.Show();
            }
            else {
                //如果是更新则使用Index
                PavementTypeWindow pavementTypeWindow = new PavementTypeWindow(testRouteViewModelSelf, "Insert", Index, -1);
                pavementTypeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pavementTypeWindow.Show();
            }

        }
    }
}
