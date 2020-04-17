using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// CarWindow.xaml 的交互逻辑
    /// 车辆信息录入窗口
    /// </summary>
    public partial class CarWindow : Window
    {
        CarViewModel carViewModelSelf = new CarViewModel();
        string operationFlag;
        Car carSelf;

        public CarWindow(CarViewModel carViewModel, string OperationFlag, Car car)
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            List<int> t = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                t.Add(i);
            }

            if (car != null)
            {
                this.DataContext = car;
                this.carSelf = car;
            }

            this.carSeatNum.ItemsSource = t;
            this.operationFlag = OperationFlag;
            this.carViewModelSelf = carViewModel;
        }

        private void Min(object sender, MouseButtonEventArgs e)
        {

        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "jpg图片|*.jpg|png图片|*.png|所有文件|*.*";
            fileDialog.ShowDialog();

            if (fileDialog.FileName != "")
            {
                //获取文件名，复制到相对路径下
                System.IO.Path.GetFileName(fileDialog.FileName);
                var path = Environment.CurrentDirectory + "\\Resources\\Picture\\" + System.IO.Path.GetFileName(fileDialog.FileName);

                if (File.Exists(path))
                {
                    MessageBox.Show("文件已存在！");
                    //return;
                }
                else
                {
                    File.Copy(fileDialog.FileName, path);
                }
                this.carPicture.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                //this.img.Source = new BitmapImage(new Uri(@"/Wpf;component/Resources/Picture/" + System.IO.Path.GetFileName(fileDialog.FileName), UriKind.Relative));
            }
        }

        //提交信息
        private void Submit(object sender, RoutedEventArgs e)
        {
            //校验  后面考虑使用校验
            if ((this.carPicture.Source != null) && (
                this.carType.Text != "" &&
                this.carNumber.Text != "" &&
                this.carSeatNum.Text != "" &&
                this.carFullRated.Text != "" &&
                this.carCurbWeight.Text != "" &&
                this.carDisplacement.Text != "" &&
                this.carFrontSuspensionSystem.Text != "" &&
                this.carRearSuspensionSystem.Text != "" &&
                this.carDriveMethod.Text != "" &&
                this.carGearbox.Text != "" &&
                this.carBrake.Text != "" &&
                this.carBuyingTime.Text != "" &&
                this.carInitialOdometerReading.Text != "" &&
                this.carPicture.Source.ToString() != ""
                ))
            {
                try
                {
                    Car car = new Car
                    {
                        Index = 0,                   
                        Type = this.carType.Text,
                        CarNumber = this.carNumber.Text,
                        SeatNum = Convert.ToInt16(this.carSeatNum.Text),
                        FullRated = Convert.ToDouble(this.carFullRated.Text),
                        CurbWeight = Convert.ToDouble(this.carCurbWeight.Text),
                        Displacement = Convert.ToDouble(this.carDisplacement.Text),
                        FrontSuspensionSystem = this.carFrontSuspensionSystem.Text,
                        RearSuspensionSystem = this.carRearSuspensionSystem.Text,
                        DriveMethod = this.carDriveMethod.Text,
                        Gearbox = this.carGearbox.Text,
                        Brake = this.carBrake.Text,
                        BuyingTime = Convert.ToDateTime(this.carBuyingTime.Text.Trim()),
                        InitialOdometerReading = Convert.ToInt16(this.carInitialOdometerReading.Text),
                        Picture = new BitmapImage(new Uri(this.carPicture.Source.ToString(), UriKind.RelativeOrAbsolute))
                    };

                    //新增数据
                    if (this.operationFlag == "Insert")
                    {                   
                        var MyVM = carViewModelSelf;
                        if (MyVM != null && MyVM.InsertCommand.CanExecute(car))
                        {
                            MyVM.InsertCommand.Execute(car);
                            this.Close();
                        }
                    }
                    //修改数据
                    else if (this.operationFlag == "Update")
                    {
                        //修改索引值
                        car.Index = this.carSelf.Index;
                        var MyVM = carViewModelSelf;

                        OKWindow okWindow = new OKWindow("修改数据", "确实要修改这条数据吗？");
                        okWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        okWindow.ShowDialog();
                        if (okWindow.Ret == true)
                        {
                            if (MyVM != null && MyVM.UpdateCommand.CanExecute(car))
                            {
                                MyVM.UpdateCommand.Execute(car);
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("有一点小问题！");
                    }
                }
                catch
                {
                    MessageBox.Show("录入失败,请检查数据格式是否有误,请重新录入！");
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("录入失败,请检查数据格式是否有误,请重新录入！");
            }
        }
    }
}
