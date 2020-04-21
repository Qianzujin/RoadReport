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
    /// UserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : Window
    {
        private UserViewModel userViewModelSelf = new UserViewModel();
        private string operationFlag = "";
        public UserWindow(UserViewModel userViewModel, User user, string OperationFlag)
        {
            InitializeComponent();
            this.userViewModelSelf = userViewModel;
            this.DataContext = user;
            this.operationFlag = OperationFlag;
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
            if (Verify() != true)
            {
                MessageBox.Show("请检查数据格式！");
            }

            User usr = this.DataContext as User;
            int index = 0 ;
            if (usr != null)
            {
                index = usr.Index;
            }
            User user = new User { Index = index, UserName =this.usrName.Text, PassWord= this.usrPassWord.Text,
                 Type = this.usrType.Text , Picture = this.usrPicture.Source as BitmapImage
            };

            if (this.operationFlag == "Update")
            {
                var MyVM = this.userViewModelSelf;
                if (MyVM != null && MyVM.UpdateCommand.CanExecute(user))
                {
                    MyVM.UpdateCommand.Execute(user);
                }
                this.Close();
            }
            else if (this.operationFlag == "Insert")
            {
                var MyVM = this.userViewModelSelf;
                if (MyVM != null && MyVM.InsertCommand.CanExecute(user))
                {
                    MyVM.InsertCommand.Execute(user);
                }
                this.Close();
            }         
            
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
                this.usrPicture.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                //this.img.Source = new BitmapImage(new Uri(@"/Wpf;component/Resources/Picture/" + System.IO.Path.GetFileName(fileDialog.FileName), UriKind.Relative));
            }

        }



        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool Verify()
        {
            if (this.usrName.Text != "" &&
                this.usrPassWord.Text != "" &&
                this.usrType.Text != "" &&
                this.usrPicture.Source != null &&
                this.usrPicture.Source.ToString() != "")
            {
                return true;
            }
            return false;
        }
    }
}
