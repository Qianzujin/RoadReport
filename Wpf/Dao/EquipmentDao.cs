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
    /// <summary>
    /// 连接数据库Sqlite暂定 也可是其他数据库
    /// </summary>
    class EquipmentDao
    {
        List<Equipment> equipmentList = new List<Equipment>();
        SqliteDbHelper db;//声明连接对象
      
        /// <summary>
        /// 构造函数
        /// </summary>
        public EquipmentDao()
        {
            //创建客户端     
            CreateClient();
            //CreateTable();
        }

        /// <summary>
        /// 建表语句
        /// </summary>
        private void CreateTable()
        {
            db.CreateTable("EquipmentInfo", new string[] { "Idx",  "Name", "Code", "TermOfValidity", "Picture" }, new string[] { "INTEGER PRIMARY KEY AUTOINCREMENT",  "TEXT", "TEXT", "DATETIME", "TEXT" });
        }
        
        /// <summary>
        /// 客户端连接串进行连接
        /// </summary>
        public void CreateClient()
        {
            db = new SqliteDbHelper("Data Source=./sqlite.db;Version=3");
        }
      
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 插入仪器信息  注意：时间插入时需要equ.TermOfValidity.ToString("s")后面加s
        /// </summary>
        /// <param name="equ"></param>
        public void Insert(Equipment equ)
        {
            //不需要插入主键自增值
            db.InsertInto(
                "EquipmentInfo",
                new string[] {  "Name", "Code", "TermOfValidity", "Picture" }, 
                new string[] {  equ.Name.ToString(), equ.Code.ToString(), equ.TermOfValidity.ToString("s"), equ.Picture.ToString() });
        }
        
        /// <summary>
        /// 修改仪器信息
        /// </summary>
        /// <param name="equ"></param>
        public void Update(Equipment equ)
        {
            db.UpdateInto(
                "EquipmentInfo", 
                new string[] { "Idx",  "Name", "Code", "TermOfValidity", "Picture" }, 
                new string[] { equ.Index.ToString(), equ.Name.ToString(),equ.Code.ToString(),
       equ.TermOfValidity.ToString("s"), equ.Picture.ToString() },"Idx",equ.Index.ToString());
        }
      
        /// <summary>
        /// 删除仪器信息
        /// </summary>
        /// <param name="Index"></param>
        public void Delete(int Index)
        {
            db.Delete("EquipmentInfo", new string[] { "Idx" }, new string[] { Index.ToString() });
        }

    }
}
