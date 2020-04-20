using LoongEgg.LoongLogger;
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
using Wpf;
using Wpf.ViewModel;

namespace Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //声明Page
        IndexInfoPage indexInfoPage;
        CoverInfoPage coverInfoPage;
        EquipmentInfoPage equipmentInfoPage;
        CarInfoPage carInfoPage;
        TestRouteInfoPage testRouteInfoPage;

        public MainWindow()
        {
            //LoginWindow loginWindow = new LoginWindow();
            //loginWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //loginWindow.ShowDialog();
            //if (loginWindow.isLogin == false)
            //{
            //    MessageBox.Show("登陆失败！");
            //    Application.Current.Shutdown();
            //}

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
            menuNameList.Add(new MenuName { Index = 4, Icon = "\xe65a", SheetName = "仪器信息" });
            menuNameList.Add(new MenuName { Index = 5, Icon = "\xe659", SheetName = "车辆信息" });
            menuNameList.Add(new MenuName { Index = 6, Icon = "\xe659", SheetName = "路线信息" });

            this.SheetMenuListBox.ItemsSource = menuNameList;
            InitPage();
        }
        
        /// <summary>
        /// 菜单栏类
        /// </summary>
        public class MenuName
        {
            public int Index { get; set; }
            public string Icon { get; set; }
            public string SheetName { get; set; }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        public void InitPage()
        {
            indexInfoPage = new IndexInfoPage();
            coverInfoPage = new CoverInfoPage();
            equipmentInfoPage = new EquipmentInfoPage();
            carInfoPage = new CarInfoPage();
            testRouteInfoPage = new TestRouteInfoPage();
        }
        
        /// <summary>
        /// 选择操作步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelSheetMenu(object sender, SelectionChangedEventArgs e)
        {
            this.home.Visibility = Visibility.Hidden;
            this.Change_Page.Visibility = Visibility.Visible;

            if (this.SheetMenuListBox.SelectedIndex == 0)
            {
                this.Change_Page.NavigationService.Navigate(indexInfoPage);
            }
            else if (this.SheetMenuListBox.SelectedIndex == 1)
            {
                this.Change_Page.NavigationService.Navigate(coverInfoPage);
            }
            else if (this.SheetMenuListBox.SelectedIndex == 4)
            {
                this.Change_Page.NavigationService.Navigate(equipmentInfoPage);
            }
            else if (this.SheetMenuListBox.SelectedIndex == 5)
            {
                this.Change_Page.NavigationService.Navigate(carInfoPage);
            }
            else if (this.SheetMenuListBox.SelectedIndex == 6)
            {
                this.Change_Page.NavigationService.Navigate(testRouteInfoPage);
            }
        }
    
        /// <summary>
        /// 结束程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 最小化程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Min(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        /// <summary>
        /// 最大化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Max(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        
        /// <summary>
        /// 主页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoHome(object sender, MouseButtonEventArgs e)
        {
            this.Change_Page.Visibility = Visibility.Hidden;
            this.home.Visibility = Visibility.Visible;
        }

        //测试
        private void Message(object sender, RoutedEventArgs e)
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            CoverViewModel coverViewModel = new CoverViewModel();

            string i = indexViewModel.test() + coverViewModel.test();
            MessageBox.Show(i);
        }
    }
}
