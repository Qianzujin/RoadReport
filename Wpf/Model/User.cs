using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Wpf.Model
{

    /// <summary>
    /// 用户信息类
    /// </summary>
    public class User : ObservableObject
    {
        public int Index { get; set; }//索引
        public string UserName { get; set; }//用户名
        public string PassWord { get; set; }//密码
        public string Type { get; set; }//用户类型 admin管理员 user普通用户 visitor游客
        public BitmapImage Picture { get; set; }//头像
    }
}
