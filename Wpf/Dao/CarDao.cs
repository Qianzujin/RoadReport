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
    class CarDao 
    {
        List<Car> carList = new List<Car>();
        SqliteDbHelper db;//声明连接对象

        /// <summary>
        /// 建表语句
        /// </summary>
        private void CreateTable()
        {
            db.CreateTable("CarInfo",
                new string[] {
               "Idx" ,"Type", "CarNumber", "SeatNum", "CurbWeight", "FullRated", "Displacement",
                    "FrontSuspensionSystem","RearSuspensionSystem","DriveMethod","Gearbox",
                    "Brake","BuyingTime","InitialOdometerReading","Picture"
            },
            new string[] {"INTEGER PRIMARY KEY AUTOINCREMENT","TEXT","TEXT","INT","DOUBLE","DOUBLE","DOUBLE","TEXT","TEXT",
            "TEXT","TEXT","TEXT","DATETIME","INT", "TEXT"
            });
        }
        
        /// <summary>
        /// 客户端连接串进行连接
        /// </summary>
        public void CreateClient()
        {
            db = new SqliteDbHelper("Data Source=./sqlite.db;Version=3");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CarDao()
        {
            CreateClient();
            //CreateTable();
        }
        
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Car> SelectAll()
        {
            carList.Clear();
            var reader = db.ReadFullTable("CarInfo");
            while (reader.Read())
            {

                carList.Add(new Car
                {
                    Index = Convert.ToInt16(reader["Idx"]),
                    Type = Convert.ToString(reader["Type"]),
                    CarNumber = Convert.ToString(reader["CarNumber"]),
                    SeatNum = Convert.ToInt16(reader["SeatNum"]),
                    CurbWeight = Convert.ToDouble(reader["CurbWeight"]),
                    FullRated = Convert.ToDouble(reader["FullRated"]),
                    Displacement = Convert.ToDouble(reader["Displacement"]),
                    FrontSuspensionSystem = Convert.ToString(reader["FrontSuspensionSystem"]),
                    RearSuspensionSystem = Convert.ToString(reader["RearSuspensionSystem"]),
                    DriveMethod = Convert.ToString(reader["DriveMethod"]),
                    Gearbox = Convert.ToString(reader["Gearbox"]),
                    Brake = Convert.ToString(reader["Brake"]),
                    BuyingTime = Convert.ToDateTime(reader["BuyingTime"]),
                    InitialOdometerReading = Convert.ToInt16(reader["InitialOdometerReading"]),
                    Picture = new BitmapImage(new Uri(Convert.ToString(reader["Picture"]), UriKind.RelativeOrAbsolute))
                });

            }
            return carList;
        }
        
        /// <summary>
        /// 插入车辆信息  注意：时间插入时需要car.BuyingTime.ToString("s")后面加s
        /// </summary>
        /// <param name="car"></param>
        public void Insert(Car car)
        {
            db.InsertInto(
                "CarInfo",
                new string[] { "Type", "CarNumber", "SeatNum", "CurbWeight", "FullRated",
                    "Displacement","FrontSuspensionSystem","RearSuspensionSystem","DriveMethod",
                    "Gearbox","Brake","BuyingTime","InitialOdometerReading","Picture" },
                new string[] {
                    car.Type.ToString(),
                    car.CarNumber.ToString(),
                    car.SeatNum.ToString(),
                    car.CurbWeight.ToString(),
                    car.FullRated.ToString(),
                    car.Displacement.ToString(),
                    car.FrontSuspensionSystem.ToString(),
                    car.RearSuspensionSystem.ToString(),
                    car.DriveMethod.ToString(),car.Gearbox.ToString() ,
                    car.Brake.ToString(),
                    car.BuyingTime.ToString("s"),
                    car.InitialOdometerReading.ToString(),
                    car.Picture.ToString()
                });
        }
        
        /// <summary>
        /// 修改车辆信息
        /// </summary>
        /// <param name="car"></param>
        public void Update(Car car)
        {
            db.UpdateInto(
                "CarInfo",
                new string[] { "Idx","Type", "CarNumber", "SeatNum", "CurbWeight", "FullRated",
                    "Displacement","FrontSuspensionSystem","RearSuspensionSystem","DriveMethod",
                    "Gearbox","Brake","BuyingTime","InitialOdometerReading","Picture" },
                new string[] {
                    car.Index.ToString(),car.Type.ToString(),
                    car.CarNumber.ToString(),
                    car.SeatNum.ToString(),
                    car.CurbWeight.ToString(),
                    car.FullRated.ToString(),
                    car.Displacement.ToString(),
                    car.FrontSuspensionSystem.ToString(),
                    car.RearSuspensionSystem.ToString(),
                    car.DriveMethod.ToString(),
                    car.Gearbox.ToString(),
                    car.Brake.ToString(),
                    car.BuyingTime.ToString("s"),
                    car.InitialOdometerReading.ToString(),
                    car.Picture.ToString()
                }, "Idx", car.Index.ToString());
        }

        /// <summary>
        /// 删除车辆信息
        /// </summary>
        /// <param name="Index"></param>
        public void Delete(int Index)
        {
            db.Delete("CarInfo", new string[] { "Idx" }, new string[] { Index.ToString() });
        }

    }
}
