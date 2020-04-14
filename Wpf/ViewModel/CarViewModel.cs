using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Wpf.Dao;
using Wpf.Model;

namespace Wpf.ViewModel
{

    public class CarViewModel : ViewModelBase
    {
        //视图显示类：定义可显示的属性

        CarDao carDao = new CarDao();
        List<Car> carViewList = new List<Car>();//存储从Dao层来的数据
        public ObservableCollection<Car> carView = new ObservableCollection<Car>();//视图层数据对象

        public CarViewModel()
        {
            SelectAll();
            UpdateViewData();
            DeleteCommand = new RelayCommand<int>(Index => Delete(Index));
            UpdateCommand = new RelayCommand<Car>(car => Update(car));
            SelectCommand = new RelayCommand<List<string>>(filterList => Select(filterList));
            InsertCommand = new RelayCommand<Car>(car => Insert(car));
        }

        //更新显示数据集
        private void UpdateViewData()
        {
            carView.Clear();
            for (int i = 0; i < carViewList.Count(); i++)
            {
                carView.Add(new Car
                {
                    Index = carViewList[i].Index,
                    IsChecked = carViewList[i].IsChecked,
                    Type = carViewList[i].Type,
                    CarNumber = carViewList[i].CarNumber,
                    SeatNum = carViewList[i].SeatNum,
                    Brake = carViewList[i].Brake,
                    BuyingTime = carViewList[i].BuyingTime,
                    CurbWeight = carViewList[i].CurbWeight,
                    Displacement = carViewList[i].Displacement,
                    DriveMethod = carViewList[i].DriveMethod,
                    FrontSuspensionSystem = carViewList[i].FrontSuspensionSystem,
                    FullRated = carViewList[i].FullRated,
                    Gearbox = carViewList[i].Gearbox,
                    InitialOdometerReading = carViewList[i].InitialOdometerReading,
                    RearSuspensionSystem = carViewList[i].RearSuspensionSystem,
                    Picture = carViewList[i].Picture,
                });
            }
        }


        // 操作命令
        public RelayCommand<int> DeleteCommand { get; set; }
        public RelayCommand<Car> UpdateCommand { get; set; }
        public RelayCommand<List<string>> SelectCommand { get; set; }
        public RelayCommand<Car> InsertCommand { get; set; }

        //从Dao层查询数据
        public void SelectAll()
        {
            carViewList.Clear();
            carViewList = carDao.SelectAll();
        }

        // 删除函数
        private void Delete(int Index)
        {
            OKWindow okWindow = new OKWindow("删除数据","确实要删除这条数据吗？");
            okWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            okWindow.ShowDialog();
            if (okWindow.Ret == true)
            {
                carDao.Delete(Index);
                SelectAll();
                UpdateViewData();
            }
         
        }

        public void Update(Car car)
        {
            carDao.Update(car);
            SelectAll();
            UpdateViewData();
        }

        private void Insert(Car car)
        {
            carDao.Insert(car);
            SelectAll();
            UpdateViewData();
        }

        private void Select(List<string> filterList)
        {
            carViewList = carDao.SelectAll();
            var retList = carViewList.Where(a => a.Type.Contains(filterList[0]) && a.CarNumber.Contains(filterList[1])).ToList();
            carViewList.Clear();
            carViewList = retList;
            UpdateViewData();
        }

        public ObservableCollection<Car> CarView
        {
            get { return carView; }
            set { carView = value; RaisePropertyChanged(); }
        }
    }
}
