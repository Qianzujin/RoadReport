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
    /// EquipmentWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentWindow : Window
    {
        //获取父窗口视图模型对象
        EquipmentViewModel equipmentViewModelSelf = new EquipmentViewModel();
        //本窗口视图模型对象
        Equipment equ;
        //操作标志
        string operationFlag;

        //有参构造
        public EquipmentWindow(EquipmentViewModel equipmentViewModel, string OperationFlag)
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            //初始化窗口参数
            for (int i = 0; i < equipmentViewModel.EquipmentView.Count(); i++)
            {
                if (equipmentViewModel.EquipmentView[i].IsChecked == true)
                {
                    equipmentViewModel.EquipmentView[i].IsChecked = false;
                    this.DataContext = equipmentViewModel.EquipmentView[i];
                    this.equ = equipmentViewModel.EquipmentView[i];
                    break;
                }
            }

            this.equipmentViewModelSelf = equipmentViewModel;
            this.operationFlag = OperationFlag;
        }

        //关闭当前窗口
        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //最小化当前窗口
        private void Min(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        //修改图片信息,将图片复制到相对路径下，如果存在则弹窗不复制，反之则复制
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
                this.img.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                //this.img.Source = new BitmapImage(new Uri(@"/Wpf;component/Resources/Picture/" + System.IO.Path.GetFileName(fileDialog.FileName), UriKind.Relative));
            }

        }

        //提交修改信息
        private void Submit(object sender, RoutedEventArgs e)
        {
            //对数据进行校验
            if ((this.img.Source != null && this.datePicker.Text != null) &&
                (this.nameTxtBox.Text != "" && this.codeTxtBox.Text != "" && this.img.Source.ToString() != "" && this.datePicker.Text != "")
            )
            {
                Equipment equ = new Equipment
                {
                    Index = 0,
                    Name = this.nameTxtBox.Text,
                    Code = this.codeTxtBox.Text,
                    Picture = this.img.Source as BitmapImage,
                    IsChecked = false,
                    TermOfValidity = Convert.ToDateTime(this.datePicker.Text.Trim())
                };

                //触发Command更新数据，必须使用同一个视图数据模型
                if (this.operationFlag == "add")
                {
                    //增加索引值
                    var idxList = equipmentViewModelSelf.EquipmentView.Select(a => a.Index).ToList();
                    if (idxList.Count() == 0)
                    {
                        equ.Index = 1;
                    }
                    else
                    {
                        equ.Index = idxList.Max() + 1;
                    }
                   
                    var MyVM = equipmentViewModelSelf;
                    if (MyVM != null && MyVM.InsertCommand.CanExecute(equ))
                        MyVM.InsertCommand.Execute(equ);
                }
                else if (this.operationFlag == "update")
                {
                    //修改索引值
                    equ.Index = this.equ.Index;
                    var MyVM = equipmentViewModelSelf;
                    if (MyVM != null && MyVM.UpdateCommand.CanExecute(equ))
                        MyVM.UpdateCommand.Execute(equ);
                }
                else
                {
                    MessageBox.Show("有一点小问题！");
                }

            }
            else
            {
                MessageBox.Show("请检查数据格式是否正确！");
            }
            this.Close();
        }
    }
}

