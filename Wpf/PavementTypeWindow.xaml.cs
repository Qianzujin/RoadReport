﻿using Microsoft.Win32;
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
using static Wpf.ViewModel.TestRouteViewModel;

namespace Wpf
{
    /// <summary>
    /// PavementTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PavementTypeWindow : Window
    {
        public PavementType ptSelf = new PavementType();
        private PavementType pt = new PavementType();

        public PavementTypeWindow(TestRouteViewModel testRouteViewModel, PavementType pts)
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            if (pts != null)
            {
                this.ptSelf = Mapper<PavementType, PavementType>(pts);
                this.DataContext = this.ptSelf;
                this.pt = pts;
            }
            else
            {
                this.DataContext = this.ptSelf;
            }
        }

        private void Min(object sender, MouseButtonEventArgs e)
        {

        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            ptSelf = this.pt;
            this.Close();
        }

        /// <summary>
        /// 提交路面类型信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit(object sender, RoutedEventArgs e)
        {

            if (Verify() == true)
            {
                OKWindow okWindow = new OKWindow("提交数据", "确实要提交这条数据吗？");
                okWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                okWindow.ShowDialog();
                if (okWindow.Ret == true)
                {
                    this.Close();
                }
                else {
                    return;
                }
            }
            else
            {
                MessageBox.Show("请检查数据格式！");
            }

        }

        /// <summary>
        /// 加载图片通用方法
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
                this.ptPicture.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                this.ptSelf.Picture = new BitmapImage(new Uri(path, UriKind.Absolute));
                //this.img.Source = new BitmapImage(new Uri(@"/Wpf;component/Resources/Picture/" + System.IO.Path.GetFileName(fileDialog.FileName), UriKind.Relative));
            }
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
        /// 数据校验
        /// </summary>
        /// <returns></returns>
        private bool Verify()
        {
            if (this.ptName.Text != "" &&
                this.ptLength.Text != "" &&
                this.ptPercent.Text != "" &&
                this.ptPicture.Source != null &&
                this.ptPicture.Source.ToString() != "" )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
