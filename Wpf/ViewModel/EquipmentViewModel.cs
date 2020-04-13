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
            SelectAllEquipment();
            UpdateViewData();
            DeleteCommand = new RelayCommand<int>(Index => Delete(Index));
            UpdateCommand = new RelayCommand<Equipment>(equ => Update(equ));
            SelectCommand = new RelayCommand<List<string>>(filterList => Select(filterList));
            AddCommand = new RelayCommand<Equipment>(equ => Add(equ));

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
            equipmentViewList = equipmentDao.SelectAllEquipment();
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

        // Commandes

        // 删除命令
        public RelayCommand<int> DeleteCommand { get; set; }
        public RelayCommand<Equipment> UpdateCommand { get; set; }
        public RelayCommand<List<string>> SelectCommand { get; set; }
        public RelayCommand<Equipment> AddCommand { get; set; }

        // 删除函数
        private void Delete(int Index) 
        {
           for (int i = 0; i < equipmentViewList.Count(); i++)
           {
               if (equipmentViewList[i].Index == Index)
               {
                   equipmentViewList.RemoveAt(i);
                    equipmentDao.DeleteData(Index);
               }
           }
            UpdateViewData();
        }

        public void Update(Equipment equ)
        {
            for (int i = 0; i < equipmentViewList.Count(); i++)
            {
                if (equipmentViewList[i].Index == equ.Index)
                {
                    equipmentViewList[i] = equ;
                    //equipmentDao.UpdateData(equ);
                    equipmentDao.Update(equ);
                }
            }
            UpdateViewData();
        }

        private void Add(Equipment equ) 
        {
            //equipmentViewList.Add(equ);
            equipmentDao.AddData(equ);          
            UpdateViewData();

        }


        private void Select(List<string> filterList)
        {
            equipmentViewList = equipmentDao.SelectAllEquipment();
            var retList = equipmentViewList.Where(a => a.Name.Contains(filterList[0]) && a.Code.Contains(filterList[1])).ToList();
            equipmentViewList = retList;
            UpdateViewData();
        }

    }
}
