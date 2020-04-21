using LoongEgg.LoongLogger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Model;

namespace Wpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public static User user;

        public App()
        {
            LoggerManager.Enable(LoggerType.Console | LoggerType.Debug | LoggerType.File, LoggerLevel.Debug);
            LoggerManager.WriteFatal("ＴＨＥ　ＰＲＯＧＲＡＭ　ＩＳ　ＳＴＡＲＴ");

            user = new User();
        }

    }
}
