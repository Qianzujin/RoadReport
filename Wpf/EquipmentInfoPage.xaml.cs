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
    /// EquipmentInfoPage.xaml 的交互逻辑
    /// 仪器信息操作窗口
    /// </summary>
    public partial class EquipmentInfoPage : Page
    {
        EquipmentViewModel equipmentViewModel = new EquipmentViewModel();

        /// <summary>
        /// 构造函数
        /// </summary>
        public EquipmentInfoPage()
        {
            InitializeComponent();
            this.DataContext = equipmentViewModel;
        }

        /// <summary>
        /// 编辑仪器信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClick(object sender, RoutedEventArgs e)
        {
            //获取当前数据传给修改窗口
            Equipment equ = this.equipmentDataGrid.SelectedItem as Equipment;

            EquipmentWindow equipmentWindow = new EquipmentWindow(equipmentViewModel, "Update", equ);
            equipmentWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            equipmentWindow.Show();
        }

        /// <summary>
        /// 触发combobox按键事件，填充下拉列表数据为仪器名称数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void equNameKeyUp(object sender, KeyEventArgs e)
        {
            var namelist = equipmentViewModel.EquipmentView.Select(t => t.Name).ToList();
            filterKeyUp(this.equName, namelist);
        }
        
        /// <summary>
        /// 仪器编号筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void equCodeKeyUp(object sender, KeyEventArgs e)
        {
            var codeList = equipmentViewModel.EquipmentView.Select(t => t.Code).ToList();
            filterKeyUp(this.equCode, codeList);
        }

        /*暂时不做时间筛选
        private void equDateKeyUp(object sender, KeyEventArgs e)
        {
            var dateList = equipmentViewModel.EquipmentView.Select(t => t.TermOfValidity.ToString()).ToList();
            filterKeyUp(this.equCode, dateList);
        }
        */

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
        /// 查询仪器信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectEquInfo(object sender, RoutedEventArgs e)
        {
            //触发Command更新数据，必须使用同一个视图数据模型
            List<string> filterList = new List<string>();
            filterList.Add(this.equName.Text);
            filterList.Add(this.equCode.Text);
            var MyVM = equipmentViewModel;
            if (MyVM != null && MyVM.SelectCommand.CanExecute(filterList))
                MyVM.SelectCommand.Execute(filterList);
        }

        /// <summary>
        /// 增加仪器信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEquInfo(object sender, RoutedEventArgs e)
        {
            //获取当前数据传给修改窗口
            //Equipment equ = this.equipmentDataGrid.SelectedItem as Equipment;
            //equ.IsChecked = true;

            EquipmentWindow equipmentWindow = new EquipmentWindow(equipmentViewModel,"add",null);
            equipmentWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            equipmentWindow.Show();
        }
    }
}
