using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Wpf.Model
{
    public class Car
    {
        public int Index { get; set; }
        public bool IsChecked { get; set; } 
        public string Type { get; set; }//车型
        public string CarNumber { get; set; }//车牌
        public int SeatNum { get; set; }//座位数
        public double CurbWeight { get; set; }//整备质量
        public double FullRated { get; set; }//满载质量
        public double Displacement { get; set; }//排量
        public string FrontSuspensionSystem { get; set; }//前悬挂系统
        public string RearSuspensionSystem { get; set; }//后悬挂系统
        public string DriveMethod { get; set; }//驱动方式
        public string Gearbox { get; set; }//变速箱 
        public string Brake { get; set; }//制动器
        public DateTime BuyingTime { get; set; }//购买时间
        public int InitialOdometerReading { get; set; }//初始里程表度数
        public BitmapImage Picture { get; set; }//车辆图片
    }
}
