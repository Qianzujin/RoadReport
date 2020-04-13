using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Wpf.Model;

namespace Wpf.Dao
{
    class EquipmentDao : ObservableObject
    {
        List<Equipment> equipmentList = new List<Equipment>();
        public EquipmentDao()
        {
            for (int i = 0; i < 100; i++)
            {
                //轮胎.png
                equipmentList.Add(new Equipment { Index=i, IsChecked=false, Name = i.ToString(), Code = "A", TermOfValidity = new DateTime(2007, 1, 1, 21, 21, 21), Picture = new BitmapImage(new Uri(@"./Resources/轮胎.png", UriKind.Relative)) });
            }
        }

        public List<Equipment> SelectAllEquipment()
        {
            return equipmentList;
        }

        public void UpdateData(Equipment equ)
        {
            for (int i = 0; i < equipmentList.Count(); i++)
            {
                if (equipmentList[i].Index == equ.Index)
                {
                    equipmentList[i] = equ;
                }
            }
        }

        public void DeleteData(int Index)
        {
            for (int i = 0; i < equipmentList.Count(); i++)
            {
                if (equipmentList[i].Index == Index)
                {
                    equipmentList.RemoveAt(i);
                }
            }
        }

        public void AddData(Equipment equ)
        {
            equipmentList.Add(equ);
        }

    }
}
