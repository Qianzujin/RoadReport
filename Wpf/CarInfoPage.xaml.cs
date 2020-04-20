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
    /// CarInfoPage.xaml 的交互逻辑
    /// 车辆信息操作窗口
    /// </summary>
    public partial class CarInfoPage : Page
    {
        CarViewModel carViewModel = new CarViewModel();

        /// <summary>
        /// 构造函数
        /// </summary>
        public CarInfoPage()
        {
            InitializeComponent();
            this.DataContext = carViewModel;
        }
      
        /// <summary>
        /// 筛选通用方法
        /// </summary>
        /// <param name="itemCombobox"></param>
        /// <param name="itemList"></param>
        private void filterKeyUp(ComboBox itemCombobox, List<string> itemList)
        {
            List<string> sourcesList = new List<string>();

            foreach (var item in itemList)
            {
                if (item.Contains(itemCombobox.Text.Trim()) ||
                    item.Contains(itemCombobox.Text.ToLower()) ||
                    item.Contains(itemCombobox.Text.ToUpper()))
                {
                    sourcesList.Add(item);
                }
            }

            itemCombobox.ItemsSource = sourcesList;
            itemCombobox.IsDropDownOpen = true;
        }

        /// <summary>
        /// 编辑车辆信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClick(object sender, RoutedEventArgs e)
        {
            //获取当前数据传给修改窗口
            Car car = this.carDataGrid.SelectedItem as Car;
            CarWindow carWindow = new CarWindow(carViewModel, "Update", car);
            carWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            carWindow.Show();
        }

        /// <summary>
        /// 根据筛选条件筛选车辆信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectCarInfo(object sender, RoutedEventArgs e)
        {
            //触发Command更新数据，必须使用同一个视图数据模型
            List<string> filterList = new List<string>();
            filterList.Add(this.carType.Text);
            filterList.Add(this.carNumber.Text);
            var MyVM = carViewModel;
            if (MyVM != null && MyVM.SelectCommand.CanExecute(filterList))
                MyVM.SelectCommand.Execute(filterList);
        }

        /// <summary>
        /// 增加车辆信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCarInfo(object sender, RoutedEventArgs e)
        {
            CarWindow carWindow = new CarWindow(carViewModel, "Insert",null);
            carWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            carWindow.Show();
        }

        /// <summary>
        /// 当键盘抬起时触发车辆类型输入框的内容刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carTypeKeyUp(object sender, KeyEventArgs e)
        {
            var typeList = carViewModel.CarView.Select(t => t.Type).ToList();
            filterKeyUp(this.carType, typeList);
        }

        /// <summary>
        /// 当键盘抬起时触发车辆牌号输入框的内容刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carNumberKeyUp(object sender, KeyEventArgs e)
        {
            var numberList = carViewModel.CarView.Select(t => t.CarNumber).ToList();
            filterKeyUp(this.carNumber, numberList);
        }
    }
}
