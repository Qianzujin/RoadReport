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
    class CoverViewModel : ViewModelBase
    {
        CoverDao coverDao = new CoverDao();//连接Dao层
        Cover cover;//初始化视图模型对象

        private ObservableCollection<Cover> coverView = new ObservableCollection<Cover>();

        public CoverViewModel()
        {
            SelectCoverData();
            coverView.Add(cover);
        }

        private void SelectCoverData() 
        {
            cover = coverDao.Get(); //使用Dao层获取数据
        }

        public ObservableCollection<Cover> CoverView  
        {
            get { return coverView; }
            set { coverView = value; RaisePropertyChanged(); }
        }

        public string test()
        {
            return cover.TestItems;
        }
    }


}
