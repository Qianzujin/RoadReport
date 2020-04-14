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
            menuNameList.Add(new MenuName { Index = 4, Icon = "\xe65a", SheetName = "仪器信息" });
            menuNameList.Add(new MenuName { Index = 4, Icon = "\xe659", SheetName = "车辆信息" });

            this.SheetMenuListBox.ItemsSource = menuNameList;
            InitPage();
        }

        //菜单栏类
        public class MenuName
        {
            public int Index { get; set; }
            public string Icon { get; set; }
            public string SheetName { get; set; }
        }


        //初始化页面
        public void InitPage()
        {
            indexInfoPage = new IndexInfoPage();
            coverInfoPage = new CoverInfoPage();
            equipmentInfoPage = new EquipmentInfoPage();
            carInfoPage = new CarInfoPage();
        }

        
        //选择操作步
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
        }

        //结束程序
        private void Close(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //最小化窗口
        private void Min(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        //最大化窗口
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

        //主页
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
