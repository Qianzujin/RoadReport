using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Wpf.Model
{
    //路面类型
    public class PavementType : ObservableObject
    {
        public int Id { get; set; } //是否选中
        public int Index { get; set; }//父索引
        public string Name { get; set; } //路面类型名称
        public string Length { get; set; } //路面长度
        public string Percent { get; set; } //路面长度百分比
        public BitmapImage Picture { get; set; } //路面类型图片
    }

    /*试验路线 查询索引，当索引为1时，先查询PavementType信息将
     * 所有索引1的路面信息查出来放到临时变量中，再查TestRoute信息，
     * 存储信息时需要额外使用PavementTypeInfo的存储方法
     * 两个信息通过Index进行关联创建两张表PavemenType及TestRoute
    */
    public class TestRouteBase
    {
        public int Index { get; set; }//索引
        public string TestRoutes { get; set; } //试验路线
        public string LineMileage { get; set; }//线路里程
        public string Material { get; set; }//材料
        public string Time { get; set; }//耗时
        public BitmapImage Picture { get; set; } //试验路线图片

    }

    public class TestRoute 
    {
        private TestRouteBase testRouteBase;
        public List<PavementType> pavementTypeInfo;

        public TestRouteBase TestRouteBase
        {
            get { return testRouteBase; }
            set { testRouteBase = value; }
        }


        public List<PavementType> PavementTypeInfo
        {
            get { return pavementTypeInfo; }
            set { pavementTypeInfo = value; }
        }
        //public TestRouteBase TestRouteBase { get; set; } //路试线路基本信息
        //public List<PavementType> PavementTypeInfo { get; set; }//路面类型信息
    }
}
