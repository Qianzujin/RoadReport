using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wpf.Dao;
using Wpf.Model;

namespace Wpf.ViewModel
{
    public class EquipmentViewModel : ViewModelBase
    {
        EquipmentDao equipmentDao = new EquipmentDao();//构造Dao对象用于查询或者持久化仪器数据
        List<Equipment> equipmentViewList = new List<Equipment>();//构造仪器列表存储Dao层查询的仪器数据

        private ObservableCollection<Equipment> equipmentView = new ObservableCollection<Equipment>();

        //视图模型构造函数
        public EquipmentViewModel()
        {
            SelectAllEquipment();
            UpdateViewData();
            SelectedCommand = new RelayCommand<List<Equipment>>(t => Select(t));
        }

        //更新视图对象数据  遍历赋值 *更新视图前保证视图数据最新可以调用SelectAllEquipment
        public void UpdateViewData()
        {
            foreach (var item in equipmentViewList)
            {
                equipmentView.Add(item);
            }
        }

        //查询所有仪器 从持久化数据获取视图数据
        public void SelectAllEquipment()
        {
            var tmp = equipmentDao.SelectAllEquipment();
            equipmentViewList.Clear();
            equipmentViewList = tmp;
        }

        //视图对象
        public ObservableCollection<Equipment> EquipmentView
        {
            get { return equipmentView; }
            set { equipmentView = value; RaisePropertyChanged(); }
        }

        //暂时用不到
        public void SubmitData(Equipment equ)
        {
            equipmentDao.UpdateData(equ);
        }

        public void DeleteData(Equipment equ)
        {
            equipmentDao.DeleteData(equ);
        }

        // Commandes
        public RelayCommand<List<Equipment>> SelectedCommand { get; set; }

        private void Select(List<Equipment> t)
        {
            equipmentViewList.RemoveAt(1);
            equipmentView.Clear();
            UpdateViewData();
        }
    }
}
