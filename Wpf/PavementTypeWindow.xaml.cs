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
using static Wpf.ViewModel.TestRouteViewModel;

namespace Wpf
{
    /// <summary>
    /// PavementTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PavementTypeWindow : Window
    {
        //private TestRouteViewModel testRouteViewModelSelf;
        public PavementType pt= new PavementType();

        public PavementTypeWindow(TestRouteViewModel testRouteViewModel,PavementType pt)
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            this.DataContext = pt;
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
            // pt = new PavementType
            // {
            //     Id = 0,
            //     Index = 0,
            //     Name = this.ptName.Text,
            //     Length = this.ptLength.Text,
            //     Percent = this.ptPercent.Text,
            //     Picture = this.ptPicture.Source as BitmapImage
            // };

            pt.Name = this.ptName.Text;
            pt.Length = this.ptLength.Text;
            pt.Percent = this.ptPercent.Text;
            pt.Picture = this.ptPicture.Source as BitmapImage;

            this.Close();
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
                this.ptPicture.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                //this.img.Source = new BitmapImage(new Uri(@"/Wpf;component/Resources/Picture/" + System.IO.Path.GetFileName(fileDialog.FileName), UriKind.Relative));
            }
        }
    }
}
