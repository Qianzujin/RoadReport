using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        private List<PavementType> ptInfo = new List<PavementType>();
        private TestRoute trSelf = new TestRoute();
        private TestRouteBase trbSelf = new TestRouteBase();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="testRouteViewModel"></param>
        /// <param name="OperationFlag"></param>
        public TestRouteWindow(TestRouteViewModel testRouteViewModel, string OperationFlag)
        {
            InitializeComponent();

            Messenger.Default.Register<TestRoute>(this, "TestRouteMessage", TestRouteMessage);

            this.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            this.testRouteViewModelSelf = testRouteViewModel;
            this.OperationFlag = OperationFlag;
        }

        /// <summary>
        /// 通过Messenger发送消息获取TestRoute信息
        /// </summary>
        /// <param name="tr">选中的测试路线</param>
        private void TestRouteMessage(TestRoute tr)
        {
            //拷贝tr数据

            //首先拷贝路面类型数据
            //ptInfo.Clear();
            foreach (var item in tr.pavementTypeInfo)
            {
                PavementType r = Mapper<PavementType, PavementType>(item);
                ptInfo.Add(r);
            }

            //拷贝路面基础信息数据
            trbSelf = Mapper<TestRouteBase, TestRouteBase>(tr.TestRouteBase);

            trSelf = new TestRoute { TestRouteBase = trbSelf, pavementTypeInfo = ptInfo };

            this.pavementTypeInfoDataGrid.ItemsSource = trSelf.PavementTypeInfo;
            this.DataContext = trSelf;
        }


        /// <summary>
        /// 反射实现两个类的对象之间相同属性的值的复制
        /// 适用于初始化新实体
        /// </summary>
        /// <typeparam name="D">返回的实体</typeparam>
        /// <typeparam name="S">数据源实体</typeparam>
        /// <param name="s">数据源实体</param>
        /// <returns>返回的新实体</returns>
        public static D Mapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>(); //构造新实例
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  

                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }

        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Min(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 编辑按钮 点击DataGrid中的编辑按钮，对路面类型信息进行编辑，记录选中的索引，等待路面类型修改窗
        /// 口关闭后，对对应的索引值直接赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="ptInfo">存储路面类型信息</param>
        private void EditClick(object sender, RoutedEventArgs e)
        {
            PavementType pt = this.pavementTypeInfoDataGrid.SelectedItem as PavementType;
            int idx = -1;//记录路面类型在私有变量中的位置
            for (int i = 0; i < ptInfo.Count(); i++)
            {
                if (ptInfo[i] == pt)
                { idx = i; }
            }

            //打开修改路面类型窗口
            PavementTypeWindow pavementTypeWindow = new PavementTypeWindow(testRouteViewModelSelf, pt);
            pavementTypeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pavementTypeWindow.ShowDialog();

            //当修改当前路面类型时，将修改过的路面类型进行赋值
            if (idx != -1)
            {
                //trSelf.PavementTypeInfo[idx] = pavementTypeWindow.ptSelf;
                ptInfo[idx] = pavementTypeWindow.ptSelf;
            }

          //  if (OperationFlag == "Update")
          //  {
                //更新当前视图
                this.pavementTypeInfoDataGrid.ItemsSource = null;
                this.pavementTypeInfoDataGrid.ItemsSource = ptInfo;
                // ptList = tr.PavementTypeInfo;
           // }
           // else
           // {
           //     //更新当前视图
           //     this.pavementTypeInfoDataGrid.ItemsSource = null;
           //     this.pavementTypeInfoDataGrid.ItemsSource = trSelf.PavementTypeInfo;
           //     ptInfo = trSelf.PavementTypeInfo;
           // }
            //var MyVM = this.testRouteViewModelSelf;
            //if (MyVM != null && MyVM.UpdatePavementTypeCommand.CanExecute(ptList))
            //    MyVM.UpdatePavementTypeCommand.Execute(ptList);
        }

        /// <summary>
        /// 提交 需要判断新增还是编辑,通过父窗口传过来的提交测试路线信息还是修改测试路线信息 
        /// 调用对应的command命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="OperationFlag">操作类型</param>
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
                TestRoute tr = new TestRoute { TestRouteBase = trb, PavementTypeInfo = ptInfo };

                var MyVM = this.testRouteViewModelSelf;
                if (MyVM != null && MyVM.InsertCommand.CanExecute(tr))
                    MyVM.InsertCommand.Execute(tr);
            }
            else if (OperationFlag == "Update")
            {
                TestRouteBase trb = new TestRouteBase
                {
                    Index = this.trSelf.TestRouteBase.Index,
                    LineMileage = this.trbLineMileage.Text,
                    Material = this.trbMaterial.Text,
                    Picture = this.trbPicture.Source as BitmapImage,
                    TestRoutes = this.trbTestRoutes.Text,
                    Time = this.trbTime.Text
                };

                TestRoute tr = new TestRoute { TestRouteBase = trb, PavementTypeInfo = ptInfo };

                var MyVM = this.testRouteViewModelSelf;
                if (MyVM != null && MyVM.UpdateCommand.CanExecute(tr))
                    MyVM.UpdateCommand.Execute(tr);
            }
            this.Close();
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool VerifyPavementType(PavementType pt)
        {
            if (pt != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 插入一条路面类型信息，点击该按钮，会弹窗pavement，填写完整则添加一条路面类型信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Insert(object sender, RoutedEventArgs e)
        {
            //获取增加数据时Index索引
            PavementTypeWindow pavementTypeWindow = new PavementTypeWindow(testRouteViewModelSelf, null);
            pavementTypeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pavementTypeWindow.ShowDialog();
            if (pavementTypeWindow.ptSelf.Name != null)
            {
                this.ptInfo.Add(pavementTypeWindow.ptSelf);//获取子窗口pt路面类型信息
            }

            this.pavementTypeInfoDataGrid.ItemsSource = null;
            this.pavementTypeInfoDataGrid.ItemsSource = this.ptInfo;

        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="ptInfo">存储路面类型信息</param>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            PavementType pavementType = this.pavementTypeInfoDataGrid.SelectedItem as PavementType;
            ptInfo.Remove(pavementType);
            //更新当前视图
            //更新当前视图
            this.pavementTypeInfoDataGrid.ItemsSource = null;
            this.pavementTypeInfoDataGrid.ItemsSource = ptInfo;

        }
    }
}
