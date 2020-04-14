using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Wpf.Dao;
using Wpf.Model;

namespace Wpf.ViewModel
{
    public class CarViewModel : ViewModelBase
    {
        //视图显示类：定义可显示的属性
        public class CarViewC 
        { 
            public int Index { get; set; }
            public string Type { get; set; }
            public string CarNumber { get; set; } 
            public BitmapImage Picture { get; set; }
        }

        CarDao carDao = new CarDao();
        List<Car> carViewList = new List<Car>();//存储从Dao层来的数据
        public ObservableCollection<CarViewC> carView = new ObservableCollection<CarViewC>();//视图层数据对象

        public CarViewModel()
        {
            DeleteCommand = new RelayCommand<int>(Index => Delete(Index));
            UpdateCommand = new RelayCommand<Car>(car => Update(car));
            SelectCommand = new RelayCommand<List<string>>(filterList => Select(filterList));
            AddCommand = new RelayCommand<Car>(car => Add(car));
        }

        //更新显示数据集
        private void UpdateViewData()
        {
            carView.Clear();
            for (int i = 0; i < carViewList.Count(); i++)
            {
                carView.Add(new CarViewC
                {
                    Index = carViewList[i].Index,
                    Type = carViewList[i].Type,
                    CarNumber = carViewList[i].CarNumber,
                    Picture = carViewList[i].Picture,
                });
            }
        }


        // 操作命令
        public RelayCommand<int> DeleteCommand { get; set; }
        public RelayCommand<Car> UpdateCommand { get; set; }
        public RelayCommand<List<string>> SelectCommand { get; set; }
        public RelayCommand<Car> AddCommand { get; set; }

        //从Dao层查询数据
        public void  SelectAll()
        {
            carViewList = carDao.SelectAll();
        }

        // 删除函数
        private void Delete(int Index)
        {
        
        }

        public void Update(Car car)
        {
         
        }

        private void Add(Car car)
        {
      
        }

        private void Select(List<string> filterList)
        {

        
        }

        public ObservableCollection<CarViewC> CarView
        {
            get { return carView; }
            set { carView = value; RaisePropertyChanged(); }
        }
    }
}
