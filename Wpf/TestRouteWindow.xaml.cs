using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string OperationFlag;
        private List<PavementType> ptList = new List<PavementType>();
        private TestRouteBase trbUpdate;

        public TestRouteWindow(TestRouteViewModel testRouteViewModel, string OperationFlag, TestRouteBase trb)
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


         
            this.DataContext = testRouteViewModel;
            this.trbUpdate = trb;
            //this.DataContext = trb;
            this.OperationFlag = OperationFlag;
        }



        private void Min(object sender, MouseButtonEventArgs e)
        {

        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //编辑
        private void EditClick(object sender, RoutedEventArgs e)
        {
            //获取当前选择路面类型
            PavementType pt = this.pavementTypeInfoDataGrid.SelectedItem as PavementType;
            int idx = -1;//记录路面类型在私有变量中的位置
            for (int i = 0; i < ptList.Count(); i++)
            {
                if (ptList[i] == pt)
                { idx = i; }
            }
          
            //打开修改路面类型窗口
            PavementTypeWindow pavementTypeWindow = new PavementTypeWindow(testRouteViewModelSelf, pt);
            pavementTypeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pavementTypeWindow.ShowDialog();

            if (idx != -1)
            {
                //当修改当前路面类型时，将修改过的路面类型进行赋值
                ptList[idx] = pavementTypeWindow.pt;
            }

            //更新当前视图
            var MyVM = this.testRouteViewModelSelf;
            if (MyVM != null && MyVM.UpdatePavementTypeCommand.CanExecute(ptList))
                MyVM.UpdatePavementTypeCommand.Execute(ptList);
        }

        //提交 需要判断新增还是编辑
        private void Submit(object sender, RoutedEventArgs e)
        {
           
            if (OperationFlag == "Insert")
            {
                TestRouteBase trb = new TestRouteBase
                {
                    Index = 0,
                    LineMileage = this.trbLineMileage.Text,
                    Material = this.trbMaterial.Text,
                    Picture = this.trbPicture.Source as BitmapImage,
                    TestRoutes = this.trbTestRoutes.Text,
                    Time = this.trbTime.Text

                };

                //如果是Insert必须保证路面类型必须有一条
                TestRoute tr = new TestRoute { TestRouteBase = trb, PavementTypeInfo = ptList };

                var MyVM = this.testRouteViewModelSelf;
                if (MyVM != null && MyVM.InsertCommand.CanExecute(tr))
                    MyVM.InsertCommand.Execute(tr);
            }
            else if(OperationFlag == "Update") {

                TestRouteBase trb = new TestRouteBase
                {
                    Index = this.trbUpdate.Index,
                    LineMileage = this.trbLineMileage.Text,
                    Material = this.trbMaterial.Text,
                    Picture = this.trbPicture.Source as BitmapImage,
                    TestRoutes = this.trbTestRoutes.Text,
                    Time = this.trbTime.Text
                };

                TestRoute tr = new TestRoute { TestRouteBase = trb, PavementTypeInfo = ptList };

                var MyVM = this.testRouteViewModelSelf;
                if (MyVM != null && MyVM.UpdateCommand.CanExecute(tr))
                    MyVM.UpdateCommand.Execute(tr);
            }
           
        }

        private bool VerifyPavementType(PavementType pt)
        {
            if (pt != null)
            {
                return true;
            }
            return false;
        }

        //插入一项路面类型
        private void Insert(object sender, RoutedEventArgs e)
        {
            //获取增加数据时Index索引

            //当当前操作是插入一条路面类型，首先需要知道该操作是否,必须保证
            if (OperationFlag == "Insert")
            {
                //如果是插入则自己产生Index索引，如何告诉下级上级是插入还是更新
                PavementTypeWindow pavementTypeWindow = new PavementTypeWindow(testRouteViewModelSelf, null);
                pavementTypeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pavementTypeWindow.ShowDialog();
                if (pavementTypeWindow.pt != null)
                {
                    this.ptList.Add(pavementTypeWindow.pt);//获取子窗口pt路面类型信息
                }

                //更新当前视图
                var MyVM = this.testRouteViewModelSelf;
                if (MyVM != null && MyVM.UpdatePavementTypeCommand.CanExecute(ptList))
                    MyVM.UpdatePavementTypeCommand.Execute(ptList);
            }
            else if(OperationFlag == "Update")
            {
                //获取视图中的路面类型
                foreach (var item in this.testRouteViewModelSelf.pavementTypeView)
                {
                    this.ptList.Add(item);
                }
                //如果是更新则使用Index
                PavementTypeWindow pavementTypeWindow = new PavementTypeWindow(testRouteViewModelSelf, null);
                pavementTypeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pavementTypeWindow.ShowDialog();
                if (pavementTypeWindow.pt != null)
                {
                    this.ptList.Add(pavementTypeWindow.pt);//获取子窗口pt路面类型信息
                }

                //更新当前视图
                var MyVM = this.testRouteViewModelSelf;
                if (MyVM != null && MyVM.UpdatePavementTypeCommand.CanExecute(ptList))
                    MyVM.UpdatePavementTypeCommand.Execute(ptList);
            }

        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "jpg图片|*.jpg|png图片|*.png|所有文件|*.*";
            fileDialog.ShowDialog();

            if (fileDialog.FileName != "")
            {
                //获取文件名，复制到相对路径下
                System.IO.Path.GetFileName(fileDialog.FileName);
                var path = Environment.CurrentDirectory + "\\Resources\\Picture\\" + System.IO.Path.GetFileName(fileDialog.FileName);

                if (File.Exists(path))
                {
                    MessageBox.Show("文件已存在！");
                    //return;
                }
                else
                {
                    File.Copy(fileDialog.FileName, path);
                }
                this.trbPicture.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                //this.img.Source = new BitmapImage(new Uri(@"/Wpf;component/Resources/Picture/" + System.IO.Path.GetFileName(fileDialog.FileName), UriKind.Relative));
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var pavementType = this.pavementTypeInfoDataGrid.SelectedItem as PavementType;
            ptList.Remove(pavementType);
            //更新当前视图
            var MyVM = this.testRouteViewModelSelf;
            if (MyVM != null && MyVM.DeletePavementTypeCommand.CanExecute(ptList))
                MyVM.DeletePavementTypeCommand.Execute(pavementType);
        }
    }
}
