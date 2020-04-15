using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class TestRouteViewModel : ViewModelBase
    {
        //持久层对象Dao
        TestRouteDao testRouteDao = new TestRouteDao();
        public List<TestRoute> testRouteList = new List<TestRoute>();//临时保存查询数据

        //测试路线基础信息视图对象
        //  public ObservableCollection<TestRoute> testRouteView = new ObservableCollection<TestRoute>();//视图层数据对象

        //测试路线基础信息视图对象
        public ObservableCollection<TestRouteBase> testRouteBaseView = new ObservableCollection<TestRouteBase>();

        //测试路线对应的路面类型信息
        public ObservableCollection<PavementType> pavementTypeView = new ObservableCollection<PavementType>();


        public TestRouteViewModel()
        {
            // SelectAll();
            PavementType pt = new PavementType() {
                Index =1, Id=1,  Length="22", Name="测试",  Percent="11",
                 Picture = new System.Windows.Media.Imaging.BitmapImage(new Uri("./Resources/Picture/1.jpg", UriKind.RelativeOrAbsolute))
            };
            List<PavementType> ptList = new List<PavementType>();
            


            for (int i = 0; i < 11; i++)
            {
                ptList.Add(new PavementType()
                {
                    Index = 1,
                    Id = i,
                    Length = "22",
                    Name = "测试",
                    Percent = "11",
                    Picture = new System.Windows.Media.Imaging.BitmapImage(new Uri("./Resources/Picture/1.jpg", UriKind.RelativeOrAbsolute))
                });
            }

            testRouteList.Add(new TestRoute
            {
                PavementTypeInfo = ptList,
                TestRouteBase = new TestRouteBase()
                {
                    Index = 1,
                    IsChecked = false,
                    LineMileage = "1",
                    Material = "1",
                    TestRoutes = "lux",
                    Time = "a",
                    Picture = new System.Windows.Media.Imaging.BitmapImage(new Uri("./Resources/Picture/1.jpg", UriKind.RelativeOrAbsolute))
                }
            });
            UpdateViewData();


            DeleteCommand = new RelayCommand<int>(Index => Delete(Index));
            UpdateCommand = new RelayCommand<TestRoute>(tr => Update(tr));
            InsertCommand = new RelayCommand<TestRoute>(tr => Insert(tr));
            UpdatePavementTypeCommand = new RelayCommand<PavementType>(t => UpdatePavementType(t));
        // SelectCommand = new RelayCommand<List<string>>(filterList => Select(filterList));
    }
         
        //获取当前完整数据
        public TestRoute GetCurrectTestRoute(int Index)
        {
            foreach (var item in testRouteList)
            {
                if (item.TestRouteBase.Index == Index)
                {
                    return item;
                }
            }
            return null;
        }

        public ObservableCollection<PavementType> PavementTypeView
        {
            get { return pavementTypeView; }
            set { pavementTypeView = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<TestRouteBase> TestRouteBaseView
        {
            get { return testRouteBaseView; }
            set { testRouteBaseView = value; RaisePropertyChanged(); }
        }

        // 操作命令
        public RelayCommand<int> DeleteCommand { get; set; }
        public RelayCommand<TestRoute> UpdateCommand { get; set; }     
        public RelayCommand<TestRoute> InsertCommand { get; set; }
        public RelayCommand<PavementType> UpdatePavementTypeCommand { get; set; }
        //public RelayCommand<List<string>> SelectCommand { get; set; }

        //更新PavementTypeView显示数据集 
        public void UpdatePavementTypeViewData(int Index)
        {
            pavementTypeView.Clear();
            for (int i = 0; i < testRouteList.Count(); i++)
            {
                if (testRouteList[i].TestRouteBase.Index == Index)
                {
                    foreach (var item in testRouteList[i].PavementTypeInfo)
                    {
                        pavementTypeView.Add(item);
                    }
                }
            }
        }

        //更新路面类型显示数据集
        private void UpdateViewData() 
        {
            pavementTypeView.Clear();
            for (int i = 0; i < testRouteList.Count(); i++)
            {
                testRouteBaseView.Add(testRouteList[i].TestRouteBase);
            }
        }

        //查询所有测试路线 
        private void SelectAll()
        {
            testRouteList.Clear();
            testRouteList = testRouteDao.SelectAll();
            UpdateViewData();
        }

        //删除测试路线 
        private void Delete(int Index)
        {
            //testRouteDao.Delete(Index);
            for (int i = 0; i < testRouteList.Count(); i++)
            {
                if (testRouteList[i].TestRouteBase.Index == Index)
                {
                    testRouteList.RemoveAt(i);
                }
            }
            SelectAll();
        }

        //修改测试路线 
        private void Update(TestRoute tr)
        {
            testRouteDao.Update(tr);
            SelectAll();
        }

        //增加测试路线 
        private void Insert(TestRoute tr) 
        {
            testRouteDao.Insert(tr);
            SelectAll();
        }



        private void UpdatePavementType(PavementType pt)
        {

            //获取选中的测试路线路面信息
            foreach (var item in this.testRouteList)
            {
                if (item.TestRouteBase.Index == pt.Index)
                {
                    for (int i = 0; i < item.PavementTypeInfo.Count(); i++)
                    {
                        if (item.PavementTypeInfo[i].Id == pt.Id)
                        {
                            //找到需要修改的元素PavementType
                            //System.Windows.MessageBox.Show("");
                            item.PavementTypeInfo[i] = pt;
                        }
                    }
                }
            }
            //testRouteBaseView.Clear();
            //for (int i = 0; i < testRouteList.Count(); i++)
            //{
            //    testRouteBaseView.Add(testRouteList[i].TestRouteBase);
            //}
        }
    }
}
