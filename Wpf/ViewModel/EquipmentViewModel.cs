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
        private ObservableCollection<Equipment> equipmentView = new ObservableCollection<Equipment>();//视图层数据对象

        //视图模型构造函数
        public EquipmentViewModel()
        {
            equipmentViewList = equipmentDao.SelectAll();
            UpdateViewData();
            DeleteCommand = new RelayCommand<int>(Index => Delete(Index));
            UpdateCommand = new RelayCommand<Equipment>(equ => Update(equ));
            SelectCommand = new RelayCommand<List<string>>(filterList => Select(filterList));
            InsertCommand = new RelayCommand<Equipment>(equ => Insert(equ));

        }

        //更新视图对象数据  遍历赋值 *更新视图前保证视图数据最新可以调用SelectAllEquipment
        public void UpdateViewData()
        {
            equipmentView.Clear();
            foreach (var item in equipmentViewList)
            {
                equipmentView.Add(item);
            }
        }

        //查询所有仪器 从持久化数据获取视图数据
        public void SelectAllEquipment()
        {
            equipmentViewList.Clear();
            equipmentViewList = equipmentDao.SelectAll(); 
        }

        //视图对象
        public ObservableCollection<Equipment> EquipmentView
        {
            get { return equipmentView; }
            set { equipmentView = value; RaisePropertyChanged(); }
        }

        // Commandes

        // 操作命令
        public RelayCommand<int> DeleteCommand { get; set; }
        public RelayCommand<Equipment> UpdateCommand { get; set; }
        public RelayCommand<List<string>> SelectCommand { get; set; }
        public RelayCommand<Equipment> InsertCommand { get; set; }

        // 删除函数
        private void Delete(int Index) 
        {
            OKWindow okWindow = new OKWindow("删除数据", "确实要删除这条数据吗？");
            okWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            okWindow.ShowDialog();
            if (okWindow.Ret == true)
            {
                equipmentDao.Delete(Index);
            }
            SelectAllEquipment();
            UpdateViewData();
        }

        public void Update(Equipment equ)
        {
            for (int i = 0; i < equipmentViewList.Count(); i++)
            {
                if (equipmentViewList[i].Index == equ.Index)
                {
                    equipmentDao.Update(equ);
                }
            }
            SelectAllEquipment();
            UpdateViewData();
        }

        private void Insert(Equipment equ)  
        {
            equipmentDao.Insert(equ);
            SelectAllEquipment();
            UpdateViewData();
        }

        private void Select(List<string> filterList)
        {
            equipmentViewList = equipmentDao.SelectAll();
            var retList = equipmentViewList.Where(a => a.Name.Contains(filterList[0]) && a.Code.Contains(filterList[1])).ToList();
            equipmentViewList = retList;
            UpdateViewData();
        }

    }
}
