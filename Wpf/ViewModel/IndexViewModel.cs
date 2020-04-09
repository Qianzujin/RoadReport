using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Dao;
using Wpf.Model;

namespace Wpf.ViewModel
{
    class IndexViewModel : ViewModelBase
    {
        IndexDao indexDao = new IndexDao();//连接Dao层
        Index index;//初始化视图模型对象

        private ObservableCollection<Index> indexView = new ObservableCollection<Index>();

        public IndexViewModel()
        {
            SelectIndexData();
            indexView.Add(index);
        }

        private void SelectIndexData()
        {
            index = indexDao.Get(); //使用Dao层获取数据
        }

        public ObservableCollection<Index> IndexView
        {
            get { return indexView; }
            set { indexView = value; RaisePropertyChanged(); }
        }

        public string test()
        {
            return index.IndexName;
        }

    }
}
