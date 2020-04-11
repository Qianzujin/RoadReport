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
                equipmentList.Add(new Equipment { Index=i,  Name = i.ToString(), Code = "A", TermOfValidity = new DateTime(2007, 1, 1, 21, 21, 21), Picture = new BitmapImage(new Uri(@"./Resources/轮胎.png", UriKind.Relative)) });
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
                    equipmentList[i].Name = equ.Name;
                    equipmentList[i].Code = equ.Code;
                    equipmentList[i].TermOfValidity = equ.TermOfValidity;
                    equipmentList[i].Picture = equ.Picture;
                }
            }
        }

        public void DeleteData(Equipment equ)
        {
            for (int i = 0; i < equipmentList.Count(); i++)
            {
                if (equipmentList[i] == equ)
                {
                    equipmentList.Remove(equipmentList[i]);
                }
            }
        }

   
    }
}
