using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Dao;
using Wpf.Model;

namespace Wpf.ViewModel
{
    class EquipmentViewModel : ViewModelBase
    {
        EquipmentDao equipmentDao = new EquipmentDao();//构造Dao对象用于查询或者持久化仪器数据
        List<Equipment> equipmentList = new List<Equipment>();//构造仪器列表存储Dao层查询的仪器数据

        private ObservableCollection<Equipment> equipmentView = new ObservableCollection<Equipment>();

        //视图模型构造函数
        public EquipmentViewModel()
        {
            SelectAllEquipment();
            UpdateViewData();
        }

        //更新视图对象数据  遍历赋值
        public void UpdateViewData()
        {
            foreach (var item in equipmentList)
            {
                equipmentView.Add(item);
            }
        }

        //查询所有仪器
        public void SelectAllEquipment()
        {
            equipmentList = equipmentDao.SelectAllEquipment();
        }

        //视图对象
        public ObservableCollection<Equipment> EquipmentView
        {
            get { return equipmentView; }
            set { equipmentView = value; RaisePropertyChanged(); }
        }
    }
}
