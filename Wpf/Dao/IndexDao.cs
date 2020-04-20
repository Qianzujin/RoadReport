using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Model;

namespace Wpf.Dao
{
    class IndexDao 
    {
        Index index;
        public IndexDao()
        {
            index = new Index { IndexName = "目录名字", IndexPage = "1" };
        }
        public void Set(Index idx)
        {
            index = idx;
        }
        public Index Get()
        {
            return index;
        }
    }
}
