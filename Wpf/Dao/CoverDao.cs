using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Model;

namespace Wpf.Dao
{
    class CoverDao 
    {
        Cover cover;
        public CoverDao()
        {
            cover = new Cover { Theme = "主题", TestItems = "测试目的" };
        }
        public void Set(Cover cv)
        {
            cover = cv;
        }
        public Cover Get()
        {
            return cover;
        }

    }
}
