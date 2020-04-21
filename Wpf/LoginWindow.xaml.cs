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
using Wpf.Dao;

namespace Wpf
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// 登陆窗口
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool isLogin = false;
        public LoginWindow()
        {     
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            UserDao userDao = new UserDao();

            if (Verify() != true)
            {
                MessageBox.Show("验证信息不能为空！");
                return;
            }
            App.user.UserName = this.usrName.Text;
            App.user.Type = "管理员";
            if (userDao.VertifyLogin(this.usrName.Text, this.usrPassWord.Password) == true)
            {
                isLogin = true;
            }
            else {
                isLogin = false;
            }
            this.Close();
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool Verify()
        {
        
            if (this.usrName.Text != "" &&
                this.usrPassWord.Password != "")
            {
                return true;
            }
            return false;
        }
    }
}
