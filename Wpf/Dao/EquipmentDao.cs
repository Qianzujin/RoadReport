using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Wpf.Model;

namespace Wpf.Dao
{
    //连接数据库Sqlite暂定 也可是其他数据库
    class EquipmentDao : ObservableObject
    {
        List<Equipment> equipmentList = new List<Equipment>();
        SqliteDbHelper db;//声明连接对象

        //构造函数
        public EquipmentDao()
        {
            //创建客户端     
            CreateClient();
            //CreateTable();
        }

        private void CreateTable()
        {
            db.CreateTable("EquipmentInfo", new string[] { "Idx",  "Name", "Code", "TermOfValidity", "Picture" }, new string[] { "INTEGER PRIMARY KEY AUTOINCREMENT",  "TEXT", "TEXT", "DATETIME", "TEXT" });
        }

        //客户端连接串进行连接
        public void CreateClient()
        {
            db = new SqliteDbHelper("Data Source=./sqlite.db;Version=3");
        }

        //查询所有数据
        public List<Equipment> SelectAll()
        {
            equipmentList.Clear();
            var reader = db.ReadFullTable("EquipmentInfo");
            while (reader.Read())
            {
                equipmentList.Add(new Equipment
                {
                    Index = Convert.ToInt16(reader["Idx"]),
                    Name = Convert.ToString(reader["Name"]),
                    Code = Convert.ToString(reader["Code"]),
                    TermOfValidity = Convert.ToDateTime(reader["TermOfValidity"]),
                    Picture = new BitmapImage(new Uri(Convert.ToString(reader["Picture"]), UriKind.RelativeOrAbsolute))
                });
            }
            return equipmentList;
        }

        //插入   注意：时间插入时需要equ.TermOfValidity.ToString("s")后面加s
        public void Insert(Equipment equ)
        {
            //不需要插入主键自增值
            db.InsertInto(
                "EquipmentInfo",
                new string[] {  "Name", "Code", "TermOfValidity", "Picture" }, 
                new string[] {  equ.Name.ToString(), equ.Code.ToString(), equ.TermOfValidity.ToString("s"), equ.Picture.ToString() });
        }

        //修改
        public void Update(Equipment equ)
        {
            db.UpdateInto(
                "EquipmentInfo", 
                new string[] { "Idx",  "Name", "Code", "TermOfValidity", "Picture" }, 
                new string[] { equ.Index.ToString(), equ.Name.ToString(),equ.Code.ToString(),
       equ.TermOfValidity.ToString("s"), equ.Picture.ToString() },"Idx",equ.Index.ToString());
        }
        
        //删除
        public void Delete(int Index)
        {
            db.Delete("EquipmentInfo", new string[] { "Idx" }, new string[] { Index.ToString() });
        }





        //向数据库插入100条测试数据
        private void Insert100()
        {
            for (int i = 0; i < equipmentList.Count(); i++)
            {
                db.InsertInto("EquipmentInfo", new string[]
                { "Idx", "Name", "Code", "TermOfValidity", "Picture" },
                new string[] { equipmentList[i].Index.ToString(),
                    equipmentList[i].Name.ToString(),
                    equipmentList[i].Code.ToString(),
                    equipmentList[i].TermOfValidity.ToString("s"),
                    equipmentList[i].Picture.ToString()
                });
            }
        }

        //本地查询
        public List<Equipment> SelectAllEquipment()
        {
            return equipmentList;
        }

        //本地修改
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

        //本地删除
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

        //本地增加
        public void AddData(Equipment equ)
        {
            equipmentList.Add(equ);
        }

    }
}
