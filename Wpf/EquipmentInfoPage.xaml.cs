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
    /// </summary>
    public partial class EquipmentInfoPage : Page
    {
        EquipmentViewModel equipmentViewModel = new EquipmentViewModel();
        public EquipmentInfoPage()
        {
           
            InitializeComponent();
            this.DataContext = equipmentViewModel;

        }

        public void Refresh()
        {
            this.DataContext = equipmentViewModel;
            var t = equipmentViewModel;
            MessageBox.Show("d");
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            //获取当前数据传给修改窗口
            var equ = (Equipment)this.equipmentDataGrid.SelectedItem; 
            EquipmentWindow equipmentWindow = new EquipmentWindow(equ , equipmentViewModel);
            equipmentWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            equipmentWindow.Show();         
        }
    }
}
