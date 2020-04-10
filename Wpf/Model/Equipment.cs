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
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime TermOfValidity { get; set; }
        public BitmapImage Picture { get; set; } 
    }
}
