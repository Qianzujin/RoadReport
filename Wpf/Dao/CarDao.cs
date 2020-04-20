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

        //建表语句
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

        //客户端连接串进行连接
        public void CreateClient()
        {
            db = new SqliteDbHelper("Data Source=./sqlite.db;Version=3");
        }

        public CarDao()
        {
            CreateClient();
            //CreateTable();
        }

        //查询所有数据
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

        //插入   注意：时间插入时需要car.BuyingTime.ToString("s")后面加s
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

        //修改
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

        //删除
        public void Delete(int Index)
        {
            db.Delete("CarInfo", new string[] { "Idx" }, new string[] { Index.ToString() });
        }


    }
}
