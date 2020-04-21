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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Model;
using Wpf.ViewModel;

namespace Wpf
{
    /// <summary>
    /// UserInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoPage : Page
    {
        public UserViewModel userViewModel = new UserViewModel();
        public UserInfoPage()
        {
            InitializeComponent();
            this.DataContext = userViewModel;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            User user = this.userDataGrid.SelectedItem as User;
            UserWindow userWindow = new UserWindow(userViewModel, user,"Update");
            userWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            userWindow.Show();
        }

        private void AddUserInfo(object sender, RoutedEventArgs e)
        {
            User user = this.userDataGrid.SelectedItem as User;
            UserWindow userWindow = new UserWindow(userViewModel, user, "Insert");
            userWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            userWindow.Show();
        }
    }
}
