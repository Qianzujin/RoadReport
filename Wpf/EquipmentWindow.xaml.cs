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
using Wpf.Model;
using Wpf.ViewModel;

namespace Wpf
{
    /// <summary>
    /// EquipmentWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentWindow : Window
    {
        EquipmentViewModel equipmentViewModelSelf;

        public EquipmentWindow(Equipment equ, EquipmentViewModel equipmentViewModel)
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            this.DataContext = equ;
            this.equipmentViewModelSelf = equipmentViewModel;
        }




        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Max(object sender, MouseButtonEventArgs e)
        {

        }

        private void Min(object sender, MouseButtonEventArgs e)
        {

        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            this.img.Source = new BitmapImage(new Uri(@"./Resources/轮胎.png", UriKind.Relative));
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.datePicker.Text);
            MessageBox.Show(this.datePicker.DataContext.ToString());
            Equipment equ = new Equipment
                {
                    Name = this.nameTxtBox.Text,
                    Code = this.codeTxtBox.Text,
                    TermOfValidity = Convert.ToDateTime(this.datePicker.Text),
                    Picture = (BitmapImage)img.Source
                };
                equipmentViewModelSelf.SubmitData(equ);
            }
   
        }
    }

