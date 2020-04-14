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
    /// </summary>
    public partial class CarInfoPage : Page
    {
        CarViewModel carViewModel = new CarViewModel(); 

        public CarInfoPage()
        {
            InitializeComponent();
            this.DataContext = carViewModel;
        }

        //筛选通用方法
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

        private void EditClick(object sender, RoutedEventArgs e)
        {
            //获取当前数据传给修改窗口
            Car car = this.carDataGrid.SelectedItem as Car;
            car.IsChecked = true;

            CarWindow carWindow = new CarWindow(carViewModel, "Update");
            carWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            carWindow.Show();
        }

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

        private void AddCarInfo(object sender, RoutedEventArgs e)
        {
            CarWindow carWindow = new CarWindow(carViewModel,"Insert");
            carWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            carWindow.Show();
        }

        private void carTypeKeyUp(object sender, KeyEventArgs e)
        {
            var typeList = carViewModel.CarView.Select(t => t.Type).ToList();
            filterKeyUp(this.carType, typeList);
        }

        private void carNumberKeyUp(object sender, KeyEventArgs e)
        {
            var numberList = carViewModel.CarView.Select(t => t.CarNumber).ToList();
            filterKeyUp(this.carNumber, numberList);
        }
    }
}
