using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Wpf.Model
{
    public class Equipment
    {
        public int Index { get; set; }//索引
        public string Name { get; set; }//仪器名称
        public string Code { get; set; }//仪器编号
        public DateTime TermOfValidity { get; set; }//仪器有效期
        public BitmapImage Picture { get; set; } //仪器图片
    } 
}
