using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Model;

namespace Wpf.Dao
{
    class EquipmentDao : ObservableObject
    {
        List<Equipment> equipmentList = new List<Equipment>();
        public EquipmentDao()
        {
            equipmentList.Add(new Equipment { Name = "仪器1", Code = "A", TermOfValidity = new DateTime(2007, 1, 1, 21, 21, 21), Picture = "图1" });
        }

        public List<Equipment> SelectAllEquipment()
        {
            return equipmentList;
        }
    }
}
