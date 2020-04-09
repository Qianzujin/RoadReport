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
using Wpf.ViewModel;

namespace Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (render, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            List<MenuName> menuNameList = new List<MenuName>();

            menuNameList.Add(new MenuName { Index = 0, Icon = "\xe652", SheetName = "封面信息" });
            menuNameList.Add(new MenuName { Index = 1, Icon = "\xe653", SheetName = "目录信息" });
            menuNameList.Add(new MenuName { Index = 2, Icon = "\xe654", SheetName = "结论信息" });
            menuNameList.Add(new MenuName { Index = 3, Icon = "\xe655", SheetName = "轮胎信息" });
            this.SheetMenuListBox.ItemsSource = menuNameList;
         
        }

        public class MenuName
        {
            public int Index { get; set; }
            public string Icon { get; set; }
            public string SheetName { get; set; }
        }

        private void SelSheetMenu(object sender, SelectionChangedEventArgs e)
        {
            this.home.Visibility = Visibility.Hidden;
            this.Change_Page.Visibility = Visibility.Visible;

            if (this.SheetMenuListBox.SelectedIndex == 0)
            {
                this.Change_Page.NavigationService.Navigate(new indexInfoPage());
            }
            else if (this.SheetMenuListBox.SelectedIndex == 1)
            {
                this.Change_Page.NavigationService.Navigate(new coverInfoPage());
            }
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Min(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void Max(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else {
                this.WindowState = WindowState.Normal;
            }
        }

        private void GoHome(object sender, MouseButtonEventArgs e)
        {
            this.Change_Page.Visibility = Visibility.Hidden;
            this.home.Visibility = Visibility.Visible;
        }

        private void Message(object sender, RoutedEventArgs e)
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            CoverViewModel coverViewModel = new CoverViewModel();

            string i = indexViewModel.test() + coverViewModel.test();
            MessageBox.Show(i);    
        }
    }
}
