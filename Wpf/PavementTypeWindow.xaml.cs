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
using static Wpf.ViewModel.TestRouteViewModel;

namespace Wpf
{
    /// <summary>
    /// PavementTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PavementTypeWindow : Window
    {
        private TestRouteViewModel testRouteViewModelSelf;
        private int Index = -1;
        private int Id = -1;

        public PavementTypeWindow(TestRouteViewModel testRouteViewModel, string OperationFlag, int Index, int Id)
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
            this.DataContext = testRouteViewModel.PavementTypeView[Id];
            this.Index = Index;
            this.Id = Id;
        }

        private void Min(object sender, MouseButtonEventArgs e)
        {

        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            //获取当前操作属性
            //获取当前操作对象 Index
            //获取当前的值
            PavementType pt = new PavementType
            {
                Id = this.Id,
                Index = this.Index,
                Name = this.ptName.Text,
                Length = this.ptLength.Text,
                Percent = this.ptPercent.Text,
                Picture = this.ptPicture.Source as BitmapImage
            };

    
            var MyVM = this.testRouteViewModelSelf;
            if (MyVM != null && MyVM.UpdatePavementTypeCommand.CanExecute(pt))
                MyVM.UpdatePavementTypeCommand.Execute(pt);

            //获取选中的测试路线路面信息
            // foreach (var item in this.testRouteViewModelSelf.testRouteList)
            // {
            //     if (item.TestRouteBase.Index == Index)
            //     {
            //         for (int i = 0; i < item.PavementTypeInfo.Count(); i++)
            //         {
            //             if (item.PavementTypeInfo[i].Id == this.Id)
            //             {
            //                 //找到需要修改的元素PavementType
            //             }
            //         }
            //     }
            // }

            ////获取选中的测试路线基础信息
            //foreach (var item in this.testRouteViewModelSelf.TestRouteBaseView)
            //{
            //    if (item.Index == Index)
            //    {
            //        TestRoute tr = new TestRoute { PavementTypeInfo = pt, TestRouteBase = item };
            //    }
            //}

        }
    }
}
